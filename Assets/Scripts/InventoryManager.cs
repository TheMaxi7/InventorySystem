
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public GameObject inventory;
    public GameObject slotsHolder;
    public Item[] itemsArray;
    public GameObject[] itemSlots;
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

    public void Start()
    {
        itemSlots = new GameObject[slotsHolder.transform.childCount];
        for (int i = 0; i < slotsHolder.transform.childCount; i++)
        {
            itemSlots[i] = slotsHolder.transform.GetChild(i).gameObject;
        }
        itemsArray = new Item[itemSlots.Length];

    }
    public void AddItem(Item item)
    {
        bool itemAdded = false;

        for (int i = 0; i < itemsArray.Length; i++)
        {
            if (itemsArray[i] != null && itemsArray[i] == item)
            {
                itemsArray[i].amount++;
                itemAdded = true;
                break;
            }
        }

        if (!itemAdded)
        {
            for (int i = 0; i < itemsArray.Length; i++)
            {
                if (itemsArray[i] == null)
                {
                    itemsArray[i] = item;
                    break;
                }
            }
        }
        ListItems();
    }


    public void RemoveItem(Item item)
    {
        for (int i = 0; i < itemsArray.Length; i++)
        {
            if (itemsArray[i] == item)
                itemsArray[i] = null;
        }
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
        foreach (Transform slot in ItemContent)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }
        int i = 0;
        while (i < itemsArray.Length)
        {
            if (itemsArray[i] != null)
            {
                var item = itemsArray[i];
                var slot = itemSlots[i];
                GameObject obj = Instantiate(InventoryItem, slot.transform);
                var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var itemAmount = obj.transform.Find("ItemIcon/ItemAmount").GetComponent<TextMeshProUGUI>();

                itemName.text = item.itemName;
                itemIcon.sprite = item.itemIcon;
                itemAmount.text = item.amount.ToString();

                var controller = obj.GetComponent<InventoryItemController>();
                controller.SetItem(item);

                if (item.isSelected)
                {
                    selectedItem = item;
                    ShowSelectedInfo(item);
                }
            }
            i++;

        }
        if (selectedItem == null)
        {
            selectedItem = itemsArray[0];
            ShowSelectedInfo(selectedItem);
        }
    }

    public void ShowSelectedInfo(Item item)
    {
        if (item == null)
        {
            //Debug.Log("Inventario vuoto");
            selectedItemName.text = "No item selected";
            selectedItemIcon.sprite = null;
            selectedItemAmount.text = "";
            selectedItemDescription.text = "";
            return;
        }
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
