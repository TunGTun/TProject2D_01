using UnityEngine;

public class CutSceneState : ICharState<CharBaseState>
{
    public string Name => StateName.CUT_SCENE_STATE;

    public FSMType FSMType => FSMType.Status;

    public void OnEnter(CharBaseState context)
    {
        
    }

    public void OnExit(CharBaseState context)
    {

    }

    public void OnFrameUpdate(CharBaseState context)
    {
    }

    public void OnPhysicUpdate(CharBaseState context)
    {
    }
}
