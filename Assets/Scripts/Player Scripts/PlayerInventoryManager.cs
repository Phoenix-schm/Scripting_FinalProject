using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

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
                    SpawnItemInSlot(item, slot);

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
        bool isAlreadyInInventory = false;
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            // if the item is already in the inventory, and has not reached the stack limit
            if (itemInSlot != null && itemInSlot.itemData == item && itemInSlot.amount < itemInSlot.maxAmount)
            {
                itemInSlot.amount++;
                itemInSlot.RefreshCount();
                isAlreadyInInventory = true;
            }
        }
        return isAlreadyInInventory;
    }

    private void SpawnItemInSlot(PickUpItemData item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        if (newItem.TryGetComponent<InventoryItem>(out InventoryItem inventoryItem))
        {
            inventoryItem.InitializeItem(item);
        }
    }

}
