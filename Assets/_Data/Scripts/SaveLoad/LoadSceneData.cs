using UnityEngine;

public class LoadSceneData : MyMonoBehaviour
{
    [SerializeField] protected GameObject boss;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoss();
    }

    protected virtual void LoadBoss()
    {
        if (this.boss != null) return;
        this.boss = GameObject.FindGameObjectWithTag("Boss");
        Debug.Log(transform.name + ": LoadBoss", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.LoadScene();
    }

    public virtual void LoadScene()
    {
        if (SaveLoadManager.Instance.HasSceneSave(MySceneManager.Instance.GetCurrentSceneName()))
        {
            SceneData sceneData = SaveLoadManager.Instance.LoadScene(MySceneManager.Instance.GetCurrentSceneName());
            if (this.boss != null)
                this.boss.SetActive(!sceneData.BossDefeated);
        }
        else
        {
            SceneData sceneData = new SceneData
            {
                SceneName = MySceneManager.Instance.GetCurrentSceneName(),
                BossDefeated = false,
            };

            SaveLoadManager.Instance.SaveScene(MySceneManager.Instance.GetCurrentSceneName(), sceneData);
            this.LoadScene();
        }
    }
}
