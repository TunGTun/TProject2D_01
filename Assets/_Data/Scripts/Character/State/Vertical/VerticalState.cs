using UnityEngine;

public class VerticalState : CharBaseState
{
    public IdleGroundState idleGround;
    public JumpState jump;
    public FallState fall;

    protected override void CreateState()
    {
        idleGround = new IdleGroundState();
        jump = new JumpState();
        fall = new FallState();
    }
}
