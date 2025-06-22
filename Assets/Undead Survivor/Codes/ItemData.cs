using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemID;
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;
    [Header("# Level Data")]
    public float baseDamage;
    public float baseCount;
    public float[] damages;
    public float[] counts;
    [Header("# Weapon")]
    public GameObject projectile;
    public Sprite hand;
}
