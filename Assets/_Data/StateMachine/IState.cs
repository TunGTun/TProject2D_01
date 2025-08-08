public interface IState<T>
{
    void OnEnter(T context);
    void OnFrameUpdate(T context);
    void OnPhysicUpdate(T context);
    void OnExit(T context);
}
