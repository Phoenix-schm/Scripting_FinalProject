using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemInInventory> inventory;

    public static event Action<PlayerInventory> OnUpdateInventory;

    public void AddToInventory(PlayerInventory inventory)
    {

    }

    public void RemoveFromInventory(PlayerInventory inventory)
    {

    }
}

public class ItemInInventory
{
    public GameObject item;
    public int amountInInventory;
    PickUpItemData pickUpData;

    ItemInInventory(GameObject pickUpItem, int amount, PickUpItemData data)
    {
        item = pickUpItem;
        amountInInventory = amount;
        pickUpData = data;
    }
}
