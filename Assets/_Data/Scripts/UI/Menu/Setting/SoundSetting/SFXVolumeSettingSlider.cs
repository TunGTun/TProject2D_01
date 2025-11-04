using UnityEngine;

public class SFXVolumeSettingSlider : ABaseSlider
{

    [Header("SFXVolumeSettingSlider")]
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

    public virtual void LoadSFXVolume()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            this.slider.value = PlayerPrefs.GetFloat("SFXVolume");
        }

        this.OnValueChanged(this.slider.value);
    }

    protected override void OnValueChanged(float value)
    {
        this.volumeSetting.AudioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}