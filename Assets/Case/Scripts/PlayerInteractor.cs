using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    private ChestInteractor nearbyChest;
    private CharacterController characterController;
    private CharacterMovement characterMovement;
    private bool isInteracting = false;

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
