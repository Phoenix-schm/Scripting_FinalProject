using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    private Camera playerCamera;

    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private float rayDistance = 3f;
    [SerializeField] private LayerMask mask;

    void Start()
    {
        playerCamera = GetComponent<PlayerController>().playerCamera;

        interactionText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray cameraRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);   // Creates ray from middle of camera, shoots forwards

        Debug.DrawRay(cameraRay.origin, cameraRay.direction * rayDistance);

        RaycastHit hitInfo;     // Stores hit information
        if (Physics.Raycast(cameraRay, out hitInfo, rayDistance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)      // if you've hit an interactable object
            {
                Debug.Log(hitInfo.collider.GetComponent<Interactable>().objectPrompt);
                
                interactionText.text = hitInfo.collider.GetComponent<Interactable>().objectPrompt;
            }
            else
            {
                interactionText.text = "";
            }
        }
        else
        {
            Debug.Log("You're not looking at anything");
            interactionText.text = "";
        }
    }
}
