using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
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

    public void OnbeginDrag()
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }
    public void OnDrop()
    {
        if (empty)
            return;
        OnItemDroppedOn?.Invoke(this);
    }
    public void OnEndDrag()
    {
        if (empty)
            return;
        OnItemEndDrag?.Invoke(this);
    }
    public void OnpointerClick(BaseEventData eventData)
    {
        if (empty)
            return;
        PointerEventData pointerData=(PointerEventData)eventData;
        if (pointerData.button==PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

}
