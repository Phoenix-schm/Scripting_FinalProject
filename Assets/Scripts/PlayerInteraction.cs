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

    private Camera playerCamera;
    void Start()
    {
        playerCamera = GetComponent<PlayerController>().playerCamera;
    }

    void FixedUpdate()
    {
        interactionText.enabled = false;

        Ray cameraRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);   // Creates ray from middle of camera, shoots forwards
        Debug.DrawRay(cameraRay.origin, cameraRay.direction * rayDistance);

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, rayDistance, mask))                  // if the ray hits an object that the mask allows. Currently "Default"
        {
            if (hitInfo.collider.TryGetComponent<Interactable>(out Interactable interactObject))    // if you've hit an interactable object
            {
                interactionText.text = interactObject.objectPrompt;
                interactionText.enabled = true;
                PlayerInteract(interactObject);
            }
        }
    }

    private void PlayerInteract(Interactable interactObject)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactObject.BaseInteract();
        }
    }
}
