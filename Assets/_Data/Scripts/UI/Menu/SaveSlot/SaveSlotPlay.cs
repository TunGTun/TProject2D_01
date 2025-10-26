using UnityEngine;

public class SaveSlotPlay : ABaseButton
{
    [SerializeField] protected int slotIndex = 1;

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