using UnityEngine;

public class SaveSlotPlay : ABaseButton
{
    [SerializeField] protected int slotIndex;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.GetSlotIndex();
    }

    protected virtual void GetSlotIndex()
    {
        string[] parts = transform.parent.name.Split('_');

        if (parts.Length <= 0) return;

        string lastPart = parts[parts.Length - 1];

        if (int.TryParse(lastPart, out int index))
        {
            slotIndex = index;
        }
    }

    protected override void OnClick()
    {
        this.Play();
    }

    protected virtual void Play()
    {
        SaveLoadManager.Instance.SetSaveSlot(slotIndex);
        MySceneManager.Instance.LoadScene(EScene.IntermediaryScene.ToString());
    }
}