using UnityEngine;

namespace Gameplay
{
    public abstract class Key : MonoBehaviour
    {
        public Lock padlock;
        public bool isLocked { get; private set; } = true;

        public void SetIsLocked(bool isLocked)
        {
            this.isLocked = isLocked;
            padlock.CheckLocked();
        }
    }
}