using UnityEngine;
using UnityEngine.Events;

public class NPC_Interactable : Interactable
{
    public PizzaResultData pizzaRequest;
    private PlayerInventoryManager playerInventory;
    private PlayerStatusUI playerUI;
    public PlayerVariables playerVariables;
    public override void Interact(PlayerInteraction player)
    {
        playerInventory = player.GetComponentInChildren<PlayerInventoryManager>();
        playerUI = player.gameObject.GetComponent<PlayerStatusUI>();

        bool wasGivenPizza = playerInventory.RemoveItem(pizzaRequest);

        if (wasGivenPizza)
        {
            GivePlayerPoints();
            Destroy(gameObject); 
        }
    }

    public void GivePlayerPoints()
    {
        playerVariables.points += pizzaRequest.pointsGiven;
        playerUI.UpdatePlayerPoints();
    }

}
