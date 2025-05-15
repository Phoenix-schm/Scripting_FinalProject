using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CanvasRaycaster : MonoBehaviour
{
    GraphicRaycaster canvasRaycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    public GameObject mainInventory;
    public PlayerInventoryManager inventoryManager;

    private InventoryItem inventoryItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainInventory.activeSelf)
        {
            if (Input.GetMouseButtonDown(1))                                // On right click
            {
                // Raycast logic
                pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();
                canvasRaycaster.Raycast(pointerEventData, results);        // Raycast at pointer position, and list out the data

                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.TryGetComponent<InventoryItem>(out inventoryItem))    // check what it's hitting is what you want
                    {
                        inventoryManager.subMenu.transform.position = pointerEventData.position;
                        inventoryManager.subMenu.SetActive(true);

                        inventoryManager.selectedItem = result.gameObject;
                        inventoryManager.pickUpItemData = inventoryItem.itemData;
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0) && inventoryManager.subMenu.activeSelf)
            {
                pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();
                canvasRaycaster.Raycast(pointerEventData, results);

                bool hitSubMenu = false;
                foreach(RaycastResult result in results)
                {
                    if (result.gameObject == inventoryManager.subMenu.GetComponentInChildren<Image>().gameObject)
                    {
                        hitSubMenu = true;
                    }
                }

                if (!hitSubMenu)
                {
                    inventoryManager.subMenu.SetActive(false);
                }
            }
        }
        else
        {
            if (inventoryManager.subMenu.activeSelf)
            {
                inventoryManager.subMenu.SetActive(false);
            }
        }
    }
}
