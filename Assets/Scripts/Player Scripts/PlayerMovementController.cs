using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Camera playerCamera;

    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float cameraSpeed = 10f;

    private float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        float mouseY = Input.GetAxis("Mouse Y") * cameraSpeed;
        float mouseX = Input.GetAxis("Mouse X") * cameraSpeed;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        playerCamera.transform.localRotation = Quaternion.Euler(pitch, 0, 0);   // camera rotation, only rotates up/down as player rotation right/left is used
        transform.Rotate(0, mouseX, 0);                                         // player rotation, only rotates left/right as up/down would be weird for movement

        if (moveDirection.magnitude > 0)
        {
            transform.Translate(moveDirection * movementSpeed * Time.deltaTime);          // player movement forwards/backwards/left/right
        }
    }
}
