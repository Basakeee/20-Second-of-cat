using System;
using System.Collections.Generic;
using UnityEngine;
using Zlipacket.Tools;

namespace Gameplay
{
    public class Door : Lock
    {
        private void Start()
        {
            onLock.AddListener(DoorClose);
            onUnlock.AddListener(DoorOpen);
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