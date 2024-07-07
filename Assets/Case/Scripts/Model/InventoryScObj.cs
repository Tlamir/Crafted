using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventoryScObj : ScriptableObject
    {
        [SerializeField]
        public InventoryScObj otherInventory;
        [SerializeField]
        public int DraggedItemIndex = -1;
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryChanged;

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
                    InformAboutChange(); // Inform about change
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
            AddItem(item.item, item.quantity);
        }

        public void SwapItems(int arg1, int arg2)
        {
            // If item is dragged from another inventory
            if (arg1 == -1)
            {
                if (DraggedItemIndex > -1)
                {
                    SwapItemsBetweenInventories(otherInventory, DraggedItemIndex, this, arg2);
                    DraggedItemIndex = -1; // Reset dragged item index
                }
                return;
            }
            
            Debug.Log("Item moving in self inventory");
            InventoryItem item1 = inventoryItems[arg1];
            inventoryItems[arg1] = inventoryItems[arg2];
            inventoryItems[arg2] = item1;
            InformAboutChange(); // Inform about change
        }

        public void SwapItemsBetweenInventories(InventoryScObj inventory1, int index1, InventoryScObj inventory2, int index2)
        {
            InventoryItem tempItem = inventory1.GetItemAt(index1);
            inventory1.inventoryItems[index1] = inventory2.GetItemAt(index2);
            inventory2.inventoryItems[index2] = tempItem;

            inventory1.InformAboutChange(); // Inform both inventories about the change
            inventory2.InformAboutChange();
        }

        private void InformAboutChange()
        {
            Debug.Log(this.name);
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
