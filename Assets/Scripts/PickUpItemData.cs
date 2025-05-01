using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "PickUpItem", menuName = "Scriptable Objects/PickUp Item Data")]
public class PickUpItemData : ScriptableObject
{
    public string itemId;
    public string displayName;

    [Tooltip("The sprite shown in the inventory")]
    public Sprite icon;                                 
    public GameObject prefab;
    public Vector2Int range = new Vector2Int(5, 4);
    public enum PickUpTypes { Health, Ammo, Ingredient}
    public PickUpTypes type;

    public int healAmount;
    public int ammoAmount;

    public int amountInInventory;
}
