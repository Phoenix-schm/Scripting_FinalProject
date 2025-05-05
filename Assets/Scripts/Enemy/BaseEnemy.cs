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

    [SerializeField] private string currentState;   // for debugging purposes

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _stateMachine = GetComponent<StateMachine>();
        _agent = GetComponent<NavMeshAgent>();

        _stateMachine.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
