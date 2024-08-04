using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface to define interactable objects
interface IInteractable
{
    // Method to be implemented by interactable objects
    void Interact();
}

public class Interactor : MonoBehaviour
{
    // Transform component representing the source of the interaction (e.g., player's viewpoint)
    public Transform InteractorSource;

    // Range within which interactions can occur
    public float interactionRange;

    // UI element to display when an interactable object is in range
    public GameObject pickUpUI;

    void Update()
    {
        // Create a ray from the InteractorSource's position in the forward direction
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

        // Perform a raycast to check if it hits any objects within the interaction range
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactionRange))
        {
            // Check if the hit object has a component that implements the IInteractable interface
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                // If it does, show the interaction UI
                pickUpUI.SetActive(true);

                // If the 'F' key is pressed, call the Interact method on the interactable object
                if (Input.GetKeyDown(KeyCode.F))
                    interactObj.Interact();
            }
            else
            {
                // If no interactable object is hit, hide the interaction UI
                pickUpUI.SetActive(false);
            }
        }
    }
}
