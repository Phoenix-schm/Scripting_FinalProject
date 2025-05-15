using UnityEngine;

public class PizzaOven_Interactable : Interactable
{
    public GameObject pizzaOvenMenu;
    private PlayerMovementController playerMovement;
    private PlayerInteraction player;
    public override void Interact(PlayerInteraction playerInteraction)
    {
        // TODO: Exit on Tab press in player interaction
        playerMovement = playerInteraction.GetComponent<PlayerMovementController>();
        player = playerInteraction;

        playerMovement.enabled = false;
        player.interactionText.enabled = false;
        player.enabled = false;

        Debug.Log("You've interacted with a pizza oven");

        pizzaOvenMenu.SetActive(true);
    }

    private void ExitPizzaMenu()
    {
        pizzaOvenMenu.SetActive(false);
        playerMovement.enabled = true;
        player.enabled = true;
    }
}
