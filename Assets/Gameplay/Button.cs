using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Button : Key
    {
        private List<GameObject> overlapObjects = new List<GameObject>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (!overlapObjects.Contains(other.gameObject))
            {
                overlapObjects.Add(other.gameObject);
            }
            SetIsLocked(!CheckIsButtonPressed());
        }

        private void OnTriggerExit(Collider other)
        {
            if (overlapObjects.Contains(other.gameObject))
            {
                overlapObjects.Remove(other.gameObject);
            }
            SetIsLocked(!CheckIsButtonPressed());
        }

        private bool CheckIsButtonPressed()
        {
            foreach (GameObject overlapObject in overlapObjects)
            {
                if (overlapObject.TryGetComponent(out Box box))
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}