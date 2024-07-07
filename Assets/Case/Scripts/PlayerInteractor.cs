using Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField]public GameObject playerInventoryUI;
    [SerializeField]public GameObject chestInventoryUI;
    [SerializeField]public GameObject ChestInteractionText;

    private CharacterController characterController;
    private CharacterMovement characterMovement;

    public ChestInteractor nearbyChest;
    public bool isInteracting = false;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        characterMovement = GetComponent<CharacterMovement>();
    }
    void Update()
    {
        characterController.enabled = !isInteracting;
        characterMovement.SetInteracting(isInteracting);
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && nearbyChest != null)
        {
            isInteracting = !isInteracting;
            nearbyChest.ToggleChest();
            if (chestInventoryUI != null)
            {
                if (chestInventoryUI.gameObject.activeSelf)
                {
                    chestInventoryUI.SetActive(false);
                }
                else
                {
                    chestInventoryUI.SetActive(true);
                    //chestInventoryController.GetComponent<InventoryController>().RefreshDataUI();
                }
            }
        }
    }  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chest"))
        {
            nearbyChest = other.GetComponent<ChestInteractor>();
            Debug.Log("Chest detected.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chest"))
        {
            nearbyChest = null;
            Debug.Log("Chest out of range.");
        }
    }
}
