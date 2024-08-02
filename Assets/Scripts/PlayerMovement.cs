using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public Camera playerCamera;
    private Vector3 moveDirection;
    private float rotationX;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxLookUpAngle = 90f;
    [SerializeField] private float maxLookDownAngle = -90f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!InventoryManager.Instance.isInventoryOpen)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            moveDirection = (forward * Input.GetAxis("Vertical")) + (right * Input.GetAxis("Horizontal"));

            controller.Move(moveDirection * Time.deltaTime * Player.Instance.moveSpeed);
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, maxLookDownAngle, maxLookUpAngle);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, mouseX * rotationSpeed, 0);
        }

    }
}

