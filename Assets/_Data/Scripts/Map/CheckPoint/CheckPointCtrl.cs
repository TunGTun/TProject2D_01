using UnityEngine;

public class CheckPointCtrl : MySingleton<CheckPointCtrl>
{
    [SerializeField] protected Transform checkPointPortal;
    public Transform CheckPointPortal => checkPointPortal;

    [SerializeField] protected CheckPointInteract checkPointInteract;
    public CheckPointInteract CheckPointInteract => checkPointInteract;

    [SerializeField] protected Transform spawnPoint;
    public Transform SpawnPoint => spawnPoint;

    [SerializeField] protected GameObject checkPointPanel;
    public GameObject CheckPointPanel => checkPointPanel;

    [SerializeField] protected CheckPointWorldCanvas checkPointWorldCanvas;
    public CheckPointWorldCanvas CheckPointWorldCanvas => checkPointWorldCanvas;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCheckPointPortal();
        this.LoadCheckPointInteract();
        this.LoadSpawnPoint();
        this.LoadCheckPointPanel();
        this.LoadCheckPointWorldCanvas();
    }

    protected virtual void LoadCheckPointPortal()
    {
        if (this.checkPointPortal != null) return;
        this.checkPointPortal = GameObject.Find("CheckPointPortal").transform;
        Debug.Log(transform.name + ": LoadCheckPointPortal", gameObject);
    }

    protected virtual void LoadCheckPointInteract()
    {
        if (this.checkPointInteract != null) return;
        this.checkPointInteract = GetComponentInChildren<CheckPointInteract>(true);
        Debug.Log(transform.name + ": LoadCheckPointInteract", gameObject);
    }

    protected virtual void LoadSpawnPoint()
    {
        if (this.spawnPoint != null) return;
        this.spawnPoint = GameObject.Find("SpawnPoint").transform;
        Debug.Log(transform.name + ": LoadSpawnPoint", gameObject);
    }

    protected virtual void LoadCheckPointPanel()
    {
        if (this.checkPointPanel != null) return;
        this.checkPointPanel = GameObject.Find("CheckPointPanel");
        Debug.Log(transform.name + ": LoadCheckPointPanel", gameObject);
    }

    protected virtual void LoadCheckPointWorldCanvas()
    {
        if (this.checkPointWorldCanvas != null) return;
        this.checkPointWorldCanvas = GetComponentInChildren<CheckPointWorldCanvas>(true);
        Debug.Log(transform.name + ": LoadCheckPointWorldCanvas", gameObject);
    }

    private void Update()
    {
        Debug.Log(this.spawnPoint.transform.position);
    }
}
