using UnityEngine;

public class StatusState : CharBaseState
{
    public NormalState normal;
    public DeadState dead;

    protected override void CreateState()
    {
        normal = new NormalState();
        dead = new DeadState();
    }
}
