using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PizzaResult", menuName = "Scriptable Objects/Pizza Result")]
public class PizzaResultData : PickUpItemData
{
    public List<Ingredient> recipe;
    public int pointsGiven;
}
