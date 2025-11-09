using UnityEditor.UIElements;
using UnityEngine;

public class SceneTransitionState : ICharState<CharBaseState>
{
    public string Name => StateName.SCENE_TRANSITION_STATE;

    public FSMType FSMType => FSMType.Default;

    protected string originalTag;

    public ESceneDirection Direction = ESceneDirection.Horizontal;

    public float CurrentMoveInput;

    public void OnEnter(CharBaseState context)
    {
        originalTag = context.transform.parent.gameObject.tag;
        context.transform.parent.gameObject.tag = "Untagged";

        CurrentMoveInput = InputManager.Instance.MoveInput;

        context.CharCtrl.CharDamageReceiver.CanTakeDamage = false;
        InputManager.Instance.SetCanControl(false);

        if (Direction == ESceneDirection.Up)
        {
            CurrentMoveInput = 0f;
            context.CharCtrl.RigidBody2D.linearVelocityY = 0f;
            context.CharCtrl.RigidBody2D.AddForce(Vector2.up * SCharStaticData.JumpForce, ForceMode2D.Impulse);
            context.CharCtrl.AnimationCtrl.ChangeAnimationState(StateName.JUMP_STATE);
        }
    }

    public void OnExit(CharBaseState context)
    {
        context.CharCtrl.CharDamageReceiver.CanTakeDamage = true;
        InputManager.Instance.SetCanControl(true);
        context.transform.parent.gameObject.tag = originalTag;
    }

    public void OnFrameUpdate(CharBaseState context)
    {
        if (Direction == ESceneDirection.Horizontal || Direction == ESceneDirection.Up)
        {
            InputManager.Instance.MoveInput = CurrentMoveInput;
        }
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
    }
}
