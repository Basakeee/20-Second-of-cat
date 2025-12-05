using BaseZlipacket.Scene;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class RetryButton : MonoBehaviour
    {
        public void Retry()
        {
            SceneController.Instance.LoadScene("Level" + CatGameManager.Instance.currentLevel);
        }
    }
}