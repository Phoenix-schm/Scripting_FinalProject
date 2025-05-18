using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PizzaOvenSlot : MonoBehaviour
{
    public PizzaResultData pizzaResult;
    [HideInInspector] public PlayerInventoryManager inventoryManager;

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
        if (inventoryManager != null)
        {
            int foundIngredients = 0;
            foreach (Ingredient ingredient in pizzaResult.recipe)
            {
                foreach (InventoryItem item in accessIngredients)
                {
                    if (item.itemData == ingredient.ingredient)
                    {
                        foundIngredients++;
                        break;
                    }
                }
            }

            if (foundIngredients == pizzaResult.recipe.Count)
            {
                foreach (Ingredient ingredient in pizzaResult.recipe)
                {
                    foreach (InventoryItem item in accessIngredients)
                    {
                        if (item.itemData == ingredient.ingredient)
                        {
                            int ingredientIndex = accessIngredients.IndexOf(item);
                            int recipeIndex = pizzaResult.recipe.IndexOf(ingredient);
                            inventoryManager.RemoveItemAmount(accessIngredients[ingredientIndex], pizzaResult.recipe[recipeIndex].amountNeeded);
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Could not add pizza to inventory");
                return;
            }
        }
        else
        {
            Debug.Log(gameObject.name + ": Inventory Manager is null");
        }
    }
}
