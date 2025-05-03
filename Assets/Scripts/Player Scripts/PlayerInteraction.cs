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
    public Text interactionText;
    public GameObject playerInventory;
    public GameObject inventoryManager;
    private bool _isOtherMenusActive;

    [Header("Player Variables")]
    public PlayerVariables playerVariables;
    

    private Camera _playerCamera;

    // interaction buttons
    private KeyCode _interactButton = KeyCode.E;
    //private KeyCode attack = KeyCode.Mouse0;
    private KeyCode _pickUpButton = KeyCode.Mouse0;
    private KeyCode _openInventory = KeyCode.Tab;
    private char _interactInput = 'E';
    private string _pickUpInput = "Left Mouse Button";

    void Awake()
    {
        _playerCamera = GetComponent<PlayerMovementController>().playerCamera;

        // Reset PlayerVariables on restart
        playerVariables.health = playerVariables.maxHealth;
    }

    void Update()
    {
        interactionText.enabled = false;
        _isOtherMenusActive = playerInventory.activeSelf;

        Ray cameraRay = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);   // Creates ray from middle of camera, shoots forwards
        Debug.DrawRay(cameraRay.origin, cameraRay.direction * rayDistance);

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, rayDistance, mask) && !_isOtherMenusActive)   // if the ray hits an object that the mask allows. Currently "Default"
        {                                                                                                    //    and no menus are active
            GameObject hitObject = hitInfo.collider.gameObject;

            if (hitObject.TryGetComponent(out IInteractable interactScript))    // if you've hit an interactable object
            {
                //TODO: In the interact interface, add a display interactable text and display interactable menu func
                PlayerInteract(interactScript, hitObject);
            }
            if (hitObject.TryGetComponent<PickUpItem>(out PickUpItem pickUpScript))     // If you've hit something that can be picked up
            {
                PlayerPickUp(pickUpScript);
            }
        }

        if (Input.GetKeyDown(_openInventory))
        {            
            if (!playerInventory.activeSelf)                                // if inventory isn't active
            {
                Debug.Log("You've opened you're inventory");
                playerInventory.SetActive(true);
                GetComponent<PlayerMovementController>().enabled = false;   // halt movement
                Cursor.lockState = CursorLockMode.None;                     // Make Cursor visible
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
        interactionText.text = "Press " + _interactInput + " to interact with " + interactObject.name;
        if (Input.GetKeyDown(_interactButton))
        {
            interactScript.Interact(this);
        }
    }

    /// <summary>
    /// Logic for player picking up an object
    /// Should be used with objects that can be picked up and added to player inventory.
    /// </summary>
    /// <param name="pickUpScript">The script found on the pick up object</param>
    private void PlayerPickUp(PickUpItem pickUpScript)
    {
        interactionText.enabled = true;
        interactionText.text = "Press " + _pickUpInput + " to pickup " + pickUpScript.pickUpData.displayName;
        if (Input.GetKeyDown(_pickUpButton))
        {
            pickUpScript.PickUp(this);
        }
    }
}
