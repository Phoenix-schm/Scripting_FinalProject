using UnityEngine;

public class HealthPickUp : PickUpItem
{
    [SerializeField] private int healAmount;

    public bool HealPlayer(PlayerInteraction player)
    {
        bool isItemUsed = false;
        PlayerVariables playerVariables = player.playerVariables;
        if (playerVariables.health < playerVariables.maxHealth)
        {
            playerVariables.health += healAmount;
            isItemUsed = true;

            if (playerVariables.health > playerVariables.maxHealth)
            {
                playerVariables.health = playerVariables.maxHealth;
            }
        }

        return isItemUsed;
    }
}
