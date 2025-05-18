using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject[] npcs;
    public PizzaResultData[] pizzas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnNPC", 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNPC()
    {
        int spawnIndex = Random.Range(0, spawnLocations.Length);
        int npcIndex = Random.Range(0, npcs.Length);
        int pizzaIndex = Random.Range(0, pizzas.Length);

        GameObject npcClone = Instantiate(npcs[npcIndex], spawnLocations[spawnIndex].position, spawnLocations[spawnIndex].rotation);

        if (npcClone.TryGetComponent<NPC_Interactable>(out NPC_Interactable npc))
        {
            npc.pizzaRequest = pizzas[pizzaIndex];
            npc.interactText = "I want a " + npc.pizzaRequest.displayName;
        }
    }
}
