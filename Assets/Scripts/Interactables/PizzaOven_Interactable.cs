using UnityEngine;

public class PizzaOven_Interactable : Interactable
{
    //public GameObject pizzaOvenMenu;
    //public GameObject noPizzasMessage;
    //public CraftingManager craftingManager;
    //[HideInInspector] public PlayerInventoryManager playerInventoryManager;

    public GameObject ovenContent;
    public Transform pizzaSpawnLocation;

    private PlayerMovementController playerMovement;
    private PlayerInteraction player;
    public override void Interact(PlayerInteraction playerInteraction)
    {
        playerMovement = playerInteraction.GetComponent<PlayerMovementController>();
        player = playerInteraction;
        //playerInventoryManager = player.gameObject.GetComponentInChildren<PlayerInventoryManager>();
        //craftingManager.playerInventoryManager = playerInventoryManager;

        player.isOtherMenuActive = true;
        playerMovement.enabled = false;
        player.interactionText.enabled = false;
        player.enabled = false;

        Cursor.lockState = CursorLockMode.None;                     // Make Cursor visible
        Cursor.visible = true;

        Debug.Log("You've interacted with a pizza oven");

        ovenContent.SetActive(true);
        //pizzaOvenMenu.SetActive(true);

        //PizzaOvenSlot slot = craftingManager.gameObject.GetComponentInChildren<PizzaOvenSlot>();
        //if (slot == null)
        //{
        //    noPizzasMessage.SetActive(true);
        //}
        //else
        //{
        //    noPizzasMessage.SetActive(false);
        //}
    }

    public void ExitPizzaMenu()
    {
        //pizzaOvenMenu.SetActive(false);
        ovenContent.SetActive(false);
        playerMovement.enabled = true;
        
        player.enabled = true;
        player.isOtherMenuActive = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //if (pizzaOvenMenu.activeSelf)
        if (ovenContent.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.E))
            {
                ExitPizzaMenu();
            }
        }
    }
}
