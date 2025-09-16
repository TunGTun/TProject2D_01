using System.Collections;
using UnityEngine;

public class CharControl : BaseChar
{
    //Tạm
    public bool IsDead = false;

    protected override void Start()
    {
        base.Start();
        this.charCtrl.CharStateCtrl.HorizontalState.ChangeState(this.charCtrl.CharStateCtrl.HorizontalState.idleX);
        this.charCtrl.CharStateCtrl.VerticalState.ChangeState(this.charCtrl.CharStateCtrl.VerticalState.idleGround);
        this.charCtrl.CharStateCtrl.SkillState.ChangeState(this.charCtrl.CharStateCtrl.SkillState.idleSkill);
    }

    private void Update()
    {
        //this.HorizontalStateControl();
    }

    protected virtual void HorizontalStateControl()
    {
        if (InputManager.Instance.MoveInput == 0)
        {
            this.charCtrl.CharStateCtrl.HorizontalState.ChangeState(this.charCtrl.CharStateCtrl.HorizontalState.idleX);
            return;
        }
        else
        {
            this.charCtrl.CharStateCtrl.HorizontalState.ChangeState(this.charCtrl.CharStateCtrl.HorizontalState.run);
            return;
        }
    }

    protected virtual void VerticalStateControl()
    {
        if (this.charCtrl.EnvironmentChecker.IsGrounded && this.charCtrl.RigidBody2D.linearVelocityY == 0)
        {
            this.charCtrl.CharStateCtrl.VerticalState.ChangeState(this.charCtrl.CharStateCtrl.VerticalState.idleGround);
        }

        if (InputManager.Instance.SpaceInput)
        {
            this.charCtrl.CharStateCtrl.VerticalState.ChangeState(this.charCtrl.CharStateCtrl.VerticalState.jump);
        }

        if (this.charCtrl.RigidBody2D.linearVelocityY < 0)
        {
            this.charCtrl.CharStateCtrl.VerticalState.ChangeState(this.charCtrl.CharStateCtrl.VerticalState.fall);
        }

    }

    protected virtual void SkillStateControl()
    {
        if (InputManager.Instance.LeftMouseClick)
        {
            this.charCtrl.CharStateCtrl.SkillState.ChangeState(this.charCtrl.CharStateCtrl.SkillState.attack);
            StartCoroutine(AttackRoutine());
        }
    }

    protected virtual void StatusStateControl()
    {
        if (IsDead)
        {
            this.charCtrl.CharStateCtrl.StatusState.ChangeState(this.charCtrl.CharStateCtrl.StatusState.dead);
            return;
        }
        else
        {
            this.charCtrl.CharStateCtrl.StatusState.ChangeState(this.charCtrl.CharStateCtrl.StatusState.normal);
            return;
        }
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        this.charCtrl.CharStateCtrl.SkillState.ChangeState(this.charCtrl.CharStateCtrl.SkillState.idleSkill);
    }
}
