using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemTypeSO")]
public class ItemTypeSO : ScriptableObject
{
    public Sprite sprite;
    public string nameString;
    public float percentageBonus;
}
