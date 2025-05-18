using UnityEngine;
using UnityEngine.UI;

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
    private bool _areOtherMenusActive;       // for referencing if other menus are active
    public bool isOtherMenuActive;

    [Header("Player Variables")]
    public PlayerVariables playerVariables;
    public Transform spawnInFrontOfPlayer;

    private Camera _playerCamera;
    private PlayerShoot _playerShoot;

    // interaction buttons
    private KeyCode _interactButton = KeyCode.Mouse0;
    private KeyCode _pickUpButton = KeyCode.Mouse0;
    private KeyCode _openInventory = KeyCode.Tab;
    //private char _interactInput = 'E';
    private string _pickUpInput = "Left Mouse Button";

    void Awake()
    {
        _playerCamera = GetComponent<PlayerMovementController>().playerCamera;
        _playerShoot = GetComponentInChildren<PlayerShoot>();
        // Reset PlayerVariables on restart
        playerVariables.health = playerVariables.maxHealth;
        playerVariables.points = 0;
    }

    void Update()
    {
        interactionText.enabled = false;
        _areOtherMenusActive = playerInventory.activeSelf || isOtherMenuActive;

        Ray cameraRay = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);   // Creates ray from middle of camera, shoots forwards
        Debug.DrawRay(cameraRay.origin, cameraRay.direction * rayDistance);

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, rayDistance, mask) && !_areOtherMenusActive)   // if the ray hits an object that the mask allows. Currently "Default"
        {                                                                                                    //    and no menus are active
            GameObject hitObject = hitInfo.collider.gameObject;
            if (hitObject.TryGetComponent<Interactable>(out Interactable interactableObject))   // if you've hit something that can be interacted with
            {
                PlayerInteract(interactableObject);
            }

            if (hitObject.TryGetComponent<PickUpItem>(out PickUpItem pickUpScript))     // If you've hit something that can be picked up
            {
                PlayerPickUp(pickUpScript);
            }
        }
        else if (Input.GetKeyDown(_openInventory))
        {            
            if (!playerInventory.activeSelf && !_areOtherMenusActive)        // if inventory isn't active
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
        else if (Input.GetMouseButtonDown(0))
        {
            if (!_areOtherMenusActive)
            {
                _playerShoot.Shoot();
                Debug.Log("You attacked");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void PlayerInteract(Interactable interactableObject)
    {
        interactionText.enabled = true;

        if (interactableObject.interactText != "")
        {
            interactionText.text = interactableObject.interactText;
        }
        else
        {
            interactionText.text = "Press " + _pickUpInput + " to open the " + interactableObject.displayName;
        }

        if (Input.GetKeyDown(_interactButton))
        {
            interactableObject.Interact(this);
            //isOtherMenuActive = true;
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
