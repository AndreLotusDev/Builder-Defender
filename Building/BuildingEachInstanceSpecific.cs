using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEachInstanceSpecific : MonoBehaviour
{
    [SerializeField] private BuildingTypeSO buildingTypeSO;
    public BuildingTypeSO GetBuildingTypeSO() => buildingTypeSO;

    public float HowManyResourcesThisBuildGenerate { get; private set; } = 0;
    public ItemTypeSO ItemThatThisBuildingProduce { get; private set; }

    const int GENERATE_NOTHING_FROM_THIS_BUILDING = 0;

    void Start()
    {
        BuildTheHousesOfResource();
    }

    private void BuildTheHousesOfResource()
    {
        var howMuchResourceHasArroundMe = Physics2D.OverlapCircleAll(transform.position, 5f);
        var numberOfResourcesFound = 0;
        ItemThatThisBuildingProduce = buildingTypeSO.resourceGeneratorData.itemsTypesSO;

        foreach (var collidedObjectResource in howMuchResourceHasArroundMe)
        {
            BuildingHelper.MakeCalculusAboutHowManyResourceWeHaveNear(ref numberOfResourcesFound, ItemThatThisBuildingProduce, collidedObjectResource);
        }

        var foundNothing = numberOfResourcesFound is 0;
        if (foundNothing)
            FoundNothing(numberOfResourcesFound);

        var foundJustOneResource = numberOfResourcesFound is 1;
        if (foundJustOneResource)
            FoundJustOneResource(numberOfResourcesFound);

        var foundMoreThanOneResource = numberOfResourcesFound > 1;
        if (foundMoreThanOneResource)
            FoundMoreThanOneResource(numberOfResourcesFound);
    }

    private void FoundNothing(int numberOfResourcesFound)
    {
        HowManyResourcesThisBuildGenerate = BuildingHelper.CalcBonusGeneration(buildingTypeSO, numberOfResourcesFound); ;

        ResourceManager.Instace.IncreaseAmountProductionPerSecond(buildingTypeSO.resourceGeneratorData.itemsTypesSO.nameString,
        HowManyResourcesThisBuildGenerate);
    }

    private void FoundJustOneResource(int numberOfResourcesFound)
    {
        HowManyResourcesThisBuildGenerate = BuildingHelper.CalcBonusGeneration(buildingTypeSO, numberOfResourcesFound);

        ResourceManager.Instace.IncreaseAmountProductionPerSecond(buildingTypeSO.resourceGeneratorData.itemsTypesSO.nameString,
        HowManyResourcesThisBuildGenerate);
    }

    private void FoundMoreThanOneResource(int numberOfResourcesFound)
    {
        HowManyResourcesThisBuildGenerate = BuildingHelper.CalcBonusGeneration(buildingTypeSO, numberOfResourcesFound);

        ResourceManager.Instace.IncreaseAmountProductionPerSecond(buildingTypeSO.resourceGeneratorData.itemsTypesSO.nameString,
        HowManyResourcesThisBuildGenerate);
    }
}
