using UnityEngine;

public class PizzaOven_Interactable : MonoBehaviour, IInteractable
{
    private bool openPizzaOven; //false

    public void Interact(PlayerInteraction player)
    {
        Debug.Log("You've interacted with a pizza oven");
        openPizzaOven = !openPizzaOven;
    }
}
