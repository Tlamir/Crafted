using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventory
{
    public class ChestInventoryController : MonoBehaviour
    {
        [SerializeField] public MouseFollower mouseFollower;
        [SerializeField] public GameObject playerInventoryUI;
        [SerializeField] private UIInventoryPage inventoryUI;
        [SerializeField] private PlayerInteractor playerInteractor;
        [SerializeField] public InventoryScObj inventoryData;
        public int inventorySize = 6;

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            // Ensure the inventory system notifies about every change
            inventoryData.OnInventoryChanged += UpdateInventoryUI;
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            Debug.Log("UI Updated");
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
            if (inventoryItem.IsEmpty) return;

            inventoryUI.CreateDraggedItem(
                inventoryItem.item.ItemImage,
                inventoryItem.quantity,
                itemIndex
            );
        }

        private void HandleSwapItems(int sourceIndex, int targetIndex)
        {
            // Perform the swap and notify the inventory system
            inventoryData.SwapItems(sourceIndex, targetIndex);

            // Ensure UI reflects the updated inventory data
            UpdateInventoryUI(inventoryData.GetCurrentInventoryState());
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
            // Placeholder for item-specific actions
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed && playerInteractor.nearbyChest != null)
            {
                playerInteractor.isInteracting = !playerInteractor.isInteracting;
                playerInteractor.nearbyChest.ToggleChest();

                if (playerInteractor.chestInventoryUI != null)
                {
                    bool isActive = playerInteractor.chestInventoryUI.gameObject.activeSelf;
                    playerInteractor.chestInventoryUI.SetActive(!isActive);

                    if (!isActive)
                    {
                        UpdateInventoryUI(inventoryData.GetCurrentInventoryState());
                    }
                }
            }
        }
    }
}
