using UnityEngine;

public class CharMoveState : IState<CharState>
{
    public void OnEnter(CharState context)
    {
        Debug.Log("CharMoveState Enter");
    }

    public void OnExit(CharState context)
    {
        Debug.Log("CharMoveState Exit");
    }

    public void OnFrameUpdate(CharState context)
    {
    }

    public void OnPhysicUpdate(CharState context)
    {
    }
}
