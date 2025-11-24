using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class CatPlayer : MonoBehaviour
    {
        private Rigidbody rb;
        private CatInputController inputController;
        private LineRenderer lineRenderer;
        
        public bool isAiming {get; private set;}
        public bool isIdle {get; private set;} = true;
        public Vector3 aimDirection {get; private set;}

        public float shootThreshold = 1f;
        public float maxDrawLength = 1000f;
        public float maxStrength = 50f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            inputController = GetComponent<CatInputController>();
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            inputController.onMouseLeftDown.AddListener(OnMouseDown);
            inputController.onMouseLeftUp.AddListener(OnMouseUp);
            lineRenderer.enabled = false;
        }

        public void FixedUpdate()
        {
            if (isAiming)
            {
                ProcessAim();
            }
        }

        private void ProcessAim()
        {
            if (!CastMouseCickRaycast(out RaycastHit hit)) return;
            
            Vector3 horizontalWorldPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            aimDirection = horizontalWorldPoint - transform.position;
            aimDirection = Vector3.ClampMagnitude(aimDirection, maxDrawLength);
            aimDirection = -aimDirection;
            
            Debug.DrawRay(transform.position, aimDirection, Color.yellow);
            DrawLine(aimDirection + transform.position);
        }
        
        private void DrawLine(Vector3 worldPoint) {
            Vector3[] positions = {
                transform.position,
                worldPoint};
            lineRenderer.SetPositions(positions);
            lineRenderer.enabled = true;
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
                Debug.Log("Distance: " + Vector3.Distance(transform.position, aimDirection));
                isAiming = false;
                lineRenderer.enabled = false;
                Shoot();
                aimDirection = Vector3.zero;
            }
        }

        private void Shoot()
        {
            if (aimDirection == Vector3.zero || aimDirection.magnitude <= shootThreshold) return;
            
            rb.AddForce(aimDirection.normalized * (aimDirection.magnitude / maxDrawLength * maxStrength), ForceMode.Impulse);
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
        
        /*[SerializeField] private float shotPower;
        [SerializeField] private float stopVelocity = .05f; //The velocity below which the rigidbody will be considered as stopped

        [SerializeField] private LineRenderer lineRenderer;

        private bool isIdle;
        private bool isAiming;

        private Rigidbody rigidbody;

        private void Awake() {
            rigidbody = GetComponent<Rigidbody>();

            isAiming = false;
            lineRenderer.enabled = false;
        }

        private void FixedUpdate() {
            if(rigidbody.velocity.magnitude < stopVelocity) {
                Stop();
            }

            ProcessAim();
        }

        private void OnMouseDown() {
            if (isIdle) {
                isAiming = true;
            }
        }

        private void ProcessAim() {
            if(!isAiming || !isIdle) {
                return;
            }

            Vector3? worldPoint = CastMouseClickRay();

            if (!worldPoint.HasValue) {
                return;
            }

            DrawLine(worldPoint.Value);

            if (Input.GetMouseButtonUp(0)) {
                Shoot(worldPoint.Value);
            }
        }

        private void Shoot(Vector3 worldPoint) {
            isAiming = false;
            lineRenderer.enabled = false;

            Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);

            Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
            float strength = Vector3.Distance(transform.position, horizontalWorldPoint);

            rigidbody.AddForce(direction * strength * shotPower);
            isIdle = false;
        }

        private void DrawLine(Vector3 worldPoint) {
            Vector3[] positions = {
                transform.position,
                worldPoint};
            lineRenderer.SetPositions(positions);
            lineRenderer.enabled = true;
        }

        private void Stop() {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            isIdle = true;
        }

        private Vector3? CastMouseClickRay() {
            Vector3 screenMousePosFar = new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.farClipPlane);
            Vector3 screenMousePosNear = new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.nearClipPlane);
            Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
            Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
            RaycastHit hit;
            if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity)) {
                return hit.point;
            } else {
                return null;
            }
        }*/
    }
}