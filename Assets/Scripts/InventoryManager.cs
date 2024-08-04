using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Singleton instance of the InventoryManager
    public static InventoryManager Instance;

    [Header("Inventory UI")]
    public GameObject inventory;          // The inventory UI panel
    public GameObject slotsHolder;        // The parent object that holds the slot UI elements
    public Transform ItemContent;         // The content area where inventory items are listed
    public GameObject InventoryItem;      // The prefab for displaying an inventory item

    [Header("Selected Item UI")]
    [SerializeField] private Image selectedItemIcon;        // The icon of the selected item
    private Sprite defaultSelectedItemSprite;
    [SerializeField] private TextMeshProUGUI selectedItemName;       // The name of the selected item
    [SerializeField] private TextMeshProUGUI selectedItemAmount;     // The amount of the selected item
    [SerializeField] private TextMeshProUGUI selectedItemDescription; // The description of the selected item

    [HideInInspector] public Item[] itemsArray;             // Array to hold items in the inventory
    [HideInInspector] public GameObject[] itemSlots;        // Array to hold slot UI elements
    [HideInInspector] public Item selectedItem;             // The currently selected item

    [HideInInspector] public bool isInventoryOpen;          // Flag to check if the inventory is open

    private void Awake()
    {
        // Set the singleton instance
        Instance = this;
    }

    public void Start()
    {
        defaultSelectedItemSprite = selectedItemIcon.sprite;
        // Initialize itemSlots array with the children of slotsHolder
        itemSlots = new GameObject[slotsHolder.transform.childCount];
        for (int i = 0; i < slotsHolder.transform.childCount; i++)
        {
            itemSlots[i] = slotsHolder.transform.GetChild(i).gameObject;
        }
        // Initialize the itemsArray with the length of itemSlots
        itemsArray = new Item[itemSlots.Length];
    }

    /// <summary>
    /// Method to add an item to the inventory
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        bool itemAdded = false;

        // Check if the item already exists in the inventory, increment its amount if it does
        for (int i = 0; i < itemsArray.Length; i++)
        {
            if (itemsArray[i] != null && itemsArray[i] == item)
            {
                itemsArray[i].amount++;
                itemAdded = true;
                break;
            }
        }

        // If the item was not added, find an empty slot and add it there
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
        // Refresh the inventory UI
        ListItems();
    }

    /// <summary>
    /// Method to remove an item from the inventory
    /// </summary>
    /// <param name="item"></param>
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
        // Toggle inventory visibility when 'E' key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
                isInventoryOpen = false;
                Cursor.visible = false;
                Debug.Log("Inventory closed");
            }
            else
            {
                inventory.SetActive(true);
                isInventoryOpen = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                ListItems();
                Debug.Log("Inventory opened");
            }
        }
    }

    /// <summary>
    /// Method to list items in the inventory UI
    /// </summary>
    public void ListItems()
    {
        // Clear existing items in the ItemContent otherwise they duplicate each time inventory is opened
        foreach (Transform slot in ItemContent)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }

        // Populate the inventory UI with items
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

        // If no item is selected, select the first item
        if (selectedItem == null && itemsArray.Length > 0)
        {
            selectedItem = itemsArray[0];
            ShowSelectedInfo(selectedItem);
        }
    }

    /// <summary>
    /// Method to show the selected item information in the UI
    /// </summary>
    /// <param name="item"></param>
    public void ShowSelectedInfo(Item item)
    {
        if (item == null)
        {
            selectedItemName.text = "No item selected";
            selectedItemIcon.sprite = defaultSelectedItemSprite;
            selectedItemAmount.text = "";
            selectedItemDescription.text = "";
            return;
        }
        selectedItemName.text = item.itemName;
        selectedItemIcon.sprite = item.itemIcon;
        selectedItemAmount.text = item.amount.ToString();
        selectedItemDescription.text = item.itemDescription;
    }

    /// <summary>
    /// Method to delete the selected item
    /// </summary>
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

    /// <summary>
    /// Method to use the selected item
    /// </summary>
    public void UseItem()
    {
        switch (selectedItem.itemType)
        {
            case Item.ItemType.HealthPotion:
                Player.Instance.IncreaseHealth(selectedItem.value);
                break;
            case Item.ItemType.SpeedPotion:
                Player.Instance.IncreaseMoveSpeed(selectedItem.value);
                break;
        }

        if (selectedItem.amount <= 1)
        {
            DeleteItem();
        }
        else
        {
            selectedItem.amount--;
            ListItems();
            ShowSelectedInfo(selectedItem);
        }
    }
}
