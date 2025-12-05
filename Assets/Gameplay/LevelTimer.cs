using System;
using BaseZlipacket.Scene;
using BaseZlipacket.Tools;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class LevelTimer : MonoBehaviour
    {
        [SerializeField] private ObjectTimer timerObject;
        //[SerializeField] private Slider timerSlider;
        [SerializeField] private TextMeshProUGUI timerText;
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
                timerText.SetText(Mathf.FloorToInt(duration - timerObject.timeElapsed).ToString());
            }
        }

        public void TimeOut()
        {
            SceneController.Instance.LoadScene("GameOverScene");
        }
    }
}