using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,IDropHandler,IDragHandler
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TMP_Text quantifyText;

    [SerializeField]
    private Image borderImage;

    public event Action<UIInventoryItem> OnItemClicked,OnItemDroppedOn,OnItemBeginDrag,OnItemEndDrag,OnRightMouseBtnClick;

    private bool empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void Deselect()
    {
        borderImage.gameObject.SetActive(false);

    }

    public void SetData(Sprite sprite,int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite=sprite;
        this.quantifyText.text = quantity + "";
        empty = false;
    }

    public void Select()
    {
        Debug.Log("Selected");
        borderImage.gameObject.SetActive(true);
    }
    public void OnPointerDown(PointerEventData pointerData)
    {
        if (empty)
            return;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
