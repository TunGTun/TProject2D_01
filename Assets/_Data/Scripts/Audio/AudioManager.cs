using UnityEngine;

public class AudioManager : MyMonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get => _instance; }

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
        base.Awake();
        if (AudioManager._instance != null) Debug.LogError("Only 1 AudioManager allow to exist");
        AudioManager._instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        //musicSource.clip = MusicSource.Music.clip;
        //musicSource.Play();
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
}
