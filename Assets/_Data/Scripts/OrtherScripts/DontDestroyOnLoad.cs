using UnityEngine;

namespace _Data.Scripts.OrtherScripts
{
    public class DontDestroyOnLoad : MyMonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}
