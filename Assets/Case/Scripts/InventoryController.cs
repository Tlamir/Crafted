using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] 
        public GameObject playerInventoryUI;
        [SerializeField]
        private UIInventoryPage inventoryUI;
        [SerializeField]
        public InventoryScObj inventoryData;
        public int inventorySize = 10;

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }
        private void PrepareInventoryData()
        {
            //inventoryData.Initialize();
            inventoryData.OnInventoryChanged += UpdateInventoryUI;          
        }
        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }
        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }
        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity,itemIndex);
        }
        private void HandleSwapItems(int arg1, int arg2)
        {
            inventoryData.SwapItems(arg1, arg2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemScObj item = inventoryItem.item;
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
        }
        private void HandleItemActionRequest(int itemIndex)
        {

        }
        public void OnInventory(InputAction.CallbackContext context)
        {
            if (playerInventoryUI != null)
            {
                if (playerInventoryUI.gameObject.activeSelf)
                {
                    playerInventoryUI.SetActive(false);
                    inventoryUI.ResetSelection();  
                }
                else
                {
                    playerInventoryUI.SetActive(true);
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }

                }

            }
        }
    }
}