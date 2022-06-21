using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SG
{
    public class CameraManager : MonoBehaviour
    {
        private InputManager inputManager;
        
        public Transform targetTransform; // Object to follow
        public Transform cameraTransform; // Our Camera
        public Transform cameraPivot; // CamerasPivot
        
        private Vector3 cameraFollowVelocity = Vector3.zero;
        private Vector3 cameraVectorPosition;

        public float cameraFollowSpeed = 0.2f;
        public float cameraLookSpeed = 2;
        public float cameraPivotSpeed = 2;
        private float lookAngle;
        private float pivotAngle;
        
        public float minimumPivot = -35;
        public float maximumPivot = 35;
        
        private float defaultPosition;

        public LayerMask collisionLayers;
        public float cameraCollisionRadius = 0.2f;
        public float cameraCollisionOffSet = 0.2f;
        public float minimumCollisionOffSet = 0.2f;
    
        private void Awake()
        {
            targetTransform = FindObjectOfType<PlayerManager>().transform;
            inputManager = FindObjectOfType<InputManager>();
            cameraTransform = Camera.main.transform;
            defaultPosition = cameraTransform.localPosition.z;
        }

        public void HandleAllCameraMovement()
        {
            FollowTarget();
            RotateCamera();
            HandleCameraCollision();
        }

        private void FollowTarget()
        {
            Vector3 targetPosition = Vector3.SmoothDamp
                (transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
            
            transform.position = targetPosition;
        }

        private void RotateCamera()
        {
            Vector3 rotation;
            Quaternion targetRotation;
            
            lookAngle += (inputManager.cameraInputX * cameraLookSpeed);
            pivotAngle -= (inputManager.cameraInputY * cameraPivotSpeed);
            
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            rotation = Vector3.zero;
            
            rotation.y = lookAngle;
            targetRotation = Quaternion.Euler(rotation); 
            transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;
        
            targetRotation = Quaternion.Euler(rotation);
            cameraPivot.localRotation = targetRotation;
        }

        private void HandleCameraCollision()
        {
            float targetPosition = defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivot.position;
            direction.Normalize();
        
            if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction,
                    out hit, Mathf.Abs(targetPosition), collisionLayers))
            {
                float distance = Vector3.Distance(cameraPivot.position, hit.point);
                targetPosition = -(distance - cameraCollisionOffSet);
            }
        
            if (Mathf.Abs(targetPosition) < minimumCollisionOffSet)
            { 
                targetPosition =- minimumCollisionOffSet;
            }
        
            cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition,
                Time.deltaTime / 0.2f);
            cameraTransform.localPosition = cameraVectorPosition;
        }
    }
}

