using BaseZlipacket.Tools;
using UnityEngine;

namespace Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        public CatInputController inputController {get; private set;}
        private Cat currentCat;
        
        [SerializeField] public Texture2D curserSprite;
        [SerializeField] public Texture2D hoverSprite;
        
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
            if (hoverSprite == null) return;
            Vector2 cursorHotSpot = new Vector2(hoverSprite.width / 2, hoverSprite.height / 2);
            Cursor.SetCursor(hoverSprite, cursorHotSpot, CursorMode.Auto);
            
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
            if (curserSprite == null) return;
            Vector2 cursorHotSpot = new Vector2(curserSprite.width / 2, curserSprite.height / 2);
            Cursor.SetCursor(curserSprite, cursorHotSpot, CursorMode.Auto);
            
            //If Aiming Shoot it and exit Aim State
            if (currentCat != null)
            {
                currentCat.OnMouseUp();
                currentCat = null;
            }
        }
    }
}