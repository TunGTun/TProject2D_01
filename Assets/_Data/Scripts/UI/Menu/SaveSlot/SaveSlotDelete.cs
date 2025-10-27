using UnityEngine;

public class SaveSlotDelete : ABaseButton
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
        this.Delete();
    }

    protected virtual void Delete()
    {
        SaveLoadManager.Instance.DeleteSlot(slotIndex);
    }
}
