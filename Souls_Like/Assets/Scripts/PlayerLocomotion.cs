using System;
using System.Collections;
using System.Collections.Generic;
using SG;
using UnityEngine;

namespace SG
{
    public class PlayerLocomotion : MonoBehaviour
    {
        Vector3 moveDirection;
        Transform cameraObject;
        InputManager inputManager;
        Rigidbody playerRigidbody;

        [HideInInspector] public Transform myTransform;
        [HideInInspector] public AnimatorManager animatorHandler;

        public bool isSprinting;

        [Header("Movement Stats")]
        [SerializeField]
        private float slowWalkingSpeed = 1;
        [SerializeField]
        private float walkingSpeed = 2;
        [SerializeField] 
        private float runningSpeed = 5;
        [SerializeField]
        private float rotationSpeed = 5;

        void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            inputManager = GetComponent<InputManager>();
            cameraObject = Camera.main.transform;
        }

        public void HandleAllMovement()
        {
            HandleMovement();
            HandleRotation();
        }
        
        private void HandleRotation()
        {
            Vector3 targetDirection = Vector3.zero;

            targetDirection = cameraObject.forward * inputManager.verticalInput;
            targetDirection += cameraObject.right * inputManager.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = transform.forward;
            }
            
            Quaternion targetRotations = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotations,
                rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }
        
        private void HandleMovement()
        {

            moveDirection = cameraObject.forward * inputManager.verticalInput;
            moveDirection += cameraObject.right * inputManager.horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (isSprinting)
            {
                moveDirection *= runningSpeed;
            }
            else
            {
                if (inputManager.moveAmount >= 0.5f)
                {
                    moveDirection *= walkingSpeed;
                }
                else
                {
                    moveDirection *= slowWalkingSpeed;
                }
            }

            Vector3 movementVelocity = moveDirection;
            playerRigidbody.velocity = movementVelocity;
        }
    }
}
