using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;
    //patrolState property

    public void Initialize()
    {
        patrolState = new PatrolState();
        ChangeState(patrolState);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activeState?.Perform();
    }

    public void ChangeState(BaseState newState)
    {
        // if activeState isn't null, finish activities
        activeState?.Exit();

        activeState = newState;
        if (activeState != null)                    // failsafe null check
        {
            activeState.stateMachine = this;        // redo stateMachine to be the activeState
            activeState.enemy = GetComponent<BaseEnemy>();
            activeState.Enter();
        }
        else
        {
            Debug.Log("ERROR: State is null");
        }
    }
}
