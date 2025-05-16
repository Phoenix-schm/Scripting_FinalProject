using UnityEngine;

[CreateAssetMenu(fileName = "PizzaResult", menuName = "Scriptable Objects/Pizza Result")]
public class PizzaResultData : PickUpItemData
{
    public Ingredient[] recipe;
}
