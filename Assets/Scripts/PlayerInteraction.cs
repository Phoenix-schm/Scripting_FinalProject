using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] int rayDistance = 10;

    LayerMask mask;

    void Start()
    {
        mask = LayerMask.GetMask("Interactable Object");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //RaycastHit hit;
        Vector3 forward = transform.TransformDirection(transform.forward);

        if (Physics.Raycast(transform.position, forward, rayDistance, mask))
        {
            Debug.Log("You've hit an interactable object");
        }
        else
        {
            Debug.Log("You're not looking at anything");
        }
    }
}
