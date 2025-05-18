using TMPro;
using UnityEngine;

public class SpawnPizzaButton : MonoBehaviour
{
    public PizzaResultData spawnedPizza;
    public TextMeshProUGUI pizzaName;
    public PizzaOven_Interactable pizzaOven;
    private Transform spawnLocation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnLocation = pizzaOven.pizzaSpawnLocation;
        pizzaName.text = spawnedPizza.displayName;
    }

    public void SpawnPizza()
    {
        GameObject pizzaClone = Instantiate(spawnedPizza.prefab, spawnLocation.position, spawnLocation.rotation);
    }
}
