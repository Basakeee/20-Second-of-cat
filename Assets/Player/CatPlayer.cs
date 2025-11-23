using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class CatPlayer : MonoBehaviour
    {
        private Rigidbody rb;
        private CatInputController inputController;
        
        public bool isAiming {get; private set;}
        public bool isIdle { get; private set; } = true;
        public Vector3 aimDirection {get; private set;}

        public float shootThreshold = 1f;
        public float maxDrawLength = 1000f;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            inputController = GetComponent<CatInputController>();
            
            inputController.onMouseLeftDown.AddListener(OnMouseDown);
            inputController.onMouseLeftUp.AddListener(OnMouseUp);
        }

        public void Update()
        {
            if (isAiming)
            {
                ProcessAim();
            }
        }

        private void ProcessAim()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(
                new Vector3(inputController.MousePosition.x, inputController.MousePosition.y, Camera.main.farClipPlane));
            var horizontalWorldPoint = new Vector3(mousePosition.x, transform.position.y, mousePosition.z);
            
            aimDirection = horizontalWorldPoint - transform.position;
            aimDirection = Vector3.ClampMagnitude(aimDirection, maxDrawLength);
            
            Debug.DrawRay(transform.position, aimDirection, Color.yellow);
            Debug.DrawRay(transform.position, horizontalWorldPoint, Color.blue);
            
            /*var rawDirection = Camera.main.ScreenToWorldPoint(inputController.MousePosition) - transform.position;
            aimDirection = new  Vector3(rawDirection.x, 0f, rawDirection.z);
            Vector3.ClampMagnitude(aimDirection, maxDrawLength);*/
        }

        private void OnMouseDown()
        {
            if (!CastMouseCickRaycast(out RaycastHit hit)) return;
            
            //Enter Aim State
            if (hit.transform.gameObject.CompareTag("Player") /*&& isIdle*/)
            {
                isAiming = true;
            }
        }

        private void OnMouseUp()
        {
            //If Aiming Shoot it and exit Aim State
            if (isAiming)
            {
                Debug.Log("AimDirection: " + aimDirection + ", Magnitute: " + aimDirection.magnitude);
                isAiming = false;
                Shoot();
                aimDirection = Vector3.zero;
            }
        }

        private void Shoot()
        {
            if (aimDirection == Vector3.zero || aimDirection.magnitude <= shootThreshold) return;
            
            rb.AddForce(aimDirection, ForceMode.Impulse);
        }
        
        private bool CastMouseCickRaycast(out RaycastHit raycastHit)
        {
            raycastHit = new RaycastHit();
            
            Vector3 sceneMousePositionNear = new Vector3(
                inputController.MousePosition.x,
                inputController.MousePosition.y,
                Camera.main.nearClipPlane);
            Vector3 sceneMousePositionFar = new Vector3(
                inputController.MousePosition.x,
                inputController.MousePosition.y,
                Camera.main.farClipPlane);
            
            Vector3 worldMousePositionNear = Camera.main.ScreenToWorldPoint(sceneMousePositionNear);
            Vector3 worldMousePositionFar = Camera.main.ScreenToWorldPoint(sceneMousePositionFar);

            //Debug.DrawRay(worldMousePositionNear, worldMousePositionFar - worldMousePositionNear, Color.green, 1f);
            if (Physics.Raycast(worldMousePositionNear, worldMousePositionFar - worldMousePositionNear, out RaycastHit hit, float.PositiveInfinity))
            {
                Debug.Log("Hit " + hit.transform.gameObject.name);
                raycastHit = hit;
                return true;
            }
            
            return false;
        }
    }
}