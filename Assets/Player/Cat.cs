using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zlipacket.Managers;
using Zlipacket.Tools;

namespace Player
{
    public class Cat : MonoBehaviour
    {
        private Rigidbody rb;
        private LineRenderer lineRenderer;

        [HideInInspector] public bool isAiming = false;
        [HideInInspector] public bool isIdle = true;
        public Vector3 aimDirection {get; private set;}

        public float shootThreshold = 1f;
        public float maxDrawLength = 1000f;
        public float maxStrength = 50f;
        public float rotationSpeed = 720;

        [SerializeField] private AudioClip shootSfx;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
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
            if (!ZlipUtilities.CastMouseCickRaycast(PlayerController.Instance.inputController, out RaycastHit hit)) return;
            
            Vector3 horizontalWorldPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            aimDirection = horizontalWorldPoint - transform.position;
            aimDirection = Vector3.ClampMagnitude(aimDirection, maxDrawLength);
            aimDirection = -aimDirection;
            
            //If not pointing in the same direction, we want to rotate it and fast
            if (!ZlipUtilities.ApproximatelyWithMargin(Vector3.Dot(transform.forward, aimDirection), 1, 0.2f))
            {
                Quaternion toRotation = Quaternion.LookRotation(aimDirection, transform.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
            }
            
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

        public void OnMouseDown()
        {
            isAiming = true;
        }

        public void OnMouseUp()
        {
            //If Aiming Shoot it and exit Aim State
            if (isAiming)
            {
                /*Debug.Log("AimDirection: " + aimDirection + ", Magnitute: " + aimDirection.magnitude);
                Debug.Log("Distance: " + Vector3.Distance(transform.position, aimDirection));*/
                transform.rotation = Quaternion.LookRotation(aimDirection, transform.up);
                isAiming = false;
                lineRenderer.enabled = false;
                Shoot();
                aimDirection = Vector3.zero;
            }
        }

        private void Shoot()
        {
            if (aimDirection == Vector3.zero || aimDirection.magnitude <= shootThreshold) return;
            
            SoundFXManager.Instance.PlaySoundFX(shootSfx, transform);
            rb.AddForce(aimDirection.normalized * (aimDirection.magnitude / maxDrawLength * maxStrength), ForceMode.Impulse);
        }
    }
}