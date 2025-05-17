using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// All logic pertaining to moving items in the inventory.
/// </summary>
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public PickUpItemData itemData;
    [HideInInspector] public Transform parentAfterDrag;
    public int maxAmount = 9;
    public int amount = 1;
    
    public Image itemImage;
    public TextMeshProUGUI amountText;

    #region Initialization
    public void InitializeItem(PickUpItemData newItem)
    {
        itemData = newItem;
        itemImage.sprite = newItem.icon;
        RefreshCount();
    }

    public void RefreshCount()
    {
        amountText.text = amount.ToString();
        bool textActive = amount > 1;
        amountText.gameObject.SetActive(textActive);
    }
    #endregion

    #region Inputs
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemImage = GetComponent<Image>();
        Debug.Log("Begin dragging");
        parentAfterDrag = transform.parent;                 // Takes the transform of where it started
        transform.SetParent(parentAfterDrag.parent.parent); // Sets parent as the Inventory menu, the canvas (decouples from the original item slot)
        transform.SetAsLastSibling();                       // Allows item to be ontop of everything in the inventory
        itemImage.raycastTarget = false;                        // Ignore the inventoryItem image when while being dragged, can now be placed to the image below it
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
        itemImage.raycastTarget = true;                         // Can now be picked back up
    }
    #endregion
}
