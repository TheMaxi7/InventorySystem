using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Reference to the CharacterController component
    [SerializeField] private CharacterController controller;

    // Reference to the players camera
    [SerializeField] private Camera playerCamera;

    // Direction in which the character moves
    private Vector3 moveDirection;

    // Variable to store the cameras vertical rotation
    private float rotationX;

    // Speed at which the character rotates
    [SerializeField] private float rotationSpeed;

    // Maximum upward angle the camera can look
    [SerializeField] private float maxLookUpAngle = 90f;

    // Maximum downward angle the camera can look
    [SerializeField] private float maxLookDownAngle = -90f;

    void Start()
    {
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Check if the inventory is not open
        if (!InventoryManager.Instance.isInventoryOpen)
        {
            // Get the forward and right directions relative to the characters rotation
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Calculate the movement direction based on player input
            moveDirection = (forward * Input.GetAxis("Vertical")) + (right * Input.GetAxis("Horizontal"));

            // Move the character controller in the calculated direction
            controller.Move(moveDirection * Time.deltaTime * Player.Instance.moveSpeed);

            // Get the mouse input for camera rotation
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

            // Update the vertical rotation and clamp it within the specified angles
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, maxLookDownAngle, maxLookUpAngle);

            // Rotate the camera vertically
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            // Rotate the character horizontally
            transform.rotation *= Quaternion.Euler(0, mouseX * rotationSpeed, 0);
        }
    }
}
