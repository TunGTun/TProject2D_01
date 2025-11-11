using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TransitionCtrl : MyMonoBehaviour
{
    [SerializeField] protected ESceneDirection direction = ESceneDirection.Horizontal;
    public ESceneDirection Direction => direction;

    [SerializeField] protected EScene currentScene = EScene.None;
    public EScene CurrentScene => currentScene;

    [SerializeField] protected EScene nextScene = EScene.None;
    public EScene NextScene => nextScene;

    [SerializeField] protected Transform playerSpawnPoint;
    public Transform PlayerSpawnPoint => playerSpawnPoint;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerSpawnPoint();
        this.SetPlayerTransform();
    }

    protected virtual void LoadPlayerSpawnPoint()
    {
        if (playerSpawnPoint != null) return;
        this.playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").GetComponent<Transform>();
        Debug.Log(transform.name + ": LoadPlayerSpawnPoint", gameObject);
    }

    protected virtual void SetPlayerTransform()
    {
        if (this.nextScene != MySceneManager.Instance.LastScene) return;
        StartCoroutine(SceneTransitionRoutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharCtrl.Instance.CharStateCtrl.StatusState.sceneTransition.Direction = this.direction;
            CharCtrl.Instance.CharStateCtrl.StatusState.ChangeState(CharCtrl.Instance.CharStateCtrl.StatusState.sceneTransition);

            MySceneManager.Instance.LastScene = this.currentScene;
            MySceneManager.Instance.LoadScene(this.nextScene.ToString());
        }
    }

    protected virtual IEnumerator SceneTransitionRoutine()
    {
        CharCtrl.Instance.transform.position = this.playerSpawnPoint.position;

        if (this.direction == ESceneDirection.Up)
        {
            CharCtrl.Instance.RigidBody2D.linearVelocityY = 0f;
        }

        if (this.direction == ESceneDirection.Down)
        {
            CharCtrl.Instance.RigidBody2D.linearVelocityY = 0f;
            CharCtrl.Instance.RigidBody2D.AddForce(Vector2.up * SCharStaticData.JumpForce, ForceMode2D.Impulse);
            CharCtrl.Instance.CharStateCtrl.StatusState.sceneTransition.CurrentMoveInput = CharCtrl.Instance.transform.localScale.x;
            CharCtrl.Instance.AnimationCtrl.ChangeAnimationState(StateName.JUMP_STATE);
            yield return new WaitForSeconds(SSceneTransitionData.AnimationDuration * 0.4f);
        }

        yield return new WaitForSeconds(SSceneTransitionData.AnimationDuration * 0.4f);

        CharCtrl.Instance.CharStateCtrl.StatusState.ChangeState(CharCtrl.Instance.CharStateCtrl.StatusState.normal);
    }

}
