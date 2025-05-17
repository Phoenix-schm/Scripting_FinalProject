using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class PizzaOvenSlot : MonoBehaviour
{
    public PizzaResultData pizzaResult;
    public GameObject player;
    

    public TextMeshProUGUI pizzaName;
    public TextMeshProUGUI pizzaRecipe;

    [HideInInspector] public List<InventoryItem> accessIngredients = new List<InventoryItem>();
    [HideInInspector] public GameObject pizzaOvenClone = null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pizzaData"></param>
    public void Initialize(PizzaResultData pizzaData, string recipeText, GameObject initialObject)
    {
        pizzaResult = pizzaData;
        pizzaName.text = pizzaResult.displayName;
        pizzaRecipe.text = recipeText;
        pizzaOvenClone = initialObject;
        accessIngredients.Clear();
    }

    public void UpdateSlot(InventoryItem item)
    {
        string recipeText = "";
        int hasIngredients = 0;
        foreach (Ingredient ingredient in pizzaResult.recipe)
        {
            if (item.itemData == ingredient.ingredient && item.amount >= ingredient.amountNeeded)
            {
                hasIngredients++;
                recipeText += ingredient.name + " x" + ingredient.amountNeeded + " ";
                if (!accessIngredients.Contains(item))
                {
                    accessIngredients.Add(item);
                }
            }
            else if (item.itemData == ingredient.ingredient && item.amount <= ingredient.amountNeeded && item.amount != 0)
            {
                hasIngredients++;
                recipeText += "<color=red>" + ingredient.name + " x" + ingredient.amountNeeded + " </color>";
            }
            else
            {
                recipeText += "<color=red>" + ingredient.name + " x" + ingredient.amountNeeded + " </color>";
            }
        }

        pizzaRecipe.text = recipeText;

        if (hasIngredients == 0)
        {
            Destroy(pizzaOvenClone);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Add pizza to inventory and removes the required ingredients from the inventory
    /// </summary>
    public void AddPizzaToInventory()
    {
        if (accessIngredients.Count == pizzaResult.recipe.Length)
        {
            PlayerInventoryManager playerInventory = player.GetComponentInChildren<PlayerInventoryManager>();
            for (int i = 0; i < pizzaResult.recipe.Length; i++)
            {
                playerInventory.RemoveItemAmount(accessIngredients[i], pizzaResult.recipe[i].amountNeeded);
            }

            playerInventory.AddItem(pizzaResult);

        }
        else
        {
            return;
        }
    }
}
