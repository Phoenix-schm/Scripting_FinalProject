using UnityEngine;

public class Interactable_Test : MonoBehaviour, IInteractable
{
    public void Interact(PlayerInteraction player)
    {
        Debug.Log("I'm an interactive object");
    }

}
