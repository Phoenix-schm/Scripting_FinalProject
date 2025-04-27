using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour, IPickUpable, IRemovable
{
    public PickUpItemData pickUpData;

    public void PickUp(PlayerVariables player)
    {
        PickUpItemData item = pickUpData;
        player.inventory.Add(item);

        if (!player.itemData.ContainsKey(item))
        {
            player.itemData.Add(item, 0);
        }
        else
        {
            player.itemData[item] += 1;
        }

        //playerVariables.LookAtInventory();
        Destroy(gameObject);
    }

    public void RemoveItem(PlayerVariables player)
    {
        if (player.inventory.Contains(pickUpData))
        {
            player.itemData[pickUpData] -= 1;

            if (player.itemData[pickUpData] <= 0)
            {
                player.inventory.Remove(pickUpData);
            }
        }
    }
}
