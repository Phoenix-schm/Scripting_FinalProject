using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private char interactChar = 'E';
    private string pickUpString = "Left Mouse Button";

    void Start()
    {
        playerCamera = GetComponent<PlayerMovementController>().playerCamera;
    }

    void FixedUpdate()
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
            else if (hitObject.TryGetComponent(out IPickUpable pickupObject))
            {
                PlayerPickUp(pickupObject, hitObject);
            }
        }
    }

    private void PlayerInteract(IInteractable interactScript, GameObject interactObject)
    {
        interactionText.enabled = true;
        interactionText.text = "Press " + interactChar + " to interact with " + interactObject.name;
        if (Input.GetKeyDown(interactButton))
        {
            interactScript.Interact(this);
        }
    }

    private void PlayerPickUp(IPickUpable pickUpScript, GameObject pickUpObject)
    {
        interactionText.enabled = true;
        interactionText.text = "Press " + pickUpString + " to interact with " + pickUpObject.name;
        if (Input.GetKeyDown(pickUpButton))
        {
            pickUpScript.PickUpItem(this);
        }
    }
}
