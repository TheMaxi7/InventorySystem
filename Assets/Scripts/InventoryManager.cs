using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public GameObject inventory;
    public List<Item> itemsList = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(Item item)
    {
        if (itemsList.Contains(item))
            item.amount++;
        else
            itemsList.Add(item);
    }

    public void RemoveItem(Item item)
    {
        itemsList.Remove(item);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
                Debug.Log("Chiudi inventario");
            }
            else
            {
                inventory.SetActive(true);
                InventoryManager.Instance.ListItems();

                Debug.Log("Apri inventario");
            }

        }

    }
    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in itemsList)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemAmount = obj.transform.Find("ItemIcon/ItemAmount").GetComponent<TextMeshProUGUI>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.itemIcon;
            itemAmount.text = item.amount + "";
        }

    }
}
