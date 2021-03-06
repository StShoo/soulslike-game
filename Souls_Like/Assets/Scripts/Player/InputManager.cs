using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class InputManager : MonoBehaviour
    {
        private PlayerControls playerControls;
        private PlayerLocomotion playerLocomotion;
        private CameraManager cameraManager;
        private AnimatorManager animatorManager;
        
        public Vector2 movementInput;
        public Vector2 cameraInput;
        
        public float horizontalInput;
        public float verticalInput;
        public float moveAmount;
        
        public float cameraInputX;
        public float cameraInputY;
        
        public bool shiftInput;
        public bool ctrlInput;
        
        // public bool rollFlag;
        // public float rollInputTimer;
        // public bool sprintFlag;
        // public bool isInteracting;

        private void Awake()
        {
            animatorManager = GetComponentInChildren<AnimatorManager>();
            cameraManager = GetComponent<CameraManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();
                playerControls.PlayerMovment.Movement.performed += i
                    => movementInput = i.ReadValue<Vector2>();

                playerControls.PlayerMovment.Camera.performed += i => cameraInput =
                    i.ReadValue<Vector2>();

                playerControls.PlayerActions.Sprint.performed += i => shiftInput = true;
                playerControls.PlayerActions.Sprint.canceled += i => shiftInput = false;
                
                playerControls.PlayerActions.Crouch.performed += i => ctrlInput = true;
                playerControls.PlayerActions.Crouch.canceled += i => ctrlInput = false;
            }
            
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        public void HandleAllInputs()
        {
            HandleMovementInput();
            HandleSprintingInput();
            HandleCrouchingInput();
        }
        
        private void HandleMovementInput()
        {
            horizontalInput = movementInput.x;
            verticalInput = movementInput.y;
            
            cameraInputX = cameraInput.x;
            cameraInputY = cameraInput.y;
            
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
            animatorManager.UpdateAnimatorValue(0, moveAmount,
                playerLocomotion.isSprinting);
        }
        
        private void HandleSprintingInput()
        {
            if (shiftInput && moveAmount > 0.5f)
            {
                playerLocomotion.isSprinting = true;
            }
            else
            {
                playerLocomotion.isSprinting = false;
            }
        }

        private void HandleCrouchingInput()
        {
            playerLocomotion.isCrouching = ctrlInput;
        }
    }
}