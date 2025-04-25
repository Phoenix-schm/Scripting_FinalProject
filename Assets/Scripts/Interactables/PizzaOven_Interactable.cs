using UnityEngine;

public class PizzaOven_Interactable : Interactable
{
    private bool openPizzaOven; //false
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        Debug.Log("You've interacted with a pizza oven");
        openPizzaOven = !openPizzaOven;

    }
}
