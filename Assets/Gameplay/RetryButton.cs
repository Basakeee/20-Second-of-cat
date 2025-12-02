using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class RetryButton : MonoBehaviour
    {
        public void Retry()
        {
            SceneManager.LoadScene("Level" + CatGameManager.Instance.currentLevel);
        }
    }
}