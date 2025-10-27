using UnityEngine;

public class SaveSlotPlay : ABaseButton
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
        this.Play();
    }

    protected virtual void Play()
    {
        SaveLoadManager.Instance.SetSaveSlot(this.saveSlotCtrl.SlotIndex);
        MySceneManager.Instance.LoadScene(EScene.IntermediaryScene.ToString());
    }
}