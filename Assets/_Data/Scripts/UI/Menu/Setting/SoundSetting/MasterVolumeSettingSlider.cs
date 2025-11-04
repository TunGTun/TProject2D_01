using UnityEngine;
using UnityEngine.Rendering;

public class MasterVolumeSettingSlider : ABaseSlider
{
    [Header("MasterVolumeSettingSlider")]
    [SerializeField] protected VolumeSetting volumeSetting;

    protected override void LoadSlider()
    {
        base.LoadSlider();
        this.slider.minValue = 0.0001f;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadVolumeSetting();
    }

    protected virtual void LoadVolumeSetting()
    {
        if (this.volumeSetting != null) return;
        this.volumeSetting = GetComponentInParent<VolumeSetting>(true);
        Debug.LogWarning(transform.name + ": LoadVolumeSetting", gameObject);
    }

    public virtual void LoadMasterVolume()
    {

        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            this.slider.value = PlayerPrefs.GetFloat("MasterVolume");
        }

        this.OnValueChanged(this.slider.value);
    }

    protected override void OnValueChanged(float value)
    {
        this.volumeSetting.AudioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
}
