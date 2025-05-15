using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PizzaRecipe", menuName = "Pizza Recipe")]

public class RecipeData : ScriptableObject
{
    public List<PickUpItemData> recipeList;
}
