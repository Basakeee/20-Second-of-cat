using BaseZlipacket.Scene;
using BaseZlipacket.Tools;
using UnityEngine;

namespace Managers
{
    public class CatGameManager : PersistantSingleton<CatGameManager>
    {
        public int currentLevel = 1;
        public int maxLevel = 3;

        public void NextLevel()
        {
            currentLevel++;
            if (currentLevel > maxLevel)
            {
                SceneController.Instance.LoadScene("WinScene");
            }
            else
            {
                SceneController.Instance.LoadScene("Level" + currentLevel);
            }
        }
    }
}