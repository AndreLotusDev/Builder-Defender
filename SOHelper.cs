using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SOHelper 
{
   public static BuildingTypeListSO GetBuildingTypeListSO() => Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

   public static ItemTypeListSO GetTypemListSO() => Resources.Load<ItemTypeListSO>(typeof(ItemTypeListSO).Name);
}
