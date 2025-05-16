using UnityEngine;

public class HealthPickUp : PickUpItem
{
    [SerializeField] private int healAmount;
    [SerializeField] PlayerVariables playerVariables;
    public bool HealPlayer()
    {
        bool isItemUsed = false;

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
