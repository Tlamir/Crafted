using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventory
{
    public class ChestInventoryController : MonoBehaviour
    {
        [SerializeField] public GameObject playerInventoryUI;
        public int inventorySize = 6;
        [SerializeField]
        private UIInventoryPage inventoryUI;
        [SerializeField]
        private PlayerInteractor playerInteractor;

        [SerializeField]
        private InventoryScObj inventoryData;


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

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed && playerInteractor.nearbyChest != null)
            {
                playerInteractor.isInteracting = !playerInteractor.isInteracting;
                playerInteractor.nearbyChest.ToggleChest();

                if (playerInteractor.chestInventoryUI != null)
                {
                    if (playerInteractor.chestInventoryUI.gameObject.activeSelf)
                    {
                        playerInteractor.chestInventoryUI.SetActive(false);
                    }
                    else
                    {
                        playerInteractor.chestInventoryUI.SetActive(true);
                        foreach (var item in inventoryData.GetCurrentInventoryState())
                        {
                            inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                        }
                    }

                }
            }
        }

    }
}