using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private TMP_Text quantifyText;

        [SerializeField]
        private Image borderImage;

        public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

        public bool empty { get; private set; } = true;

        public void Awake()
        {
            ResetData();
            Deselect();
        }
        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            empty = true;
        }
        public void Deselect()
        {
            borderImage.gameObject.SetActive(false);
        }
        public void SetData(Sprite sprite, int quantity)
        {
            Debug.Log("SettingData");
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantifyText.text = quantity + "";
            empty = false;
        }

        public void Select()
        {
            borderImage.gameObject.SetActive(true);
        }
        public void OnPointerDown(PointerEventData pointerData)
        {
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
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {

            OnItemDroppedOn?.Invoke(this);
            if (empty)
                return;
        }

        public void OnDrag(PointerEventData eventData)
        {          
        }
    }
}