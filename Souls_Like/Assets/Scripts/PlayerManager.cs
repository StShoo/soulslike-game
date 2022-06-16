using System.Collections;
using System.Collections.Generic;
using SG;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SG
{
    public class PlayerManager : MonoBehaviour
    {
        private InputHandler inputHandler;
        private Animator anim;
    
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
        }
        
        void Update()
        {
            inputHandler.isInteracting = anim.GetBool("isInteracting");
            inputHandler.rollFlag = false;
        }
    }   
}
