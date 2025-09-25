//public enum InterruptType
//{
//    Interrupt,
//    UnInterrupt
//}

public enum FSMType
{
    Default,
    Horizontal,
    Vertical,
    Skill,
    Status
}

public interface IState<T>
{
    public string Name { get;  }

    void OnEnter(T context);
    void OnFrameUpdate(T context);
    void OnPhysicUpdate(T context);
    void OnExit(T context);
}

public interface ICharState<T> : IState<T>
{
    public FSMType FSMType { get; }
    //public InterruptType InterruptType { get; }

}

public interface IBossState<T> : IState<T>
{

}
