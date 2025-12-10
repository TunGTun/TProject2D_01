using UnityEngine;

public class Manager : MyMonoBehaviour
{
    [SerializeField] protected ESoundName sceneSoundName = ESoundName.None;

    protected override void Start()
    {
        base.Start();
        AudioManager.Instance.PlayOneShotMusic(sceneSoundName);
    }
}
