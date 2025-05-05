using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public PickUpItemData pickUpData;

    /// <summary>
    /// Pick up an item and add it's data to the player inventory
    /// </summary>
    /// <param name="player">The player being effected</param>
    public void PickUp(PlayerInteraction player)
    {
        PickUpItemData itemData = pickUpData;
        PlayerInventoryManager inventoryManager = player.inventoryManager.GetComponent<PlayerInventoryManager>();
        bool hasBeenPlaced = inventoryManager.AddItem(itemData);

        if (!hasBeenPlaced)
        {
            player.interactionText.text = "Inventory is full";
            Debug.Log("Inventory full");
        }
        else
        {
            Debug.Log("Object is destroyed");
            Destroy(gameObject);
        }
    }
}
