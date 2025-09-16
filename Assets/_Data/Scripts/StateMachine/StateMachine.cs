public class StateMachine<T>
{
    protected IState<T> currentState { get; private set; }
    public IState<T> CurrentState => currentState;

    public virtual void ChangeState(IState<T> newState, T context)
    {
        if (newState == null) return;
        if (newState == currentState) return;
        //if (currentState != null && currentState.InterruptType == InterruptType.UnInterrupt) return;
        currentState?.OnExit(context);
        currentState = newState;
        currentState?.OnEnter(context);
    }

    public void FrameUpdate(T context)
    {
        currentState?.OnFrameUpdate(context);
    }

    public void PhysicUpdate(T context)
    {
        currentState?.OnPhysicUpdate(context);
    }
}