using UnityEngine;

public class Cube_Interactable : Interactable
{
    public override void Interact(PlayerInteraction player)
    {
        Debug.Log("You've interacted with a cube");
        isOpen = !isOpen;
    }
}
