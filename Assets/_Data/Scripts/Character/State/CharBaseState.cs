using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public abstract class CharBaseState : BaseState<CharBaseState>
{
    [Header("CharBaseState")]

    [SerializeField] protected CharCtrl charCtrl;
    public CharCtrl CharCtrl => charCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharCtrl();
    }

    protected virtual void LoadCharCtrl()
    {
        if (charCtrl != null) return;
        charCtrl = GetComponentInParent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    protected override void FrameUpdate()
    {
        if (this is StatusState)
        {
            base.FrameUpdate();
            return;
        }
        if (this.charCtrl.CharStateCtrl.StatusState.StateMachine.CurrentState != null)
            if ((this.charCtrl.CharStateCtrl.StatusState.StateMachine.CurrentState as ICharState<CharBaseState>)?.FSMType != FSMType.Default) return;
        base.FrameUpdate();
    }

    protected override void PhysicUpdate()
    {
        if (this is StatusState)
        {
            base.PhysicUpdate();
            return;
        }
        if (this.charCtrl.CharStateCtrl.StatusState.StateMachine.CurrentState != null)
            if ((this.charCtrl.CharStateCtrl.StatusState.StateMachine.CurrentState as ICharState<CharBaseState>)?.FSMType != FSMType.Default) return;
        base.PhysicUpdate();
    }

    public override void ChangeState(IState<CharBaseState> newState)
    {
        if (this is StatusState)
        {
            base.ChangeState(newState);
            return;
        }
        if (this.charCtrl.CharStateCtrl.StatusState.StateMachine.CurrentState != null)
             if ((this.charCtrl.CharStateCtrl.StatusState.StateMachine.CurrentState as ICharState<CharBaseState>)?.FSMType != FSMType.Default) return;
        base.ChangeState(newState);
    }
}
