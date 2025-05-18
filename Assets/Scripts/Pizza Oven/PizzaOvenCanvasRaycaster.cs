using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PizzaOvenCanvasRaycaster : MonoBehaviour
{
    GraphicRaycaster canvasRaycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    public PizzaOven_Interactable pizzaOven;
    PlayerInventoryManager inventoryManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //inventoryManager = pizzaOven.playerInventoryManager;

        canvasRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    // Raycast logic
            //    pointerEventData = new PointerEventData(eventSystem);
            //    pointerEventData.position = Input.mousePosition;

            //    List<RaycastResult> results = new List<RaycastResult>();
            //    canvasRaycaster.Raycast(pointerEventData, results);        // Raycast at pointer position, and list out the data
            //    foreach(RaycastResult result in results)
            //    {
            //        if (result.gameObject.TryGetComponent<PizzaOvenSlot>(out PizzaOvenSlot pizzaOvenSlot))
            //        {
            //            pizzaOvenSlot.inventoryManager = inventoryManager;
            //        }
            //    }
            //}
        }
    }

}
