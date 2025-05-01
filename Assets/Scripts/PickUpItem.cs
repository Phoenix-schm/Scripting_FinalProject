using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpItem : MonoBehaviour, IPickUpable, IRemovable
{
    public PickUpItemData pickUpData;
    public int amountInStack;

    public PickUpItem(PickUpItemData sourceData)
    {
        pickUpData = sourceData;
    }

    /// <summary>
    /// Pick up an item and add it's data to the player inventory
    /// </summary>
    /// <param name="player">The player being effected</param>
    public void PickUp(PlayerInteraction player)
    {
        PlayerVariables playerVariables = player.playerVariables;
        PickUpItemData itemData = pickUpData;

        if (!playerVariables.inventory.Contains(itemData))           // if player doesn't have the pick up yet
        {
            playerVariables.inventory.Add(itemData);
            playerVariables.inventoryDictionary.Add(itemData, itemData.prefab);

            int index = playerVariables.inventory.IndexOf(itemData);
            playerVariables.inventory[index].amountInInventory = amountInStack;
        }
        else
        {
            int index = playerVariables.inventory.IndexOf(itemData);
            playerVariables.inventory[index].amountInInventory += amountInStack;
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Remove a whole item from the player inventory
    /// </summary>
    /// <param name="player">The player being effected</param>
    public void RemoveItem(PlayerVariables player, PickUpItemData removeItemData)
    {
        if (player.inventoryDictionary.TryGetValue(removeItemData, out GameObject removeItem))      // for use later on to drop object
        {
            int index = player.inventory.IndexOf(removeItemData);
            player.inventory.Remove(removeItemData);
            player.inventoryDictionary.Remove(removeItemData);
        }
    }

    /// <summary>
    /// Remove an item, one by one, from player inventory
    /// </summary>
    /// <param name="player"></param>
    public void RemoveOneItemFromStack(PlayerVariables player, PickUpItemData removeItemData)
    {
        if (player.inventoryDictionary.TryGetValue(removeItemData, out GameObject removeItem))
        {
            int index = player.inventory.IndexOf(removeItemData);
            player.inventory[index].amountInInventory--;

            if (player.inventory[index].amountInInventory <= 0)
            {
                player.inventory.Remove(removeItemData);
                player.inventoryDictionary.Remove(removeItemData);
            }
        }
    }
}
