using UnityEngine;

namespace _Data.Scripts.OrtherScripts
{
    public class DontDestroyOnLoad : MyMonoBehaviour
    {
        private static DontDestroyOnLoad _instance;
        public static DontDestroyOnLoad Instance { get => _instance; }

        protected override void Awake()
        {
            base.Awake();

            if (_instance != null && _instance != this)
            {
                Debug.LogWarning("Duplicate DontDestroyOnLoad detected, destroying the new one.");
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
