using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    // The item that this controller is managing
    public Item item;

    // Method to set the item for this controller
    public void SetItem(Item newItem)
    {
        item = newItem;
    }

    // Method to select this item in the inventory
    public void SelectItem()
    {
        // Iterate through all items in the inventory
        for (int i = 0; i < InventoryManager.Instance.itemsArray.Length; i++)
        {
            // If the item is not null and is currently selected, deselect it
            if (InventoryManager.Instance.itemsArray[i] != null && InventoryManager.Instance.itemsArray[i].isSelected)
                InventoryManager.Instance.itemsArray[i].isSelected = false;

            // If the item matches the item controlled by this instance, select it
            if (InventoryManager.Instance.itemsArray[i] == item)
                InventoryManager.Instance.itemsArray[i].isSelected = true;
        }

        // Update the selected item in the inventory manager
        InventoryManager.Instance.selectedItem = item;

        // Update the inventory list display
        InventoryManager.Instance.ListItems();
    }
}
