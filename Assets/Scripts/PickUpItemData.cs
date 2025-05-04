using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PickUpItem", menuName = "Scriptable Objects/PickUp Item Data")]
public class PickUpItemData : ScriptableObject
{
    public string itemId;
    public string displayName;

    [Header("UI Elements")]
    [Tooltip("Image that will appear in the inventory")]
    public Sprite icon;

    [Tooltip("Prefab that'll be spawned in when dropped.")]
    public GameObject prefab;

    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Gameplay Elements")]
    public PickUpTypes type;
    public bool stackable = true;
}
    
public enum PickUpTypes 
{ 
    Health,
    Ammo, 
    Ingredient,
    Pizza
}
