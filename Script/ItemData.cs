using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/New item")]
public class ItemData : ScriptableObject
{
    [Header("Data")]
    public string itemName;
    [TextArea(3, 10)]
    public string description;
    public Sprite visual;
    public GameObject prefab;
    public bool stackable;
    public int maxStack;

    [Header("Effects")]
    public int hpGiven;
    public int speedGiven;
    public float speedDuration;

    [Header("ArmorEffects")]
    public float armorPoint;

    [Header("Shop")]
    public int price;

    [Header("Types")]
    public Type type;
    public ConsumableType consumableType;
    public Rarity rarity;
    public EquipmentType equipmentType;


    public enum Type 
    {
        Resource,
        Consumable,
        Equipment
    }

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Mythical,
        Legendary
    }



    public enum ConsumableType
    {
        None,
        Potion,
        Food
    }

    public enum EquipmentType
    {
        None,
        Head,
        Chest,
        Legs,
        Feet,
    }
}

