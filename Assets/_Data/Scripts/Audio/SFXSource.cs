using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SFXSource : MyMonoBehaviour
{
    [SerializeField] protected AudioSource sfx;
    public AudioSource SFX => sfx;

    [SerializeField] protected SoundData[] sfxSounds;
    public SoundData[] SFXSounds => sfxSounds;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSFXSource();
    }

    protected virtual void LoadSFXSource()
    {
        if (sfx != null) return;
        sfx = GetComponent<AudioSource>();
        this.sfx.playOnAwake = true;
        this.sfx.loop = false;
        Debug.Log(transform.name + ": LoadSFXSource", gameObject);
    }
}
