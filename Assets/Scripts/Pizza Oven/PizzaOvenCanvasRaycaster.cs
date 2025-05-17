using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PizzaOvenCanvasRaycaster : MonoBehaviour
{
    GraphicRaycaster canvasRaycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    PizzaOven_Interactable pizzaOven;
    PlayerInventoryManager inventoryManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        pizzaOven = GetComponent<PizzaOven_Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {

        }
    }
}
