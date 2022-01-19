using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingCostSO")]
public class BuildingCostsSO : ScriptableObject
{
    public EProductType productType;
    public int quantityOfThisResource;
}
