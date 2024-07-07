using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private UIInventoryItem item;

    [SerializeField]
    public int DraggedItemIndex;

    [SerializeField]
    public InventoryScObj chestInventory;
    [SerializeField]
    public InventoryScObj PlayerInventory;

    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();
    }

    public void SetData(Sprite sprite, int quantity,int index)
    {
        item.SetData(sprite, quantity);
        UpdateDragIndex(index);
    }

    private void UpdateDragIndex(int index)
    {
        DraggedItemIndex = index;
        chestInventory.DraggedItemIndex = index;
        PlayerInventory.DraggedItemIndex = index;
    }

    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out position
                );
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}