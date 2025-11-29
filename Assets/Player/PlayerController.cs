using UnityEngine;
using Zlipacket.Tools;

namespace Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        public CatInputController inputController {get; private set;}
        private Cat currentCat;
        
        public override void Awake()
        {
            base.Awake();
            inputController = GetComponent<CatInputController>();
        }

        private void Start()
        {
            inputController.onMouseLeftDown.AddListener(OnMouseDown);
            inputController.onMouseLeftUp.AddListener(OnMouseUp);
        }
        
        private void OnMouseDown()
        {
            if (!ZlipUtilities.CastMouseCickRaycast(inputController, out RaycastHit hit)) return;
            
            //Enter Aim State
            if (hit.transform.gameObject.CompareTag("Player") /*&& isIdle*/)
            {
                if (hit.transform.TryGetComponent(out Cat cat))
                {
                    cat.OnMouseDown();
                    currentCat = cat;
                }
            }
        }

        private void OnMouseUp()
        {
            //If Aiming Shoot it and exit Aim State
            if (currentCat != null)
            {
                currentCat.OnMouseUp();
                currentCat = null;
            }
        }
    }
}