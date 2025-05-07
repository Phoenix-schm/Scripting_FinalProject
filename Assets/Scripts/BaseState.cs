
/// <summary>
/// The base state of an enemy
/// </summary>
public abstract class BaseState
{
    public BaseEnemy enemy;
    public StateMachine stateMachine;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
