using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class TransitionCtrl : MyMonoBehaviour
{
    [SerializeField] protected EScene currentScene = EScene.None;
    public EScene CurrentScene => currentScene;

    [SerializeField] protected EScene nextScene = EScene.None;
    public EScene NextScene => nextScene;

    [SerializeField] protected Transform playerSpawnPoint;
    public Transform PlayerSpawnPoint => playerSpawnPoint;

    [SerializeField] protected Transform playerTransform;
    public Transform PlayerTransform => playerTransform;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        //this.LoadPlayerSpawnPoint();
        this.LoadPlayerTransform();
    }

    //protected virtual void LoadPlayerSpawnPoint()
    //{
    //    if (playerSpawnPoint != null) return;
    //    this.playerSpawnPoint = this.GetComponentInChildren<Transform>();
    //    Debug.Log(transform.name + ": LoadPlayerSpawnPoint", gameObject);
    //}

    protected virtual void LoadPlayerTransform()
    {
        if (playerTransform != null) return;
        this.playerTransform = GameObject.Find("Character").transform;
        if (this.nextScene != MySceneManager.Instance.LastScene) return;
        this.playerTransform.position = this.playerSpawnPoint.position;
        Debug.Log(transform.name + ": LoadPlayerTransform", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            MySceneManager.Instance.LastScene = this.currentScene;
            MySceneManager.Instance.LoadScene(this.nextScene.ToString());
        }
    }

}
