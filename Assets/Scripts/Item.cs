
using UnityEngine;

// This attribute allows to create instances of this ScriptableObject
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create")]
public class Item : ScriptableObject
{
    // Unique identifier for the item
    public int id;

    // Name of the item
    public string itemName;

    // Description of the item
    public string itemDescription;

    // Icon representing the item
    public Sprite itemIcon;

    // Amount of the item
    public int amount;

    // Flag to check if the item is currently selected
    public bool isSelected = false;

    // Type of the item 
    public ItemType itemType;

    // Amount of stat increase
    public int value;

    // Enumeration of possible item types
    public enum ItemType
    {
        HealthPotion,
        SpeedPotion,

    }
}

