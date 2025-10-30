using UnityEngine;
using UnityEngine.Audio;

public class VolumeSetting : MyMonoBehaviour
{
    [Header("VolumeSetting")]
    [SerializeField] protected AudioMixer audioMixer;
    public AudioMixer AudioMixer => audioMixer;
}
