using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// Once an item is dropped on a slot, the slot becomes the parent of the dropped item
    /// </summary>
    /// <param name="eventData">The item being dropped</param>
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        droppedItem.TryGetComponent<InventoryItem>(out InventoryItem item);
        item.parentAfterDrag = transform;       // the new parent is (this) inventory slot
    }
}
