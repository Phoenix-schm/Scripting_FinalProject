using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "Player")]
public class PlayerVariables : ScriptableObject
{
    [Header("Player Stats")]
    public int maxHealth;
    public int health;
    public int speed;

    [Header("Player Inventory")]
    public List<PickUpItemData> inventory;
    public Dictionary<PickUpItemData, int> itemData;

    public void LookAtInventory()
    {
        foreach (PickUpItemData item in inventory)
        {
            Debug.Log(item.displayName);
        }
    }
    public void RemoveFromItemStack(PickUpItemData item)
    {
        if (inventory.Contains(item))
        {
            itemData[item] -= 1;
        }
    }
}
