public class StateMachine<T>
{
    public IState<T> CurrentState { get; private set; }

    public void ChangeState(IState<T> newState, T context)
    {
        if (newState == null) return;
        if (newState == CurrentState) return;
        CurrentState?.OnExit(context);
        CurrentState = newState;
        CurrentState?.OnEnter(context);
    }

    public void FrameUpdate(T context)
    {
        CurrentState?.OnFrameUpdate(context);
    }

    public void PhysicUpdate(T context)
    {
        CurrentState?.OnPhysicUpdate(context);
    }
}
