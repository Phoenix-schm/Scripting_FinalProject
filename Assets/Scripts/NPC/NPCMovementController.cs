using UnityEngine;
using UnityEngine.AI;

public class NPCMovementController : MonoBehaviour
{
    private float maxMoveTimer = 5;
    private float moveTimer = 0;

    public NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer > maxMoveTimer)
        {
            Vector3 randomDestination = transform.position + (Random.insideUnitSphere * 5);
            agent.SetDestination(randomDestination);

            moveTimer = 0;
        }

        if (agent.velocity.x > 0 || agent.velocity.z > 0)
        {
            GetComponent<Animator>().SetBool("isMoving", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isMoving", false);
        }
    }
}
