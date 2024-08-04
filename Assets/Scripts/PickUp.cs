using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Method called when the player interacts with the pickup item
    /// </summary>
    public void Interact()
    {
        // Adds the item to the players inventory using the InventoryManager singleton instance
        InventoryManager.Instance.AddItem(GetComponent<ItemController>().item);

        // Destroys the pickup item game object after it has been picked up
        Destroy(gameObject);
    }
}
