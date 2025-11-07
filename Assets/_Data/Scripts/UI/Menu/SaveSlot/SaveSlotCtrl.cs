using UnityEngine;
using UnityEngine.UI;

public class SaveSlotCtrl : MyMonoBehaviour
{
    [SerializeField] protected int slotIndex;
    public int SlotIndex => slotIndex;

    [SerializeField] protected SaveSlotDelete saveSlotDelete;
    public SaveSlotDelete SaveSlotDelete => saveSlotDelete;

    [SerializeField] protected GameObject newGame;
    public GameObject NewGame => newGame;

    [SerializeField] protected GameObject continueImage;
    public GameObject ContinueImage => continueImage;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetSlotIndex();
        this.LoadSaveSlotDelete();
        this.LoadNewGame();
        this.LoadContinueImage();

    }

    protected virtual void GetSlotIndex()
    {
        string[] parts = transform.name.Split('_');

        if (parts.Length <= 0) return;

        string lastPart = parts[parts.Length - 1];

        if (int.TryParse(lastPart, out int index))
        {
            slotIndex = index;
        }
    }

    protected virtual void LoadSaveSlotDelete()
    {
        if (saveSlotDelete != null) return;
        this.saveSlotDelete = GetComponentInChildren<SaveSlotDelete>();
        Debug.LogWarning(transform.name + ": LoadSaveSlotDelete", gameObject);
    }

    protected virtual void LoadNewGame()
    {
        if (newGame != null) return;
        this.newGame = GameObject.Find($"NewGame_{this.slotIndex}");
        Debug.LogWarning(transform.name + ": LoadNewGame", gameObject);
    }

    protected virtual void LoadContinueImage()
    {
        if (continueImage != null) return;
        this.continueImage = GameObject.Find($"Continue_{this.slotIndex}");
        Debug.LogWarning(transform.name + ": LoadContinueImage", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.SaveSlotDeleteHandle();
        this.SetSaveSlotImage();
    }

    public virtual void SaveSlotDeleteHandle()
    {
        this.saveSlotDelete.gameObject.SetActive(SaveLoadManager.Instance.HasSaveSlot(this.slotIndex));
    }

    public virtual void SetSaveSlotImage()
    {
        if (SaveLoadManager.Instance.HasSaveSlot(this.slotIndex))
        {
            this.continueImage.SetActive(true);
            this.newGame.SetActive(false);
        }
        else
        {
            this.newGame.SetActive(true);
            this.continueImage.SetActive(false);
        }

    }
}
