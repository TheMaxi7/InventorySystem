using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public GameObject inventory;
    public List<Item> itemsList = new List<Item>();
    public Item selectedItem;

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Image selectedItemIcon;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemAmount;
    public TextMeshProUGUI selectedItemDescription;

    public bool isInventoryOpen;
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
                isInventoryOpen = false;
                Cursor.visible = false;
                Debug.Log("Chiudi inventario");
            }
            else
            {

                inventory.SetActive(true);
                isInventoryOpen = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                ListItems();

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

            var controller = obj.GetComponent<InventoryItemController>();
            controller.SetItem(item);

            if (item.isSelected)
            {
                selectedItem = item;
                ShowSelectedInfo(item);
            }
        }
        if (selectedItem == null)
        {
            if (itemsList.Count != 0)
            {
                selectedItem = itemsList.First();
                ShowSelectedInfo(selectedItem);
            }
            else
            {
                selectedItemName.text = "No item selected";
                selectedItemIcon.sprite = null;
                selectedItemAmount.text = "";
                selectedItemDescription.text = "";
            }

        }

    }

    public void ShowSelectedInfo(Item item)
    {
        selectedItemName.text = item.itemName;
        selectedItemIcon.sprite = item.itemIcon;
        selectedItemAmount.text = item.amount + "";
        selectedItemDescription.text = item.itemDescription;
    }

    public void DeleteItem()
    {
        if (selectedItem.amount <= 1)
        {
            RemoveItem(selectedItem);
            selectedItem = null;
            ListItems();
        }
        else
        {
            selectedItem.amount--;
            ListItems();
            ShowSelectedInfo(selectedItem);
        }

    }

    public void UseItem()
    {
        switch (selectedItem.itemType)
        {
            case Item.ItemType.HealthPotion:
                if (selectedItem.amount <= 1)
                {
                    Player.Instance.IncreaseHealth(selectedItem.value);
                    DeleteItem();
                }
                else
                {
                    Player.Instance.IncreaseHealth(selectedItem.value);
                    selectedItem.amount--;
                    ListItems();
                    ShowSelectedInfo(selectedItem);
                }
                break;
            case Item.ItemType.SpeedPotion:
                if (selectedItem.amount <= 1)
                {
                    Player.Instance.IncreaseMoveSpeed(selectedItem.value);
                    DeleteItem();
                }
                else
                {
                    Player.Instance.IncreaseMoveSpeed(selectedItem.value);
                    selectedItem.amount--;
                    ListItems();
                    ShowSelectedInfo(selectedItem);
                }
                break;
        }

    }

}
