using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public class ResourceInfo
    {
        public float typeProductionPerSecond;
        public float typeQuantity;
    }

    private float timePassed = 0;
    private const float ONE_SECOND_TO_RESET = 1;

    public static ResourceManager Instace { get; private set; }

    private ItemTypeListSO itemTypeListSO;

    public Dictionary<string, ResourceInfo> ResourceTypeQuantityAndProductionPerSecond { get; private set; } = new Dictionary<string, ResourceInfo>();

    public event EventHandler OnResourceChanged;

    void Awake()
    {
        Instace = this;

        itemTypeListSO = Resources.Load<ItemTypeListSO>(typeof(ItemTypeListSO).Name);

        foreach (var resource in itemTypeListSO.Items)
        {
            ResourceTypeQuantityAndProductionPerSecond.Add(resource.nameString, new ResourceInfo { typeProductionPerSecond = 0, typeQuantity = 0 }) ;
        }
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed >= ONE_SECOND_TO_RESET)
        {
            AttInformationsAboutResources();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            PrintSomeInformation();
        }
    }

    private void AttInformationsAboutResources()
    {
        timePassed = 0;
        foreach (var resource in ResourceTypeQuantityAndProductionPerSecond)
        {
            var resourceInformation = resource.Value;
            resourceInformation.typeQuantity += resourceInformation.typeProductionPerSecond;
        }

        OnResourceChanged?.Invoke(this, EventArgs.Empty);
    }

    public void IncreaseAmountProductionPerSecond(string typeName, float quantityToIncrease)
    {
        ResourceTypeQuantityAndProductionPerSecond[typeName].typeProductionPerSecond += quantityToIncrease;
    }

    public float GetQuantityAmountByType(string typeName)
    {
        return ResourceTypeQuantityAndProductionPerSecond[typeName].typeQuantity;
    }

    private void PrintSomeInformation()
    {
        foreach (var resource in itemTypeListSO.Items)
        {
            var formatedString = "Type :" + resource.nameString.ToUpper() + " | Quantity: " + ResourceTypeQuantityAndProductionPerSecond[resource.nameString].typeQuantity;
            Debug.Log(formatedString);
        }
    }


}
