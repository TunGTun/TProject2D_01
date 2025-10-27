using UnityEngine;

public class SaveSlotCtrl : MyMonoBehaviour
{
    [SerializeField] protected int slotIndex;
    public int SlotIndex => slotIndex;

    [SerializeField] protected SaveSlotDelete saveSlotDelete;
    public SaveSlotDelete SaveSlotDelete => saveSlotDelete;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetSlotIndex();
        this.LoadSaveSlotDelete();
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

    protected override void Start()
    {
        base.Start();
        this.SaveSlotDeleteHandle();
    }

    public virtual void SaveSlotDeleteHandle()
    {
        this.saveSlotDelete.gameObject.SetActive(SaveLoadManager.Instance.HasSaveSlot(this.slotIndex));
    }
}
