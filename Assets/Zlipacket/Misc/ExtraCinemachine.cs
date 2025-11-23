using Unity.Cinemachine;
using UnityEngine;

namespace Zlipacket.Misc
{
    public class ExtraCinemachine : MonoBehaviour
    {
        private CinemachineCamera cinemachineCam;

        private void Start()
        {
            cinemachineCam = GetComponent<CinemachineCamera>();
        }

        public void ChangeCamera(CinemachineCamera targetCamera)
        {
            cinemachineCam.Priority--;
            targetCamera.Priority++;
        }
    }
}