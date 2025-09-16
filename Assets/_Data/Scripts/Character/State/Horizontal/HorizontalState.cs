using UnityEngine;

public class HorizontalState : CharBaseState
{
    public CharIdleXState idleX;
    public CharRunState run;

    protected override void CreateState()
    {
        idleX = new CharIdleXState();
        run = new CharRunState();
    }
}
