using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public abstract class Lock : MonoBehaviour
    {
        public bool isOpen;
        
        public List<Key> keyList;
        public UnityEvent onLock;
        public UnityEvent onUnlock;
        
        public bool CheckLocked()
        {
            foreach (Key key in keyList)
            {
                if (key.isLocked)
                {
                    if (isOpen)
                    {
                        onLock?.Invoke();
                        isOpen = false;
                    }
                    return true;
                }
            }
            
            onUnlock?.Invoke();
            isOpen = true;
            return false;
        }
    }
}