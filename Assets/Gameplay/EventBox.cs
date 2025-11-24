using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class EventBox : MonoBehaviour
    {
        public UnityEvent onPlayerOverlapped;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onPlayerOverlapped?.Invoke();
            }
        }
    }
}