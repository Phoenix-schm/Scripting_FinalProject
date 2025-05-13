using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;
    private int _maxWait = 1;   // how long the enemy will wait 
    public override void Enter()
    {
    }
    public override void Perform()
    {
        PatrolCycle();
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }
    public override void Exit()
    {
    }

    /// <summary>
    /// Going through each point of enemy.path.waypoints
    /// </summary>
    public void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f)       // if enemy is within .2f of waypoint, go to next waypoint
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > _maxWait)
            {
                if (waypointIndex < enemy.path.waypoints.Count - 1)     // run through each waypoint
                {
                    waypointIndex++;
                }
                else
                {
                    waypointIndex = 0;
                }

                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                waitTimer = 0;
            }
        }
    }
}
