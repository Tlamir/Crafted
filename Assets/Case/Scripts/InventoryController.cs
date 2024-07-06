using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public int inventorySize = 10;
    [SerializeField]
    private UIInventoryPage inventoryUI;

    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
    }
}
