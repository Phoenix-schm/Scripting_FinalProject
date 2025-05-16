using TMPro;
using UnityEngine;

public class PizzaOvenSlot : MonoBehaviour
{
    public PizzaResultData pizzaSlotResult;
    public GameObject player;

    public TextMeshProUGUI pizzaName;
    public TextMeshProUGUI pizzaRecipe;
    public bool isUsable;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pizzaResult"></param>
    /// <param name="hasAllIngedients"></param>
    public void Initialize(PizzaResultData pizzaResult, bool hasAllIngedients, string recipeText)
    {
        pizzaSlotResult = pizzaResult;
        pizzaName.text = pizzaSlotResult.displayName;
        isUsable = hasAllIngedients;
        pizzaRecipe.text = recipeText;
    }

    private string GetRecipe(PizzaResultData pizzaResult)
    {
        string recipe = "";
        Ingredient[] ingredientList = pizzaResult.recipe;

        for (int i = 0; i < ingredientList.Length; i++)                 // going through each pizza ingredient
        {
            Ingredient ingredient = ingredientList[i];
            recipe += ingredient.name + " x" + ingredient.amountNeeded;

            if (ingredientList.Length > 1 && i != ingredientList.Length)
            {
                recipe += ", ";
            }
        }

        return recipe;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void AddPizzaToInventory()
    {
        if (!isUsable)
        {
            return;
        }
        else
        {
            PlayerInventoryManager playerInventory = player.GetComponentInChildren<PlayerInventoryManager>();
            playerInventory.AddItem(pizzaSlotResult);
            Destroy(gameObject);
        }
    }
}
