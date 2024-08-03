
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public Item item;

    public void SetItem(Item newItem)
    {
        item = newItem;
    }

    public void SelectItem()
    {
        for (int i = 0; i < InventoryManager.Instance.itemsArray.Length; i++)
        {
            if (InventoryManager.Instance.itemsArray[i] != null && InventoryManager.Instance.itemsArray[i].isSelected)
                InventoryManager.Instance.itemsArray[i].isSelected = false;

            if (InventoryManager.Instance.itemsArray[i] == item)
                InventoryManager.Instance.itemsArray[i].isSelected = true;
        }
        InventoryManager.Instance.selectedItem = item;
        InventoryManager.Instance.ListItems();
    }


}
