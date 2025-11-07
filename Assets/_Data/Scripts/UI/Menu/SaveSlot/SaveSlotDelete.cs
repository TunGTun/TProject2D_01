using UnityEngine;

public class SaveSlotDelete : ABaseButton
{
    [SerializeField] protected SaveSlotCtrl saveSlotCtrl;
    public SaveSlotCtrl SaveSlotCtrl => saveSlotCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSaveSlotCtrl();
    }

    protected virtual void LoadSaveSlotCtrl()
    {
        if (saveSlotCtrl != null) return;
        this.saveSlotCtrl = GetComponentInParent<SaveSlotCtrl>();
        Debug.LogWarning(transform.name + ": LoadSaveSlotCtrl", gameObject);
    }

    protected override void OnClick()
    {
        this.Delete();
    }

    protected virtual void Delete()
    {
        SaveLoadManager.Instance.DeleteSlot(this.saveSlotCtrl.SlotIndex);
        this.saveSlotCtrl.SaveSlotDeleteHandle();
        this.saveSlotCtrl.SetSaveSlotImage();
    }
}
