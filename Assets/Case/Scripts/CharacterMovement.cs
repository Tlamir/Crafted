using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;
    public float firstPersonLookSpeed = 2f;
    public float thirdPersonLookSpeed = 2f;
    public float verticalLookLimit = 80f; // Limit for looking up and down in degrees
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isFirstPerson = true;
    private bool isInteracting = false;

    private float verticalRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        SwitchToFirstPerson();
    }

    void Update()
    {
        if (!isInteracting)
        {
            Move();
            Look();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnSwitchCamera(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isFirstPerson)
            {
                SwitchToThirdPerson();
            }
            else
            {
                SwitchToFirstPerson();
            }
        }
    }

    private void Move()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);
    }

    private void Look()
    {
        float mouseX = lookInput.x;
        float mouseY = lookInput.y;

        if (isFirstPerson)
        {
            // Rotate the character horizontally
            transform.Rotate(Vector3.up * mouseX * firstPersonLookSpeed);

            // Rotate the first person camera vertically with limit
            verticalRotation -= mouseY * firstPersonLookSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);
            firstPersonCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
        else
        {
            // Rotate the character horizontally
            transform.Rotate(Vector3.up * mouseX * thirdPersonLookSpeed);

            // Rotate the third person camera vertically with limit
            verticalRotation -= mouseY * thirdPersonLookSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);
            thirdPersonCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }

    public void SetInteracting(bool interacting)
    {
        isInteracting = interacting;
    }

    private void SwitchToFirstPerson()
    {
        isFirstPerson = true;
        firstPersonCamera.enabled = true;
        thirdPersonCamera.enabled = false;
    }

    private void SwitchToThirdPerson()
    {
        isFirstPerson = false;
        firstPersonCamera.enabled = false;
        thirdPersonCamera.enabled = true;
    }
}
