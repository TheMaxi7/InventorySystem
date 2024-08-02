using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        InventoryManager.Instance.AddItem(this.GetComponent<ItemController>().item);
        Destroy(this.gameObject);
    }
}
