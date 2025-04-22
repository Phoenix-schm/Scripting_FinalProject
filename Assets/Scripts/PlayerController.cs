using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Camera playerCamera;

    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float cameraSpeed = 10f;

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float verticalInput = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        float mouseXInput = Input.GetAxis("Mouse Y") * cameraSpeed;
        float mouseYInput = Input.GetAxis("Mouse X") * cameraSpeed;

        playerCamera.transform.Rotate(-mouseXInput, 0, 0);          // camera rotation, only rotates up/down as player rotation of left/right is used
        transform.Rotate(0, mouseYInput, 0);                        // player rotation, only rotates left/right as up/down would be weird for movement
        transform.Translate(horizontalInput, 0, verticalInput);     // player movement forwards/backwards/left/right
    }
}
