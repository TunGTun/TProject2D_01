using UnityEngine;

public class M_StateMachine 
{
    public M_EntityState currentState { get; private set; }
    public bool canChangeState = true;


    public void Initialize(M_EntityState startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(M_EntityState newState)
    {
        if (canChangeState == false)
        { return; }
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void UpdateActiveState()
    {
        currentState.Update();        
    }
    public void SwitchOffStateMachine()
    {
        canChangeState = false;
    }
}
