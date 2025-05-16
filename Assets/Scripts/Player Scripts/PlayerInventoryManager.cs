using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject subMenu;
    public PlayerInteraction playerInteraction;
    public CraftingManager pizzaCrafting;

    [HideInInspector] public PickUpItemData pickUpItemData = null;
    [HideInInspector] public GameObject selectedItem = null;

    public bool AddItem(PickUpItemData item)
    {
        bool hasBeenPlaced = IsAlreadyInInventory(item);

        if (!hasBeenPlaced)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null)                                 // The slot is empty
                {
                    itemInSlot = SpawnItemInSlot(item, slot);

                    if (item.type == PickUpTypes.Ingredient)
                    {
                        pizzaCrafting.UpdatePizzaOvenContent(itemInSlot);
                    }

                    hasBeenPlaced = true;
                    break;
                }
            }
        }

        return hasBeenPlaced;
    }


    /// <summary>
    /// Checks to see if the item already exists in the player inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private bool IsAlreadyInInventory(PickUpItemData item)
    {
        bool addedToInventoryStack = false;
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            bool isInInventoryAndStackable = itemInSlot != null && itemInSlot.itemData == item && 
                                             itemInSlot.amount < itemInSlot.maxAmount && item.stackable == true;
            if (isInInventoryAndStackable)
            {
                itemInSlot.amount++;
                itemInSlot.RefreshCount();

                if (item.type == PickUpTypes.Ingredient)
                {
                    pizzaCrafting.UpdatePizzaOvenContent(itemInSlot);
                }

                addedToInventoryStack = true;
                break;
            }
        }
        return addedToInventoryStack;
    }

    private InventoryItem SpawnItemInSlot(PickUpItemData item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        if (newItem.TryGetComponent<InventoryItem>(out InventoryItem inventoryItem))
        {
            inventoryItem.InitializeItem(item);
        }

        return inventoryItem;
    }

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

                subMenu.SetActive(false);
                ResetVariables();
            }
        }
    }
    
    public void RemoveItemAmount(int amount, Ingredient ingredient)
    {
        // go through the inventory
        // remove the needed ingredients by amount
        

        // TODO: Add pizza to inventory function. button will make it work
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
                isItemUsed = healthPickUp.HealPlayer();
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
            subMenu.SetActive(false);
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
