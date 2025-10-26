using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TransitionCtrl : MyMonoBehaviour
{
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
        CharCtrl.Instance.transform.position = this.playerSpawnPoint.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MySceneManager.Instance.LastScene = this.currentScene;
            MySceneManager.Instance.LoadScene(this.nextScene.ToString());
        }
    }

}
