using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingHelper 
{
    public static void MakeCalculusAboutHowManyResourceWeHaveNear(ref int numberOfResourcesFound, ItemTypeSO typeResourceToCheck, Collider2D collidedObjectResource)
    {
        var resourceTypeCollided = collidedObjectResource.GetComponent<ResourceType>();

        var resourceFoundIsTheSameAsTheBuilding = resourceTypeCollided?.GetTypeSO() == typeResourceToCheck;
        if (resourceFoundIsTheSameAsTheBuilding)
        {
            numberOfResourcesFound++;
        }

    }

    public static float CalcBonusGeneration(BuildingTypeSO buildingTypeSO, int numberOfResourcesFound)
    {
        if(numberOfResourcesFound is 1)
        {
            var generationNaturallyPerSecond = buildingTypeSO.resourceGeneratorData.quantityOfResourcesGeneratedPerSecond;
            return generationNaturallyPerSecond;
        }

        if (numberOfResourcesFound > 1)
        {
            var generationNaturallyPerSecond = buildingTypeSO.resourceGeneratorData.quantityOfResourcesGeneratedPerSecond;
            var increasePlusPerResourceFound = buildingTypeSO.resourceGeneratorData.itemsTypesSO.percentageBonus;

            var totalGenerated = (generationNaturallyPerSecond) + (generationNaturallyPerSecond * numberOfResourcesFound * increasePlusPerResourceFound);
            return totalGenerated;
        }

        return 0;
    }
}
