using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Camera playerCamera;

    [SerializeField] private float rayDistance = 3f;
    [SerializeField] private LayerMask mask;

    void Start()
    {
        playerCamera = GetComponent<PlayerController>().playerCamera;
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
                Debug.Log("You've hit an interactable object");
            }
        }
        else
        {
            Debug.Log("You're not looking at anything");
        }
    }
}
