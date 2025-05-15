using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public GameObject pizzaSlotPrefab;
    public List<PizzaOvenSlot> pizzaSlots = new List<PizzaOvenSlot>();

    public PickUpItemData[] ingredients;
    public string[] recipes;
    public PizzaResultData[] recipeResults;

    public PlayerInventoryManager playerInventoryManager;

}
