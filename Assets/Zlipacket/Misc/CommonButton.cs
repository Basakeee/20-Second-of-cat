using UnityEngine;
using Zlipacket.Scene;

namespace Zlipacket.Misc
{
    public class CommonButton : MonoBehaviour
    {
        public void ChangeToScene(string sceneName)
        {
            SceneController.Instance.LoadScene(sceneName);
        }
        
        public void Quit()
        {
            Application.Quit();
        }
    }
}