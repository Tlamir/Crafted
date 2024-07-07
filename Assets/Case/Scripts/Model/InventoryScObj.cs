using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventoryScObj : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int,InventoryItem>> OnInventoryChanged;

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }
        public void AddItem(ItemScObj item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = new InventoryItem
                    {
                        item = item,
                        quantity = quantity
                    };
                    return;
                }
            }
        }
        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue =
                new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.item,item.quantity);
        }

        public void SwapItems(int arg1, int arg2)
        {
            InventoryItem item1 = inventoryItems[arg1];
            inventoryItems[arg1] = inventoryItems[arg2];
            inventoryItems[arg2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryChanged?.Invoke(GetCurrentInventoryState());
        }
    }
    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemScObj item;

        public bool IsEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,

            };
        }

        public static InventoryItem GetEmptyItem()
            => new InventoryItem
            {
                item = null,
                quantity = 0,
            };
    }
}
