using UnityEngine;

public class StatusState : CharBaseState
{
    public NormalState normal;
    public HealState heal;
    public HurtState hurt;
    public DeadState dead;
    public CutSceneState cutScene;
    public SceneTransitionState sceneTransition;

    protected override void CreateState()
    {
        normal = new NormalState();
        heal = new HealState();
        hurt = new HurtState();
        dead = new DeadState();
        cutScene = new CutSceneState();
        sceneTransition = new SceneTransitionState();
    }
}
