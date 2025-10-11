using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SFXSource : MyMonoBehaviour
{
    [SerializeField] protected AudioSource sfx;
    public AudioSource SFX => sfx;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSFXSource();
    }

    protected virtual void LoadSFXSource()
    {
        if (sfx != null) return;
        sfx = GetComponent<AudioSource>();

        Debug.Log(transform.name + ": LoadSFXSource", gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

    protected override void Start()
    {
        
    }
}
