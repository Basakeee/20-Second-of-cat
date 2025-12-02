using System;
using Managers;
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

        public void NextIntermission(string intermissionLevel)
        {
            if (string.IsNullOrEmpty(intermissionLevel))
                intermissionLevel = "IntermissionScene";
            SceneController.Instance.LoadScene(intermissionLevel);
        }

        public void NextLevel()
        {
            CatGameManager.Instance.NextLevel();
        }
    }
}