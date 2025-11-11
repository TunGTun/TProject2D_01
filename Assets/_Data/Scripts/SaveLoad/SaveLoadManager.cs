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

    //Test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) this.SavePlayer();
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadSceneData loadSceneData = GameObject.Find("LoadSceneData").GetComponent<LoadSceneData>();

            bool bossDefeated = true;
            if (loadSceneData.Boss != null)
            {
                BaseBossCtrl baseBossCtrl = loadSceneData.Boss.GetComponent<BaseBossCtrl>();
                if (baseBossCtrl.BaseBossState.StateMachine.CurrentState != baseBossCtrl.BaseBossState.dead)
                    bossDefeated = false;
            }

            SceneData sceneData = new SceneData
            {
                SceneName = MySceneManager.Instance.GetCurrentSceneName(),
                BossDefeated = bossDefeated,
            };

            this.SaveScene(MySceneManager.Instance.GetCurrentSceneName(), sceneData);
        }
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
            CurrentMP = CharCtrl.Instance.CharData.CurrentMP,
            Money = CharCtrl.Instance.CharData.Money,
        };

        string path = Path.Combine(CurrentSlotPath, "playerData.json");
        File.WriteAllText(path, JsonUtility.ToJson(playerData, true));
    }

    public void CreateNewPlayerSave()
    {
        PlayerData playerData = new PlayerData
        {
            SceneName = EScene.West_Scene_5.ToString(),
            Position = new Vector3(-4.5f, -1.2f, 0),
            MaxHP = CharCtrl.Instance.CharData.MaxHP,
            MaxMP = CharCtrl.Instance.CharData.MaxMP,
            AttackDamage = CharCtrl.Instance.CharData.AttackDamage,
            CurrentHP = CharCtrl.Instance.CharData.MaxHP,
            CurrentMP = CharCtrl.Instance.CharData.MaxMP,
            Money = 0,
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
        CharCtrl.Instance.CharData.MaxHP = playerData.MaxHP;
        CharCtrl.Instance.CharData.MaxMP = playerData.MaxMP;
        CharCtrl.Instance.CharData.AttackDamage = playerData.AttackDamage;
        CharCtrl.Instance.CharData.CurrentHP = playerData.CurrentHP;
        CharCtrl.Instance.CharData.CurrentMP = playerData.CurrentMP;
        CharCtrl.Instance.CharData.Money = playerData.Money;
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

    public bool HasSaveSlot(int slot)
    {
        string path = Path.Combine(rootPath, $"SaveSlot_{slot}");
        return Directory.Exists(path);
    }

    public void DeleteSlot(int slot)
    {
        string path = Path.Combine(rootPath, $"SaveSlot_{slot}");
        if (this.HasSaveSlot(slot))
            Directory.Delete(path, true);
    }
}
