using UnityEngine;

public class DeadState : ICharState<CharBaseState>
{
    public string Name => "";

    public FSMType FSMType => FSMType.Status;

    public void OnEnter(CharBaseState context)
    {
        Debug.Log("DeadState Enter");
        InputManager.Instance.SetCanControl(false);
    }

    public void OnExit(CharBaseState context)
    {
        Debug.Log("DeadState Exit");
        InputManager.Instance.SetCanControl(false);
    }

    public void OnFrameUpdate(CharBaseState context)
    {
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
    }
}
