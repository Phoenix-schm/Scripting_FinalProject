using UnityEngine;

[CreateAssetMenu(fileName = "PizzaResult", menuName = "Scriptable Objects/Pizza Result")]
public class PizzaResultData : ScriptableObject
{
    public string pizzaName;
    public string recipe;
    public Sprite pizzaImage;
}
