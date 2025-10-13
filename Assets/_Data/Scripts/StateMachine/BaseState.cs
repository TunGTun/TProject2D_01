using Unity.VisualScripting;

public abstract class BaseState<T> : MyMonoBehaviour where T : BaseState<T>
{
    protected StateMachine<T> stateMachine;
    public StateMachine<T> StateMachine => stateMachine;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<T>();
    }

    private void Update()
    {
        this.FrameUpdate();
    }

    private void FixedUpdate()
    {
        this.PhysicUpdate();
    }

    protected virtual void FrameUpdate()
    {
        stateMachine.FrameUpdate(this as T);
    }

    protected virtual void PhysicUpdate()
    {
        stateMachine.PhysicUpdate(this as T);
    }

    public virtual void ChangeState(IState<T> newState)
    {
        stateMachine.ChangeState(newState, this as T);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.CreateState();
    }

    protected abstract void CreateState();
}
