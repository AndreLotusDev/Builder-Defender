using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    private const int LEFT_MOUSE_CODE = 0;

    private BuildingTypeSO buildingTypeSOActual;

    private int quantityOfBuildings = 0;

    private const int ZERO_ANGLE = 0;

    public static BuildingManager Instance { get; private set; }

    public event EventHandler<OnTypeBuildingChangeEventArgs> OnTypeBuildingChange;

    public event EventHandler<OnFoundNewAmountOfResourcesEventArgs> OnFoundNewAmountOfResources;

    public class OnFoundNewAmountOfResourcesEventArgs : EventArgs
    {
        public float quantityOfResourcesThatABuildingCanBring;
    }

    public class OnTypeBuildingChangeEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    void Update()
    {
        var mousePositionInWorld = UtilsClass.GetMouseWorldPosition();

        var isNotOverAnObject = !EventSystem.current.IsPointerOverGameObject();
        var isOverAObject = !isNotOverAnObject;

        var canPlace = CanPlaceBuilding(mousePositionInWorld);

        if (Input.GetMouseButtonDown(LEFT_MOUSE_CODE) && isNotOverAnObject && canPlace)
        {
            InstantiateANewBuildingIfNotNull(mousePositionInWorld);
        }

        if (isOverAObject || !canPlace)
        {
            BuildingGhost.Instance.SetMouseSpriteAsInvalid();
        }
        else
        {
            BuildingGhost.Instance.SetMouseSpriteAsValid();
        }

        if(buildingTypeSOActual != null)
            AttQuantityBonusOfResourceAmount(mousePositionInWorld);
    }

    private void AttQuantityBonusOfResourceAmount(Vector3 mousePositionInWorld)
    {
        BoxCollider2D boxCollider2D = buildingTypeSOActual.prefabBuildingType.GetComponent<BoxCollider2D>();
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(mousePositionInWorld, 5f);

        var numberOfResourcesFound = 0;
        foreach (var collider in collider2Ds)
        {
            BuildingHelper.MakeCalculusAboutHowManyResourceWeHaveNear(ref numberOfResourcesFound, buildingTypeSOActual.resourceGeneratorData.itemsTypesSO, collider);
        }

        var generationBonus = BuildingHelper.CalcBonusGeneration(buildingTypeSOActual, numberOfResourcesFound);
        OnFoundNewAmountOfResources?.Invoke(this, new OnFoundNewAmountOfResourcesEventArgs { quantityOfResourcesThatABuildingCanBring = generationBonus });
    }

    private void InstantiateANewBuildingIfNotNull(Vector3 mousePositionInWorld)
    {
        if (buildingTypeSOActual != null)
        {
            Instantiate(buildingTypeSOActual.prefabBuildingType, mousePositionInWorld, Quaternion.identity);
            quantityOfBuildings++;
        }
    }

    public void SetBuildingActualToUse(BuildingTypeSO buildingToUse)
    {
        buildingTypeSOActual = buildingToUse;

        OnTypeBuildingChange?.Invoke(this, new OnTypeBuildingChangeEventArgs
        {
            activeBuildingType = buildingTypeSOActual
        });
    }

    private bool CanPlaceBuilding(Vector3 actualPositionInWorld)
    {
        const bool DONT_HAVE_ANY_BUILDING_OVER_THE_MOUSE = false;

        if (buildingTypeSOActual != null)
        {
            const bool HAVE_SHOMETHING_BLOCKING_THE_CONSTRUCTION = false;
            const bool DONT_HAVE_SHOMETHING_BLOCKING_THE_CONSTRUCTION = true;

            BoxCollider2D boxCollider2D = buildingTypeSOActual.prefabBuildingType.GetComponent<BoxCollider2D>();
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(actualPositionInWorld + (Vector3)boxCollider2D.offset, boxCollider2D.size, ZERO_ANGLE);

            bool isAreaClear = collider2Ds.Length is 0;

            if (!isAreaClear) 
                return HAVE_SHOMETHING_BLOCKING_THE_CONSTRUCTION;

            collider2Ds = Physics2D.OverlapCircleAll(actualPositionInWorld, buildingTypeSOActual.minRadiusAreaRange);
            var haveSomething = CheckIfHaveABuildingNearby(collider2Ds);

            if (haveSomething) 
                return HAVE_SHOMETHING_BLOCKING_THE_CONSTRUCTION;

            collider2Ds = Physics2D.OverlapCircleAll(actualPositionInWorld, buildingTypeSOActual.maxRadiusAreaRange);
            var haveTheViableDistance = CheckIfHaveTheViableDistanceToConstruct(collider2Ds);

            if (!haveTheViableDistance) 
                return HAVE_SHOMETHING_BLOCKING_THE_CONSTRUCTION; 

            return DONT_HAVE_SHOMETHING_BLOCKING_THE_CONSTRUCTION;
        }

        return DONT_HAVE_ANY_BUILDING_OVER_THE_MOUSE;
    }

    private bool CheckIfHaveABuildingNearby(Collider2D[] collider2Ds)
    {
        foreach (var collider2DFound in collider2Ds)
        {
            BuildingEachInstanceSpecific buildingType = collider2DFound.GetComponent<BuildingEachInstanceSpecific>();
            if (buildingType != null)
                if (buildingType.GetBuildingTypeSO() == buildingTypeSOActual)
                    return true;
        }

        return false;
    }

    private bool CheckIfHaveTheViableDistanceToConstruct(Collider2D[] collider2Ds)
    {
        foreach (var collider2DFound in collider2Ds)
        {
            BuildingEachInstanceSpecific buildingType = collider2DFound.GetComponent<BuildingEachInstanceSpecific>();
            if (buildingType != null)
                if (buildingType.GetBuildingTypeSO() == buildingTypeSOActual)
                    return false;
        }

        return true;
    }
}
