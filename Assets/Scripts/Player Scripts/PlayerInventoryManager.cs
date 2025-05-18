using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject craftingManagerObject;
    public GameObject subMenu;

    public PlayerInteraction playerInteraction;
    //private CraftingManager craftingManager;

    [HideInInspector] public PickUpItemData pickUpItemData = null;
    [HideInInspector] public GameObject selectedItem = null;

    private void Awake()
    {
        //craftingManager = craftingManagerObject.GetComponentInChildren<CraftingManager>();
        //craftingManager.playerInventoryManager = this;
    }

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

                    //if (item.type == PickUpTypes.Ingredient)
                    //{
                    //    craftingManager.UpdatePizzaOvenContent(itemInSlot);
                    //}

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

                //if (item.type == PickUpTypes.Ingredient)
                //{
                //    foreach (PizzaOvenSlot pizzaOvenSlot in craftingManager.pizzaSlots)
                //    {
                //        pizzaOvenSlot.UpdateSlot(itemInSlot);
                //    }
                //}

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
    /// Used inCanvasRaycaster
    /// </summary>
    private void RemoveItem()
    {
        if (pickUpItemData != null && selectedItem != null)
        {
            if (selectedItem.TryGetComponent<InventoryItem>(out InventoryItem itemInSlot))
            {
                //if (pickUpItemData.type == PickUpTypes.Ingredient)
                //{
                //    UpdatePizzaSlots(itemInSlot);
                //}

                RemoveItemFromSlot(itemInSlot);

                subMenu.SetActive(false);
                ResetVariables();
            }
        }
        else
        {
            return;
        }
    }

    public bool RemoveItem(PickUpItemData item)
    {
        bool hasBeenFound = false;
        foreach (InventorySlot slot in  inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot.itemData == item)
            {
                RemoveItemFromSlot(itemInSlot);
                hasBeenFound = true;
                break;
            }
        }

        return hasBeenFound;
    }

    private void RemoveItemFromSlot(InventoryItem inventoryItem)
    {
        inventoryItem.amount--;
        inventoryItem.RefreshCount();

        if (inventoryItem.amount <= 0)
        { Destroy(inventoryItem); }
    }
    
    public void RemoveItemAmount(InventoryItem itemAccess, int amountToRemove)
    {
        itemAccess.amount -= amountToRemove;
        itemAccess.RefreshCount();

        //UpdatePizzaSlots(itemAccess);

        if (itemAccess.amount <= 0)
        {
            Destroy(itemAccess.gameObject);
        }
    }

    public void DropItem()
    {
        if (selectedItem != null && pickUpItemData != null)
        {
            Transform spawnLocation = playerInteraction.spawnInFrontOfPlayer;
            GameObject itemObject = pickUpItemData.prefab;
            Instantiate(itemObject, spawnLocation.position, spawnLocation.rotation);

            RemoveItem();
        }
        else
        {
            Debug.Log("Something is null");
        }
    }

    /// <summary>
    /// Used by subMenu in canvasRaycaster
    /// </summary>
    public void UseItem()
    {
        bool isItemUsed = false;
        if (pickUpItemData.type == PickUpTypes.Health)
        {
            if (pickUpItemData.prefab.TryGetComponent<HealthPickUp>(out HealthPickUp healthPickUp))     // If it's a valid health item
            {
                isItemUsed = healthPickUp.HealPlayer();
                //PlayerStatusUI statusUI = playerInteraction.playerStatusUI.GetComponent<PlayerStatusUI>();
                //statusUI.UpdatePlayerHealthText();
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

    //public void UpdatePizzaSlots(InventoryItem itemInSlot)
    //{
    //    for (int index = 0; index < craftingManager.pizzaSlots?.Count;)
    //    {
    //        craftingManager.pizzaSlots?[index].UpdateSlot(itemInSlot);

    //        if (craftingManager.pizzaSlots[index] != null)
    //        {
    //            index++;
    //        }
    //    }
    //}

}
