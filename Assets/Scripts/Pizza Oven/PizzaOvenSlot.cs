using TMPro;
using UnityEngine;

public class PizzaOvenSlot : MonoBehaviour
{
    public PizzaResultData pizzaSlotResult;

    public TextMeshProUGUI pizzaName;
    public TextMeshProUGUI pizzaRecipe;

    public void Initialize(PizzaResultData pizzaResult)
    {
        pizzaSlotResult = pizzaResult;
        pizzaName.text = pizzaResult.displayName;
        pizzaRecipe.text = pizzaResult.recipe;
    }
}
