using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        image = GetComponent<Image>();
        Debug.Log("Begin dragging");
        parentAfterDrag = transform.parent;                 // Takes the transform of where it started
        transform.SetParent(parentAfterDrag.parent.parent); // Sets parent as the Inventory menu, the canvas (decouples from the original item slot)
        transform.SetAsLastSibling();                       // Allows item to be ontop of everything in the inventory
        image.raycastTarget = false;                        // Ignore the inventoryItem image when while being dragged, can now be placed to the image below it
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;           // Item follows mouse around
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Stop dragging");
        transform.SetParent(parentAfterDrag);               // Sets as original parent
        image.raycastTarget = true;                         // Can now be picked back up
    }
}
