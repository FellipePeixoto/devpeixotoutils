using UnityEngine;

namespace DevPeixoto.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {

        private static T _instance;
        private static bool _quitting = false;
        [HideInInspector] public bool _duplicatedInstance = false;

        public static T Instance
        {
            get
            {
                if (_instance == null && !_quitting)
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject(typeof(T).ToString());
                        _instance = go.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = gameObject.GetComponent<T>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            else if (_instance.GetInstanceID() != this.GetInstanceID())
            {
                _duplicatedInstance = true;
                Destroy(this.gameObject);
                string log = string.Format("Instance of {0} already exists, removing {1}", GetType().FullName, ToString());
                Debug.LogWarning(log);
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _quitting = true;
        }
    }
}