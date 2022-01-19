using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceType : MonoBehaviour {
    [SerializeField] private ItemTypeSO itemTypeThatCanBeExtracted;

    public ItemTypeSO GetTypeSO() => itemTypeThatCanBeExtracted;
}



