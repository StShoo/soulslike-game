using System;
using System.Collections;
using System.Collections.Generic;
using SG;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator animator;

    int horizontal;
    int vertical;
    
    public bool canRotate;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValue(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        float snappedHorizontal;
        float snappedVertical;

        #region Horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if(horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if(horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if(horizontalMovement < 0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion
        
        #region Vertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if(verticalMovement > 0.55f)
        {
            snappedVertical = 1;
        }
        else if(verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if(verticalMovement < 0.55f)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion

        if (isSprinting)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2;
        }
        
        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

    // public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    // {
    //     animator.applyRootMotion = isInteracting;
    //     animator.SetBool("isInteracting", isInteracting);
    //     animator.CrossFade(targetAnim, 0.2f);
    // }

    // public void CanRotate()
    // {
    //     canRotate = true;
    // }
    //
    // public void StopRotation()
    // {
    //     canRotate = false;
    // }

    // public void OnAnimatorMove()
    // {
    //     if (inputManager.isInteracting == false)
    //         return;
    //
    //     float delta = Time.deltaTime;
    //     playerLocomotion.rigidbody.drag = 0;
    //     Vector3 deltaPosition = anim.deltaPosition;
    //     deltaPosition.y = 0;
    //     Vector3 velosity = deltaPosition / delta;
    //     playerLocomotion.rigidbody.velocity = velosity;
    // }
}
