using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // The item that this draggable component represents
    private Item item;

    // The original parent of the item before dragging
    [HideInInspector] public Transform parentAfterDrag;

    // Index of the closest inventory slot to the dragged item
    private int closestSlot = -1;

    // Index of the item in the inventory array
    private int itemIndex;

    // Called at the start of the script lifecycle
    private void Start()
    {
        // Get the item associated with this draggable component
        item = gameObject.GetComponent<InventoryItemController>().item;
    }

    // Method to set the item associated with this draggable component
    public void SetItem(Item newItem)
    {
        item = newItem;
    }

    // Called when dragging begins
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Find the index of the item in the inventory array
        itemIndex = FindItemIndex(item);

        // Store the current parent for reassigning if necessary
        parentAfterDrag = transform.parent;

        // Set the item's parent to the root to avoid it being clipped by other UI elements
        transform.SetParent(transform.root);

        // Move the item to the front of the UI hierarchy
        transform.SetAsLastSibling();

        // Disable raycast target to allow drag detection
        GetComponent<Image>().raycastTarget = false;
    }

    // Called continuously while dragging
    public void OnDrag(PointerEventData eventData)
    {
        // Update the position of the item to follow the mouse
        transform.position = Input.mousePosition;

        // Find the closest inventory slot to the current mouse position
        closestSlot = GetClosestSlot();
    }

    // Called when dragging ends
    public void OnEndDrag(PointerEventData eventData)
    {
        if (closestSlot >= 0 && closestSlot < InventoryManager.Instance.itemSlots.Length)
        {
            // If the closest slot is empty, move the item there
            if (InventoryManager.Instance.itemsArray[closestSlot] == null)
            {
                transform.SetParent(InventoryManager.Instance.itemSlots[closestSlot].transform);
                transform.localPosition = Vector3.zero;
                InventoryManager.Instance.itemsArray[itemIndex] = null;
                InventoryManager.Instance.itemsArray[closestSlot] = item;
                InventoryManager.Instance.ListItems();
            }
            else
            {
                // If the closest slot is occupied, swap the items
                transform.SetParent(InventoryManager.Instance.itemSlots[closestSlot].transform);
                transform.localPosition = Vector3.zero;
                var holder = InventoryManager.Instance.itemsArray[closestSlot];
                InventoryManager.Instance.itemsArray[itemIndex] = holder;
                InventoryManager.Instance.itemsArray[closestSlot] = item;
                InventoryManager.Instance.ListItems();
            }
        }
        else
        {
            // If no valid slot is found, destroy the dragged item and reset its position
            Destroy(transform.gameObject);
            transform.localPosition = Vector3.zero;
            transform.SetParent(parentAfterDrag);
        }

        // Re-enable raycast target
        GetComponent<Image>().raycastTarget = true;
    }

    /// <summary>
    /// Method to find the closest inventory slot to the current mouse position
    /// </summary> 
    private int GetClosestSlot()
    {
        int closestSlotIndex = -1;
        float closestDistance = Mathf.Infinity;

        // Iterate through all inventory slots
        for (int i = 0; i < InventoryManager.Instance.itemSlots.Length; i++)
        {
            // Calculate the distance between the slot and current the mouse position
            float distance = Vector2.Distance(InventoryManager.Instance.itemSlots[i].transform.position, Input.mousePosition);

            // Update the closest slot if the distance is within the threshold and is the shortest found so far
            if (distance <= 30 && distance < closestDistance)
            {
                closestDistance = distance;
                closestSlotIndex = i;
            }
        }

        return closestSlotIndex;
    }

    /// <summary>
    /// Method to find the index of the given item in the inventory array
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private int FindItemIndex(Item item)
    {
        for (int i = 0; i < InventoryManager.Instance.itemsArray.Length; i++)
        {
            if (item == InventoryManager.Instance.itemsArray[i])
                return i;
        }
        return -1;
    }
}
