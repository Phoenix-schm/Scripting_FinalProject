using UnityEngine;

public class AttackState : BaseState
{
    private float _moveTimer;

    private float _timeUntilLostPlayer = 3.5f;
    private float _losePlayerTimer;
    public override void Enter()
    {
    }
    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            _losePlayerTimer = 0;
            enemy.Agent.SetDestination(enemy.player.transform.position);

            stateMachine.lookingForPlayerIndicator.SetActive(false);
            stateMachine.seePlayerIndicator.SetActive(true);
            //_moveTimer += Time.deltaTime;
            //if (_moveTimer > Random.Range(3, 7))
            //{
            //    enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));   // enemy chooses a random position within a 5m circle
            //    _moveTimer = 0;
            //}
        }
        else
        {
            stateMachine.seePlayerIndicator.SetActive(false);
            stateMachine.lookingForPlayerIndicator.SetActive(true);

            _losePlayerTimer += Time.deltaTime;
            if (_losePlayerTimer > _timeUntilLostPlayer)
            {
                stateMachine.lookingForPlayerIndicator.SetActive(false);
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public override void Exit()
    {
    }
}
