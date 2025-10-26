using System.Collections;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MySingleton<SaveLoadManager>
{
    protected string saveLocation;

    protected override void Awake()
    {
        base.Awake();
        string saveDir = @"C:\SaveLoadGame\TKTT\";
        //if (!Directory.Exists(saveDir))
        //    Directory.CreateDirectory(saveDir);

        saveLocation = Path.Combine(saveDir, "saveLoadData.json");
    }

    protected override void Start()
    {
        base.Start();
        //this.LoadGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) this.SaveGame();
    }

    public virtual void SaveGame()
    {
        SaveLoadData saveLoadData = new SaveLoadData
        {
            SceneName = MySceneManager.Instance.GetCurrentSceneName(),
            PlayerPosition = CharCtrl.Instance.transform.position
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveLoadData));
    }

    // Chua toi uu
    public virtual void CreateNewSave()
    {
        SaveLoadData saveLoadData = new SaveLoadData
        {
            SceneName = EScene.West_Scene_5.ToString(),
            PlayerPosition = new Vector3(-8.5f, -1.2f, 0)
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveLoadData));
    }

    public virtual bool HasSavedFile()
    {
        return File.Exists(saveLocation);
    }

    public virtual void LoadGame()
    {
        if (this.HasSavedFile())
        {
            SaveLoadData saveLoadData = JsonUtility.FromJson<SaveLoadData>(File.ReadAllText(saveLocation));
            StartCoroutine(LoadGameRoutine(saveLoadData));
        }
        else
        {
            this.CreateNewSave();
            this.LoadGame();
        }    
    }

    protected virtual IEnumerator LoadGameRoutine(SaveLoadData saveLoadData)
    {
        MySceneManager.Instance.LoadScene(saveLoadData.SceneName);

        yield return new WaitUntil(() => MySceneManager.Instance.GetCurrentSceneName() == saveLoadData.SceneName);

        CharCtrl.Instance.transform.position = saveLoadData.PlayerPosition;
    }

    public virtual void DeleteSaveData()
    {
        if (!this.HasSavedFile()) return;
        File.Delete(saveLocation);
    }
}
