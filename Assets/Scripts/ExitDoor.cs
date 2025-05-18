using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : Interactable
{
    public override void Interact(PlayerInteraction player)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);      // Loads the next scene in the queue
    }

}
