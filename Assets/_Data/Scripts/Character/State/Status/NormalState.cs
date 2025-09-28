using UnityEngine;

public class NormalState : ICharState<CharBaseState>
{
    public string Name => "";

    public FSMType FSMType => FSMType.Default;

    public void OnEnter(CharBaseState context)
    {
        context.CharCtrl.AnimationCtrl.UpdateAnimation();
    }

    public void OnExit(CharBaseState context)
    {
        //Debug.Log("NormalState Exit");
    }

    public void OnFrameUpdate(CharBaseState context)
    {
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
    }
}
