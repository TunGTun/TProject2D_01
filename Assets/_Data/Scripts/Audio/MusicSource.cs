using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MusicSource : MyMonoBehaviour
{
    [SerializeField] protected AudioSource music;
    public AudioSource Music => music;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAudioSource();
    }

    protected virtual void LoadAudioSource()
    {
        if (music != null) return;
        this.music = GetComponent<AudioSource>();

        Debug.Log(transform.name + ": LoadAudioSource", gameObject);
    }

    protected override void Start()
    {
        music.clip = Music.clip;
        music.Play();
    }
}
