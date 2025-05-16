using UnityEngine;
using System.Collections.Generic;

public class CraftingManager : MonoBehaviour
{
    public GameObject pizzaSlotPrefab;
    public List<PizzaOvenSlot> pizzaSlots = new List<PizzaOvenSlot>();

    public PizzaResultData[] pizzaList;

    public PlayerInventoryManager playerInventoryManager;

    public void UpdatePizzaOvenContent(InventoryItem itemInSlot)
    {
        ClearSlots();

        foreach (PizzaResultData pizza in pizzaList)
        {
            int ingredientsNeeded = 0;
            int currentIngredients = 0;
            string recipeText = "";

            foreach (Ingredient ingredient in pizza.recipe)
            {
                ingredientsNeeded += 1;
                
                if (itemInSlot.amount == ingredient.amountNeeded && itemInSlot.itemData == ingredient.ingredient)         // if the player has the ingredient and the amount needed                      
                {
                    recipeText += ingredient.name + " x" + ingredient.amountNeeded + " ";
                    currentIngredients += 1;
                }
                else if (itemInSlot.itemData == ingredient.ingredient)                                                   // if they at least have the ingredient
                {
                    currentIngredients += 1;
                    recipeText += "<color=red>" + ingredient.name + " x" + ingredient.amountNeeded + " </color>"; 
                }
            }

            if (currentIngredients == ingredientsNeeded || currentIngredients != 0)
            {
                bool canBeMade = currentIngredients == ingredientsNeeded;
                GameObject pizzaSlotClone = Instantiate(pizzaSlotPrefab, gameObject.transform);

                if (pizzaSlotClone.TryGetComponent<PizzaOvenSlot>(out PizzaOvenSlot pizzaOvenSlot))
                {
                    pizzaOvenSlot.Initialize(pizza, canBeMade, recipeText);
                    pizzaSlots.Add(pizzaOvenSlot);
                }
            }
        }
    }

    private void ClearSlots()
    {
        for (int i = 0; i < pizzaSlots.Count;)
        {
            pizzaSlots[i]?.DestroySelf();
        }
    }
}
