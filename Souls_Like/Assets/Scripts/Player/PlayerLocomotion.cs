using System;
using System.Collections;
using System.Collections.Generic;
using SG;
using UnityEngine;

namespace SG
{
    public class PlayerLocomotion : MonoBehaviour
    {
        private Vector3 moveDirection;
        private Transform cameraObject;
        private InputManager inputManager;
        private PlayerManager playerManager;
        private AnimatorManager animatorManager;
        public Rigidbody playerRigidbody;

        [HideInInspector] public Transform myTransform;
        [HideInInspector] public AnimatorManager animatorHandler;

        [Header("Locomotion Status")]
        public bool isStanding;
        public bool isWalking;
        public bool isSprinting;
        public bool isCrouching;

        [Header("Movement Stats")]
        [SerializeField]
        private float slowWalkingSpeed = 1;
        [SerializeField]
        private float walkingSpeed = 2;
        [SerializeField] 
        private float runningSpeed = 5;
        [SerializeField]
        private float rotationSpeed = 5;

        [Header("Movement Sounds")]
        public AudioSource walkingSteps;
        public AudioSource crouchingSteps;
        public AudioSource runningSteps;

        void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            inputManager = GetComponent<InputManager>();
            playerManager = GetComponent<PlayerManager>();
            animatorManager = GetComponentInChildren<AnimatorManager>();
            cameraObject = Camera.main.transform;
            isStanding = true;
        }

        public void HandleAllMovement()
        {
            if (playerManager.isInteracting)
            {
                return;
            }

            HandleCrouching();
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

            if (!isSprinting && !isCrouching && inputManager.moveAmount != 0)
            {
                isWalking = true;
                walkingSteps.Pause();
            }
            else
            {
                isWalking = false;

                runningSteps.Pause();
            }

            if (isSprinting && !isCrouching)
            {
                moveDirection *= runningSpeed;

                walkingSteps.Pause();
                runningSteps.Play();

            }
            else if(isCrouching)
            {
                if (inputManager.moveAmount >= 0.5f)
                {
                    moveDirection *= slowWalkingSpeed;
                }
                else
                {
                    moveDirection *= slowWalkingSpeed/2;
                }

                walkingSteps.Pause();
                runningSteps.Pause();
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

                walkingSteps.Play();
                runningSteps.Pause();
            }
            

            Vector3 movementVelocity = moveDirection;
            playerRigidbody.velocity = movementVelocity;
        }

        private void HandleCrouching()
        {
            if (isCrouching)
            {
                if (isStanding)
                {
                    animatorManager.PlayTargetAnimation("Stand To Crouched", true);
                    isStanding = false;
                }
            }
            else
            {
                if (!isStanding)
                {
                    animatorManager.PlayTargetAnimation("Crouched To Stand", true);
                    isStanding = true;
                }
            }
        }
    }
}
