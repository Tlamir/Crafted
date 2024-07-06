using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Sprite image,image2;
    public int quanitity;
    public string title,description;
    private int currentlyDraggedItemIndex=-1;

    private void Awake()
    {
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
        listOfUIItems[0].SetData(image, quanitity);
        listOfUIItems[1].SetData(image2, quanitity);
    }
    public void InitializeInventoryUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem uiItem=Instantiate(itemPrefab,Vector3.zero,Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(UIInventoryItem item)
    {
        
    }

    private void HandleEndDrag(UIInventoryItem item)
    {
        Debug.Log("EndDrag");
        mouseFollower.Toggle(false);
    }

    private void HandleSwap(UIInventoryItem item)
    {
        int index = listOfUIItems.IndexOf(item);
        if (index < -1)
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
            return;
        }
        listOfUIItems[currentlyDraggedItemIndex].SetData(index == 0 ? image : image2, quanitity);
        listOfUIItems[index].SetData(currentlyDraggedItemIndex == 0 ? image : image2, quanitity);
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;


    }

    private void HandleBeginDrag(UIInventoryItem item)
    {
        int index=listOfUIItems.IndexOf(item);
        if (index < -1)
            return;
        currentlyDraggedItemIndex = index;
        mouseFollower.Toggle(true);
        mouseFollower.SetData(index==0? image: image2, quanitity);
    }

    private void HandleItemSelection(UIInventoryItem item)
    {
        Debug.Log(item.name);
        itemDescription.SetDescription(image, title, description);
        listOfUIItems[0].Select();
    }
}
