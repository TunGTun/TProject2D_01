using UnityEngine;

public class MusicVolumeSettingSlider : ABaseSlider
{

    [Header("MusicVolumeSettingSlider")]
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
        this.volumeSetting = GetComponentInParent<VolumeSetting>();
        Debug.LogWarning(transform.name + ": LoadVolumeSetting", gameObject);
    }

    protected override void Start()
    {
        base.Start();

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            this.slider.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        this.OnValueChanged(this.slider.value);
    }

    protected override void OnValueChanged(float value)
    {
        this.volumeSetting.AudioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
}