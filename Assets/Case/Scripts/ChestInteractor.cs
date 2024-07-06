using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestInteractor : MonoBehaviour
{
    private bool isOpen = false;
    [SerializeField] private Transform chestLid;
    // Rotation settings
    public Vector3 openRotation = new Vector3(30, 0, 0); 
    public float openDuration = 1f; // Duration to open the chest
    public float closeDuration = 1f; // Duration to close the chest

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isAnimating = false;
    private float animationTime = 0f;
    private float animationDuration = 0f;

    private void Awake()
    {
        if (chestLid != null)
        {
            initialRotation = chestLid.localRotation;
            targetRotation = Quaternion.Euler(openRotation);
        }
    }

    private void Update()
    {
        if (isAnimating)
        {
            AnimateLid();
        }
    }

    public void ToggleChest()
    {
        isOpen = !isOpen;
        isAnimating = true;
        animationTime = 0f;
        animationDuration = isOpen ? openDuration : closeDuration;

        if (isOpen)
        {
            OpenChest();
        }
        else
        {
            CloseChest();
        }
    }

    public void OpenChest()
    {
        Debug.Log("Chest opened!");
    }

    private void CloseChest()
    {
        Debug.Log("Chest closed!");
    }

    private void AnimateLid()
    {
        animationTime += Time.deltaTime;
        float t = Mathf.Clamp01(animationTime / animationDuration);
        Quaternion targetRot = isOpen ? targetRotation : initialRotation;
        chestLid.localRotation = Quaternion.Lerp(chestLid.localRotation, targetRot, t);

        if (t >= 1f)
        {
            chestLid.localRotation = targetRot;
            isAnimating = false;
        }
    }
}
