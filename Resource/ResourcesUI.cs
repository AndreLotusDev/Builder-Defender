using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    private ItemTypeListSO itemsTypes;
    Dictionary<string, TextMeshProUGUI> resourceUIPerType = new Dictionary<string, TextMeshProUGUI>();

    void Awake()
    {
        SetUIOfResources();
    }

    private void SetUIOfResources()
    {
        itemsTypes = SOHelper.GetTypemListSO();

        var templateResourceUI = transform.Find("resourceTemplate");

        var indexResource = 0;
        var offsetPositionBetweenEachOther = 160;
        foreach (var resourceType in itemsTypes.Items)
        {
            Transform templateResourceUITemp = Instantiate(templateResourceUI, transform);
            templateResourceUITemp.GetComponent<RectTransform>().anchoredPosition = new Vector2(-offsetPositionBetweenEachOther * indexResource, 0);
            
            templateResourceUITemp.Find("resourceImage").GetComponent<Image>().sprite = resourceType.sprite;
            var textResourceTemp = templateResourceUITemp.Find("resourceText").GetComponent<TextMeshProUGUI>();
            textResourceTemp.SetText(0.ToString());

            templateResourceUITemp.gameObject.SetActive(true);

            resourceUIPerType.Add(resourceType.nameString, textResourceTemp);

            indexResource++;
        }

        templateResourceUI.gameObject.SetActive(false);
    }

    void Start()
    {
        ResourceManager.Instace.OnResourceChanged += ResourceManager_OnResourceChanged;
    }

    private void ResourceManager_OnResourceChanged(object sender, System.EventArgs e)
    {
        var resourceManager = sender as ResourceManager;

        foreach (var resource in resourceManager.ResourceTypeQuantityAndProductionPerSecond)
        {
            resourceUIPerType[resource.Key].SetText(resource.Value.typeQuantity.ToString());
        }
    }
}
