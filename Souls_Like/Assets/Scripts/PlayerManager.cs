using System;
using System.Collections;
using System.Collections.Generic;
using SG;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SG
{
    public class PlayerManager : MonoBehaviour
    {
        private InputManager inputManager;
        private PlayerLocomotion playerLocomotion;
        private Animator anim;
        private CameraManager cameraManager;
    
        void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            cameraManager = FindObjectOfType<CameraManager>();
        }

        private void Update()
        {
            inputManager.HandleAllInputs();
        }

        private void FixedUpdate()
        {
            playerLocomotion.HandleAllMovement();
        }

        private void LateUpdate()
        {
            cameraManager.HandleAllCameraMovement();
        }
    }   
}
