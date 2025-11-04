using UnityEngine;
using UnityEngine.Audio;

public class VolumeSetting : MyMonoBehaviour
{
    [Header("VolumeSetting")]
    [SerializeField] protected AudioMixer audioMixer;
    public AudioMixer AudioMixer => audioMixer;

    [SerializeField] protected MasterVolumeSettingSlider masterVolumeSettingSlider;
    [SerializeField] protected MusicVolumeSettingSlider musicVolumeSettingSlider;
    [SerializeField] protected SFXVolumeSettingSlider sfxVolumeSettingSlider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMasterVolumeSettingSlider();
        this.LoadMusicVolumeSettingSlider();
        this.LoadSFXVolumeSettingSlider();
    }

    protected virtual void LoadMasterVolumeSettingSlider()
    {
        if (this.masterVolumeSettingSlider != null) return;
        this.masterVolumeSettingSlider = GetComponentInChildren<MasterVolumeSettingSlider>();
        Debug.LogWarning(transform.name + ": LoadMasterVolumeSettingSlider", gameObject);
    }

    protected virtual void LoadMusicVolumeSettingSlider()
    {
        if (this.musicVolumeSettingSlider != null) return;
        this.musicVolumeSettingSlider = GetComponentInChildren<MusicVolumeSettingSlider>();
        Debug.LogWarning(transform.name + ": LoadMusicVolumeSettingSlider", gameObject);
    }

    protected virtual void LoadSFXVolumeSettingSlider()
    {
        if (this.sfxVolumeSettingSlider != null) return;
        this.sfxVolumeSettingSlider = GetComponentInChildren<SFXVolumeSettingSlider>();
        Debug.LogWarning(transform.name + ": LoadSFXVolumeSettingSlider", gameObject);
    }

    public virtual void LoadVolume()
    {
        this.masterVolumeSettingSlider.LoadMasterVolume();
        this.musicVolumeSettingSlider.LoadMusicVolume();
        this.sfxVolumeSettingSlider.LoadSFXVolume();
    }
}
