using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;
        [SerializeField]
        private UIInventoryDescription itemDescription;
        [SerializeField]
        private MouseFollower mouseFollower;

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();


        private int currentlyDraggedItemIndex = -1;

        public Action<int> OnItemActionRequested { get; internal set; }

        public event Action<int> OnDescriptionRequested, OnItemRequested, OnStartDragging;
        public event Action<int, int> OnSwapItems;
        public Sprite defaultSprite;

        private void Awake()
        {
            mouseFollower.Toggle(false);
            ResetSelection();
        }

        private void OnEnable()
        {
            mouseFollower.Toggle(false);
            itemDescription.ResetDescription();
        }

        private void OnDisable()
        {
            ResetDraggedItem();
        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();

        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listOfUIItems)
            {
                item.Deselect();
            }
        }

        public void InitializeInventoryUI(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }

        }

        public void UpdateData(int itemIndex,
                Sprite itemImage, int itemQuantity)
        {
            Debug.Log("Update data pls");
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleShowItemActions(UIInventoryItem item)
        {

        }

        private void HandleEndDrag(UIInventoryItem item)
        {
            Debug.Log("EndDrag");
            ResetDraggedItem();

        }

        private void HandleSwap(UIInventoryItem item)
        {
            int index = listOfUIItems.IndexOf(item);
            if (index < -1)
            {
                return;
            }           
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
            HandleItemSelection(item);


        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem item)
        {
            int index = listOfUIItems.IndexOf(item);
            if (index < -1)
                return;
            currentlyDraggedItemIndex = index;
            currentlyDraggedItemIndex = index;
            HandleItemSelection(item);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UIInventoryItem item)
        {
            int index = listOfUIItems.IndexOf(item);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            listOfUIItems[itemIndex].Select();
        }

        internal void ResetAllItems()
        {
            foreach(var item in listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}