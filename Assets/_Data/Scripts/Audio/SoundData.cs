using UnityEngine;

[System.Serializable]
public class SoundData
{
    public ESoundName soundName;
    public AudioClip soundClip;
    [Range(0.0001f, 1f)]
    public float volume = 1f;
}
