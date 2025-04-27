using UnityEngine;

public class Cube_Interactable : MonoBehaviour, IInteractable
{
    private bool openCube;

    public void Interact(PlayerInteraction player)
    {
        Debug.Log("You've interacted with a cube");
        openCube = !openCube;
    }
}
