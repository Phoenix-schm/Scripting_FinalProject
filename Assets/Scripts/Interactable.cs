using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string objectPrompt;     // Displayed message when looking at interactable object

    public void BaseInteract()
    {
        // called by player
        Interact();
    }

    protected virtual void Interact()
    {

    }
}
