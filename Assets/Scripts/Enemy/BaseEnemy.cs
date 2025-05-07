using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemy : MonoBehaviour
{
    private StateMachine _stateMachine;
    private NavMeshAgent _agent;
    public NavMeshAgent Agent { get => _agent; }
    public EntityPath path;

    [SerializeField] private int enemySpeed = 10;
    [SerializeField] private string currentState;   // for debugging purposes
    [SerializeField] private GameObject player;
    [SerializeField] private float sightDistance = 20f;
    [SerializeField] private float fieldOfView = 45f;
    [SerializeField] private float eyeHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _stateMachine = GetComponent<StateMachine>();
        _agent = GetComponent<NavMeshAgent>();

        _stateMachine.Initialize();
        Agent.speed = enemySpeed;

        if (path != null)           // make enemy spawn position it's first patrol waypoint
        {
            transform.position = path.waypoints[0].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = _stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        bool canSeePlayer = false;
        if (player == null)
        {
            Debug.Log(gameObject.name + ": Player object is null. Add player object to reference.");
            return canSeePlayer;
        }

        if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)        // if the player is close enough to be seen
        {
            Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
            float angleToPlayer = Vector3.Angle(targetDirection, transform.position);

            bool isPlayerWithinFOV = angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView;

            if (isPlayerWithinFOV)
            {
                Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);     // ray in direction of player
                if (Physics.Raycast(ray, out RaycastHit hitinfo, sightDistance))
                {
                    if (hitinfo.collider.gameObject == player)
                    {
                        canSeePlayer = true;
                        Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                    }
                }
            }
        }   

        return canSeePlayer;
    }
}
