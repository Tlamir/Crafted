using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChestInventoriesScObj : ScriptableObject
{
   public List<InventoryScObj> ChestInventories = new List<InventoryScObj>();
}
