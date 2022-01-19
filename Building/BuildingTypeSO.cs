using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypeSO")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefabBuildingType;
    public Sprite sprite;

    public ResourceGeneratorData resourceGeneratorData;

    public float minRadiusAreaRange;
    public float maxRadiusAreaRange;

    public List<BuildingCostsSO> buildingCosts;
}
