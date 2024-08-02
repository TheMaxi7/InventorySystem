using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        InventoryManager.Instance.AddItem(GetComponent<ItemController>().item);
        Destroy(gameObject);
    }
}
