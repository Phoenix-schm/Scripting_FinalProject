using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySubMenu : MonoBehaviour
{
    public PlayerInteraction playerInteraction;

    [HideInInspector] public PickUpItemData pickUpItemData;
    [HideInInspector] public GameObject selectedItem;

    /// <summary>
    /// Removes item from inventory, if item amount is <= 0, Destroy it.
    /// </summary>
    private void RemoveItem()
    {
        if (pickUpItemData != null && selectedItem != null)
        {
            if (selectedItem.TryGetComponent<InventoryItem>(out InventoryItem item))
            {
                item.amount--;
                if (item.amount <= 0)
                {
                    Destroy(selectedItem);
                }

                gameObject.SetActive(false);
                ResetVariables();
            }

        }
    }

    public void DropItem()
    {
        if (selectedItem != null)
        {
            Transform spawnLocation = playerInteraction.spawnInFrontOfPlayer;
            GameObject itemObject = pickUpItemData.prefab;
            Instantiate(itemObject, spawnLocation.position, spawnLocation.rotation);

            RemoveItem();
        }
    }

    public void UseItem()
    {
        bool isItemUsed = false;
        if (pickUpItemData.type == PickUpTypes.Health)
        {
            if (pickUpItemData.prefab.TryGetComponent<HealthPickUp>(out HealthPickUp healthPickUp))     // If it's a valid health item
            {
                isItemUsed = healthPickUp.HealPlayer(playerInteraction);
                PlayerStatusUI statusUI = playerInteraction.playerStatusUI.GetComponent<PlayerStatusUI>();
                statusUI.UpdatePlayerHealthText();
            }
            else
            {
                Debug.Log("<color=red>Health item does not have script HealthPickUp</color=red>");
            }
        }
        else if (pickUpItemData.type == PickUpTypes.Ammo)
        {
            // Add to ammo
        }
        else if (pickUpItemData.type == PickUpTypes.Pizza)
        {
            // heal player
        }

        if (isItemUsed)
        {
            RemoveItem();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public static bool IsBeingDestroy(bool isDestroyed)
    {
        return isDestroyed;
    }

    private void ResetVariables()
    {
        pickUpItemData = null;
        selectedItem = null;
    }
}
