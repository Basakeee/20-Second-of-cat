using System;
using UnityEngine;
using UnityEngine.Events;
using Zlipacket.Scene;

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

        public void ChangeLevel(string levelName)
        {
            SceneController.Instance.LoadScene(levelName);
            
        }
    }
}