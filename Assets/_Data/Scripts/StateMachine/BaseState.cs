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
        stateMachine.FrameUpdate(this as T);
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicUpdate(this as T);
    }

    public void ChangeState(IState<T> newState)
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
