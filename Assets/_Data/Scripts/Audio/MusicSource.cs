using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicSource : MyMonoBehaviour
{
    [SerializeField] protected AudioSource music;
    public AudioSource Music => music;

    [SerializeField] protected SoundData[] musicSounds;
    public SoundData[] MusicSounds => musicSounds;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMusic();
    }

    protected virtual void LoadMusic()
    {
        if (music != null) return;
        this.music = GetComponent<AudioSource>();
        this.music.playOnAwake = true;
        this.music.loop = true;
        Debug.Log(transform.name + ": LoadMusic", gameObject);
    }
}
