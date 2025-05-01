using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Generic Attributes")]
    [SerializeField] private float rayDistance = 3f;
    [SerializeField] private LayerMask mask;

    [Header("UI Attributes")]
    [Tooltip("Text that shows up when hovering over an intereactable object")]
    [SerializeField] private Text interactionText;
    public GameObject playerInventory;
    private bool isOtherMenusActive;

    [Header("Player Variables")]
    public PlayerVariables playerVariables;
    

    private Camera playerCamera;

    // interaction buttons
    private KeyCode interactButton = KeyCode.E;
    //private KeyCode attack = KeyCode.Mouse0;
    private KeyCode pickUpButton = KeyCode.Mouse0;
    private KeyCode openInventory = KeyCode.Tab;
    private char interactChar = 'E';
    private string pickUpString = "Left Mouse Button";

    void Awake()
    {
        playerCamera = GetComponent<PlayerMovementController>().playerCamera;

        // Reset PlayerVariables on restart
        playerVariables.inventory = new List<PickUpItemData>();
        playerVariables.inventoryDictionary = new Dictionary<PickUpItemData, GameObject>();
        playerVariables.health = playerVariables.maxHealth;
    }

    void Update()
    {
        interactionText.enabled = false;
        isOtherMenusActive = playerInventory.activeSelf;

        Ray cameraRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);   // Creates ray from middle of camera, shoots forwards
        Debug.DrawRay(cameraRay.origin, cameraRay.direction * rayDistance);

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, rayDistance, mask) && !isOtherMenusActive)                  // if the ray hits an object that the mask allows. Currently "Default"
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            if (hitObject.TryGetComponent(out IInteractable interactScript))    // if you've hit an interactable object
            {
                PlayerInteract(interactScript, hitObject);
            }
            if (hitObject.TryGetComponent(out IPickUpable pickUpScript))   // if you've hit an object that can be picked up
            {
                PlayerPickUp(pickUpScript, hitObject);
            }
        }

        if (Input.GetKeyDown(openInventory))
        {            
            Debug.Log("You've opened you're inventory");
            if (!playerInventory.activeSelf)                                // if inventory isn't active
            {
                playerInventory.SetActive(true);
                GetComponent<PlayerMovementController>().enabled = false;   // halt movement
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                playerInventory.SetActive(false);
                GetComponent<PlayerMovementController>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    /// <summary>
    /// Logic for player interacting with an object.
    /// Should be used with objects in the overworld that cannot be picked up in player inventory
    /// </summary>
    /// <param name="interactScript">The script found on the interactable object</param>
    /// <param name="interactObject">The object being interacted with</param>
    private void PlayerInteract(IInteractable interactScript, GameObject interactObject)
    {
        interactionText.enabled = true;
        interactionText.text = "Press " + interactChar + " to interact with " + interactObject.name;
        if (Input.GetKeyDown(interactButton))
        {
            interactScript.Interact(this);
        }
    }

    /// <summary>
    /// Logic for player picking up an object
    /// Should be used with objects that can be picked up and added to player inventory.
    /// </summary>
    /// <param name="pickUpScript">The script found on the pick up object</param>
    /// <param name="pickUpObject">The object being picked up</param>
    private void PlayerPickUp(IPickUpable pickUpScript, GameObject pickUpObject)
    {
        interactionText.enabled = true;
        interactionText.text = "Press " + pickUpString + " to pickup " + pickUpObject.name;
        if (Input.GetKeyDown(pickUpButton))
        {
            pickUpScript.PickUp(this);
        }
    }
}
