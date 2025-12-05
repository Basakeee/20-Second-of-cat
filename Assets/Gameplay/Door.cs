using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Door : Lock
    {
        [SerializeField] private Quaternion openRotation;
        private Quaternion closeRotation;
        [SerializeField] private float stepDuration = 1/60f;
        private float openPercentage = 0f;
        
        private void Start()
        {
            closeRotation = transform.rotation;
            onLock.AddListener(DoorClose);
            onUnlock.AddListener(DoorOpen);
        }

        private void FixedUpdate()
        {
            if (isOpen && openPercentage < 1f)
            {
                openPercentage += stepDuration;
                openPercentage = Mathf.Clamp(openPercentage, 0f, 1f);
                transform.rotation = Quaternion.Lerp(closeRotation, openRotation, openPercentage);
            }
            else if (!isOpen && openPercentage > 0f)
            {
                openPercentage -= stepDuration;
                openPercentage = Mathf.Clamp(openPercentage, 0f, 1f);
                transform.rotation = Quaternion.Lerp(closeRotation, openRotation, openPercentage);
            }
        }

        private void DoorOpen()
        {
            Debug.Log("DoorOpen");
        }

        private void DoorClose()
        {
            Debug.Log("DoorClose");
        }
    }
}