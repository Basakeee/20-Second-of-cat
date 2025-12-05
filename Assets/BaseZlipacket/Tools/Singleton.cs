using UnityEngine;

namespace BaseZlipacket.Tools
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance {get; private set;}

        public virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }
    }

    public abstract class PersistantSingleton<T> : Singleton<T> where T : Component
    {
        public override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}