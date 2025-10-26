using System.Collections;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MySingleton<SaveLoadManager>
{
    private string rootPath = @"C:\SaveLoadGame\TKTT\";
    private int currentSlot = 1;

    public string CurrentSlotPath => Path.Combine(rootPath, $"SaveSlot_{currentSlot}");

    protected override void Awake()
    {
        base.Awake();
        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);
    }

    protected override void Start()
    {
        base.Start();
        //this.LoadGame();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L)) this.SaveGame();
    }

    public void SetSaveSlot(int slot)
    {
        currentSlot = Mathf.Clamp(slot, 1, 3);
        if (!Directory.Exists(CurrentSlotPath))
            Directory.CreateDirectory(CurrentSlotPath);
    }

    public void SavePlayer()
    {
        PlayerData playerData = new PlayerData
        {
            SceneName = MySceneManager.Instance.GetCurrentSceneName(),
            Position = CharCtrl.Instance.transform.position,
            MaxHP = CharCtrl.Instance.CharData.MaxHP,
            MaxMP = CharCtrl.Instance.CharData.MaxMP,
            AttackDamage = CharCtrl.Instance.CharData.AttackDamage,
            CurrentHP = CharCtrl.Instance.CharData.CurrentHP,
            CurrentMP = CharCtrl.Instance.CharData.CurrentMP
        };

        string path = Path.Combine(CurrentSlotPath, "playerData.json");
        File.WriteAllText(path, JsonUtility.ToJson(playerData, true));
    }

    public void CreateNewPlayerSave()
    {
        PlayerData playerData = new PlayerData
        {
            SceneName = EScene.West_Scene_5.ToString(),
            Position = new Vector3(-8.5f, -1.2f, 0),
            MaxHP = CharCtrl.Instance.CharData.MaxHP,
            MaxMP = CharCtrl.Instance.CharData.MaxHP,
            AttackDamage = CharCtrl.Instance.CharData.AttackDamage,
            CurrentHP = CharCtrl.Instance.CharData.MaxHP,
            CurrentMP = CharCtrl.Instance.CharData.MaxHP
        };

        string path = Path.Combine(CurrentSlotPath, "playerData.json");
        File.WriteAllText(path, JsonUtility.ToJson(playerData, true));
    }

    public bool HasPlayerSave()
    {
        string path = Path.Combine(CurrentSlotPath, "playerData.json");
        return File.Exists(path);
    } 

    public void LoadPlayer()
    {
        if (this.HasPlayerSave())
        {
            string path = Path.Combine(CurrentSlotPath, "playerData.json");
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(path));
            StartCoroutine(LoadPlayerRoutine(playerData));
        }
        else
        {
            this.CreateNewPlayerSave();
            this.LoadPlayer();
        }
    }

    protected virtual IEnumerator LoadPlayerRoutine(PlayerData playerData)
    {
        MySceneManager.Instance.LoadScene(playerData.SceneName);
        yield return new WaitUntil(() => MySceneManager.Instance.GetCurrentSceneName() == playerData.SceneName);
        CharCtrl.Instance.transform.position = playerData.Position;
    }

    public void SaveScene(string sceneName, SceneData data)
    {
        string path = Path.Combine(CurrentSlotPath, $"{sceneName}.json");
        File.WriteAllText(path, JsonUtility.ToJson(data, true));
    }

    public bool HasSceneSave(string sceneName)
    {
        string path = Path.Combine(CurrentSlotPath, $"{sceneName}.json");
        return File.Exists(path);
    }

    public SceneData LoadScene(string sceneName)
    {
        string path = Path.Combine(CurrentSlotPath, $"{sceneName}.json");
        return JsonUtility.FromJson<SceneData>(File.ReadAllText(path));
    }

    public void DeleteSlot(int slot)
    {
        string path = Path.Combine(rootPath, $"SaveSlot_{slot}");
        if (Directory.Exists(path))
            Directory.Delete(path, true);
    }

    //public virtual void SaveGame()
    //{
    //    PlayerData saveLoadData = new PlayerData
    //    {
    //        SceneName = MySceneManager.Instance.GetCurrentSceneName(),
    //        Position = CharCtrl.Instance.transform.position
    //    };

    //    File.WriteAllText(saveLocation, JsonUtility.ToJson(saveLoadData));
    //}

    //// Chua toi uu
    //public virtual void CreateNewSave()
    //{
    //    PlayerData saveLoadData = new PlayerData
    //    {
    //        SceneName = EScene.West_Scene_5.ToString(),
    //        Position = new Vector3(-8.5f, -1.2f, 0)
    //    };

    //    File.WriteAllText(saveLocation, JsonUtility.ToJson(saveLoadData));
    //}

    //public virtual bool HasSavedFile()
    //{
    //    return File.Exists(saveLocation);
    //}

    //public virtual void LoadGame()
    //{
    //    if (this.HasSavedFile())
    //    {
    //        PlayerData saveLoadData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(saveLocation));
    //        StartCoroutine(LoadGameRoutine(saveLoadData));
    //    }
    //    else
    //    {
    //        this.CreateNewSave();
    //        this.LoadGame();
    //    }
    //}

    //public virtual void DeleteSaveData()
    //{
    //    if (!this.HasSavedFile()) return;
    //    File.Delete(saveLocation);
    //}
}
