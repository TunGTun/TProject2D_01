using UnityEngine;

public class StatusState : CharBaseState
{
    public NormalState normal;
    public HurtState hurt;
    public DeadState dead;

    protected override void CreateState()
    {
        normal = new NormalState();
        hurt = new HurtState();
        dead = new DeadState();
    }
}
