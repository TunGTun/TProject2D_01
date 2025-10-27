using UnityEngine;

public class AudioManager : MySingleton<AudioManager>
{
    [Header("--AudioSource--")]
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

    protected override void Awake()
    {
        
    }

    protected override void Start()
    {
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
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        MusicSource.Music.clip = clip;
        MusicSource.Music.loop = loop;
        MusicSource.Music.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlaySFX(clip);
    }

}
