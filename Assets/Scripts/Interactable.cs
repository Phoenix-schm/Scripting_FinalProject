using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string objectPrompt;     // Displayed message when looking at interactable object
    public string objectName;

    public void BaseInteract()
    {
        // called by player
        Interact();
    }

    protected virtual void Interact()
    {

    }

    private void PauseMovement()
    {
        Time.timeScale = 0;
    }

    private void ResumeMovement()
    {
        Time.timeScale = 1.0f;
    }
}
