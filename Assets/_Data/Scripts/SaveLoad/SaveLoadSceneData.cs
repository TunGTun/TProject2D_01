using UnityEngine;

public class SaveLoadSceneData : MySingleton<SaveLoadSceneData>
{
    [SerializeField] protected GameObject boss;
    public GameObject Boss => boss;

    [SerializeField] protected SceneData sceneData;
    public SceneData SceneData => sceneData;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoss();
        this.LoadScene();
    }

    protected virtual void LoadBoss()
    {
        if (this.boss != null) return;
        this.boss = GameObject.FindGameObjectWithTag("Boss");
        Debug.Log(transform.name + ": LoadBoss", gameObject);
    }

    public virtual void LoadScene()
    {
        if (SaveLoadManager.Instance.HasSceneSave(MySceneManager.Instance.GetCurrentSceneName()))
        {
            this.sceneData = SaveLoadManager.Instance.LoadScene(MySceneManager.Instance.GetCurrentSceneName());
            if (this.boss != null)
            {
                if (sceneData.BossDefeated)
                {
                    this.boss.SetActive(!sceneData.BossDefeated);
                    this.boss = null;
                }
            }

        }
        else
        {
            this.sceneData = new SceneData
            {
                SceneName = MySceneManager.Instance.GetCurrentSceneName(),
            };

            this.SaveScene();

            this.LoadScene();
        }
    }

    public virtual void SaveScene()
    {
        SaveLoadManager.Instance.SaveScene(MySceneManager.Instance.GetCurrentSceneName(), this.sceneData);
    }
}
