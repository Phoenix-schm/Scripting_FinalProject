using UnityEngine;
using System.Collections.Generic;

public class CraftingManager : MonoBehaviour
{
    public GameObject pizzaSlotPrefab;
    public List<PizzaOvenSlot> pizzaSlots = new List<PizzaOvenSlot>();

    public PizzaResultData[] pizzaList;
    [HideInInspector] public PlayerInventoryManager playerInventoryManager;

    private void Start()
    {

    }
    public void UpdatePizzaOvenContent(InventoryItem itemInSlot)
    {
        ClearSlots();
        pizzaSlots = new List<PizzaOvenSlot>();

        foreach (PizzaResultData pizza in pizzaList)                // checking each Craftable pizza in the list
        {
            GameObject pizzaSlotClone = null;

            foreach (Ingredient ingredient in pizza.recipe)         // Checking each ingredient in that pizza
            {                
                if (itemInSlot?.itemData == ingredient.ingredient)   // we have one of the ingredients, a slot can be created
                {
                    pizzaSlotClone = Instantiate(pizzaSlotPrefab, gameObject.transform);
                    break;
                }
            }

            if (pizzaSlotClone != null)
            {
                if (pizzaSlotClone.TryGetComponent<PizzaOvenSlot>(out PizzaOvenSlot pizzaOvenSlot))
                {
                    string recipeText = CheckInventory(pizza, pizzaOvenSlot);

                    pizzaOvenSlot.Initialize(pizza, recipeText, pizzaSlotClone);
                    pizzaSlots.Add(pizzaOvenSlot);
                }
            }
        }
    }

    private void ClearSlots()
    {
        for (int i = 0; i < pizzaSlots.Count;)
        {
            if (pizzaSlots[i] != null)
            {
                pizzaSlots[i].DestroySelf();
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// Check inventory for the required ingredients
    /// </summary>
    /// <param name="pizza">the pizza data being looked at</param>
    /// <param name="pizzaOvenSlot"> the oven slot being updated</param>
    /// <returns>string of the recipe</returns>
    public string CheckInventory(PizzaResultData pizza, PizzaOvenSlot pizzaOvenSlot)
    {
        string recipeText = "";
        foreach (Ingredient ingredient in pizza.recipe)                 // checking each ingredient of the recipe
        {
            int foundIngredient = 0;
            foreach (InventorySlot slot in playerInventoryManager.inventorySlots)   // checking the whole inventory
            {
                InventoryItem item = GetComponentInChildren<InventoryItem>();
            
                if (item != null)
                {
                    if (item.itemData == ingredient.ingredient && item.amount >= ingredient.amountNeeded)     // if player has ingredient and the amount needed
                    {
                        recipeText += ingredient.name + " x" + ingredient.amountNeeded + " ";
                        foundIngredient++;
                        if (!pizzaOvenSlot.accessIngredients.Contains(item))
                        {
                            pizzaOvenSlot.accessIngredients.Add(item);
                        }
                        break;
                    }
                }

            }

            if (foundIngredient == 0)
            {
                recipeText += "<color=red>" + ingredient.name + " x" + ingredient.amountNeeded + " </color>";
            }
        }
        return recipeText;
    }
}
