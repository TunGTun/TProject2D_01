using System;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MySingleton<AudioManager>
{
    [Header("AudioManager")]
    [SerializeField] protected MusicSource musicSource;
    public MusicSource MusicSource => musicSource;

    [SerializeField] protected SFXSource sfxSource;
    public SFXSource SFXSource => sfxSource;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMusicSource();
        this.LoadSFXSource();
    }

    protected virtual void LoadMusicSource()
    {
        if (musicSource != null) return;
        this.musicSource = GetComponentInChildren<MusicSource>();
        Debug.Log(transform.name + ": LoadMusicSource", gameObject);
    }

    protected virtual void LoadSFXSource()
    {
        if (sfxSource != null) return;
        this.sfxSource = GetComponentInChildren<SFXSource>();
        Debug.Log(transform.name + ": LoadSFXSource", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.PlayMusic(ESoundName.MainMenu);
    }

    public virtual void PlayMusic(ESoundName name)
    {
        SoundData soundData = Array.Find(this.musicSource.MusicSounds, x => x.soundName == name);
        this.musicSource.Music.clip = soundData.soundClip;
        this.musicSource.Music.Play();
    }

    public void StopMusic()
    {
        if (this.musicSource.Music != null)
        {
            this.musicSource.Music.Stop();
        }
    }

    public void PlaySFX(ESoundName name)
    {
        SoundData soundData = Array.Find(this.sfxSource.SFXSounds, x => x.soundName == name);
        this.sfxSource.SFX.PlayOneShot(soundData.soundClip);
    }
}
