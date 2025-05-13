using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Collections;
using System;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemy : MonoBehaviour
{
    public UnityEvent onAttackPlayer;
    private StateMachine _stateMachine;
    private NavMeshAgent _agent;
    public NavMeshAgent Agent { get => _agent; }
    public EntityPath path;
    public GameObject player;
    public EnemyData enemyData;

    [SerializeField] private string currentState;   // for debugging purposes
    [SerializeField] private int enemySpeed = 10;

    [Header("Sight Values")]
    [SerializeField] private float sightDistance = 20f;
    [SerializeField] private float fieldOfView = 45f;
    [SerializeField] private float eyeHeight;

    [HideInInspector] public int health;

    // attack
    private int _hurtTimer;
    private WaitForSeconds hurtDelay = new WaitForSeconds(1);
   
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

        health = enemyData.health;
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
            float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

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

    IEnumerator HurtCountDown()
    {
        Debug.Log("Start hurt timer");
        yield return hurtDelay;
        Debug.Log("Finish wait timer");
        _hurtTimer = 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerInteraction>(out PlayerInteraction player) && _hurtTimer == 0)
        {
            _hurtTimer++;
            PlayerVariables playerVariables = player.playerVariables;
            playerVariables.health -= enemyData.damage;
            onAttackPlayer.Invoke();
            StartCoroutine("HurtCountDown");
        }
    }
}
