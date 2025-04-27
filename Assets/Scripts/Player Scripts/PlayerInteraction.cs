using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Generic Attributes")]
    [SerializeField] private float rayDistance = 3f;
    [SerializeField] private LayerMask mask;

    [Header("UI Attributes")]
    [Tooltip("Text that shows up when hovering over an intereactable object")]
    [SerializeField] private Text interactionText;

    [Header("Player Variables")]
    [SerializeField] PlayerVariables playerVariables;

    private Camera playerCamera;

    // interaction buttons
    private KeyCode interactButton = KeyCode.E;
    private KeyCode attack = KeyCode.Mouse0;
    private KeyCode pickUpButton = KeyCode.Mouse0;
    private KeyCode openInventory = KeyCode.Tab;
    private char interactChar = 'E';
    private string pickUpString = "Left Mouse Button";

    void Awake()
    {
        playerCamera = GetComponent<PlayerMovementController>().playerCamera;

        // Reset PlayerVariables on restart
        playerVariables.inventory = new List<PickUpItemData>();
        playerVariables.itemData = new Dictionary<PickUpItemData, int>();
        playerVariables.health = playerVariables.maxHealth;
    }

    void Update()
    {
        interactionText.enabled = false;

        Ray cameraRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);   // Creates ray from middle of camera, shoots forwards
        Debug.DrawRay(cameraRay.origin, cameraRay.direction * rayDistance);

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, rayDistance, mask))                  // if the ray hits an object that the mask allows. Currently "Default"
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            if (hitObject.TryGetComponent(out IInteractable interactObject))    // if you've hit an interactable object
            {
                PlayerInteract(interactObject, hitObject);
            }
            if (hitObject.TryGetComponent(out IPickUpable pickupObject))   // if you've hit an object that can be picked up
            {
                hitObject.TryGetComponent<PickUpItem>(out PickUpItem item);
                PlayerPickUp(item);
            }
        }

        if (Input.GetKeyDown(openInventory))
        {
            // open menu, pause movement but not world
            // get mouse clicks
            // if clicked on inventory item, show item stats
            // able to drop item

            //close menu on tab again
        }
    }

    /// <summary>
    /// Logic for player interacting with an object.
    /// Should be used with objects in the overworld that cannot be picked up in player inventory
    /// </summary>
    /// <param name="interactInterface">The interface being called</param>
    /// <param name="interactObject">The object being interacted with</param>
    private void PlayerInteract(IInteractable interactInterface, GameObject interactObject)
    {
        interactionText.enabled = true;
        interactionText.text = "Press " + interactChar + " to interact with " + interactObject.name;
        if (Input.GetKeyDown(interactButton))
        {
            interactInterface.Interact(this);
        }
    }

    /// <summary>
    /// Logic for player picking up an object
    /// Should be used with objects that can be picked up and added to player inventory.
    /// </summary>
    /// <param name="pickUpObject">Object being picked up</param>
    private void PlayerPickUp(PickUpItem pickUpObject)
    {
        interactionText.enabled = true;
        interactionText.text = "Press " + pickUpString + " to pickup " + pickUpObject.pickUpData.displayName;
        if (Input.GetKeyDown(pickUpButton))
        {
            pickUpObject.PickUp(playerVariables);
        }
    }
}
