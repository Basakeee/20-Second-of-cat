using System;
using UnityEngine;
using UnityEngine.UI;
using Zlipacket.Tools;

namespace Gameplay
{
    public class LevelTimer : MonoBehaviour
    {
        [SerializeField] private ObjectTimer timerObject;
        [SerializeField] private Slider timerSlider;
        [SerializeField] private float duration = 20f;

        private void Start()
        {
            timerObject.SetDuration(duration);
            timerObject.StartTimer();
            timerObject.onTimerFinished.AddListener(TimeOut);
        }

        private void FixedUpdate()
        {
            if (timerObject.isRunning)
            {
                timerSlider.value = 1 - timerObject.percentage;
            }
        }

        public void TimeOut()
        {
            
        }
    }
}