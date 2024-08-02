using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public int amount;
    public bool isSelected = false;
    public ItemType itemType;
    public int value;

    public enum ItemType
    {
        HealthPotion,
        SpeedPotion,
        Bomb,
    }

}
