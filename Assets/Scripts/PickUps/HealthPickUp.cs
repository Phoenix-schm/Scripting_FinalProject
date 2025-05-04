using UnityEngine;

public class HealthPickUp : PickUpItem
{
    public int healAmount;

    public void HealPlayer(PlayerInteraction player)
    {
        PlayerVariables playerVariables = player.playerVariables;

        if (playerVariables.health < playerVariables.maxHealth)
        {
            playerVariables.health += healAmount;

            if (playerVariables.health > playerVariables.maxHealth)
            {
                playerVariables.health = playerVariables.maxHealth;
            }
        }
        else if (playerVariables.health == playerVariables.maxHealth)
        {

        }
    }
}
