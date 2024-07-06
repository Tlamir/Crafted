using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ChestInteractor : MonoBehaviour
{
    public GameObject chestUI; // Assign the chest UI GameObject in the inspector
    public GameObject playerInventoryUI; // Assign the player inventory UI GameObject in the inspector
    private bool isOpen = false;

    public void ToggleChest()
    {
        if (isOpen)
        {
            CloseChest();
        }
        else
        {
            OpenChest();
        }
    }

    public void OpenChest()
    {
        isOpen = true;
        Debug.Log("Chest opened!");
        // Display the chest UI and player inventory UI
        //chestUI.SetActive(true);
        //playerInventoryUI.SetActive(true);
    }

    private void CloseChest()
    {
        isOpen = false;
        Debug.Log("Chest closed!");
        // Hide the chest UI and player inventory UI
        //chestUI.SetActive(false);
        //playerInventoryUI.SetActive(false);
    }
}
