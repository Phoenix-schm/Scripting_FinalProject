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
    public Dictionary<PickUpItemData, GameObject> inventoryDictionary;

    public static void HealthUpdate(PickUpItem healthItem)
    {
        if (healthItem != null)
        {
        }
    }
}
