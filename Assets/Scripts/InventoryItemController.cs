using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    private Item item;

    public void SetItem(Item newItem)
    {
        item = newItem;
    }

    public void SelectItem()
    {
        foreach (Item item in InventoryManager.Instance.itemsList)
        {
            if (item.isSelected)
            {
                item.isSelected = false;
            }
        }
        item.isSelected = true;
        InventoryManager.Instance.selectedItem = item;
        InventoryManager.Instance.ListItems();
    }


}
