using System;
using System.Collections;
using System.Collections.Generic;
using SG;
using UnityEngine;

public class IdleState : State
{
    private PursueTargetState pursueTargetState;
    private PlayerLocomotion playerLocomotion;

    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float detectionRadius = 30f;
    
    [SerializeField] private float distantDetectionRadius = 20f;
    [SerializeField] private float closeDetectionRadius = 10f;

    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetState>();
        playerLocomotion = FindObjectOfType<PlayerLocomotion>();
    }

    public override State Tick()
    {
        FindATarget();
        return this;
    }

    private void FindATarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,
            detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            PlayerManager player = colliders[i].transform.GetComponent<PlayerManager>();

            if (player != null)
            {
                Vector3 targetDirection = transform.position - player.transform.position;
                float distanceToTarget = Mathf.Sqrt(targetDirection.x * targetDirection.x +
                                                    targetDirection.z * targetDirection.z);
                
                if (distanceToTarget <= distantDetectionRadius && distanceToTarget > closeDetectionRadius)
                {
                    if (playerLocomotion.isSprinting)
                    {
                        Debug.Log("Monster Found you!");
                    }
                    else
                    {
                        Debug.Log("Monster is near but cant see you");
                    }
                }
                else if (distanceToTarget <= closeDetectionRadius)
                {
                    if (playerLocomotion.isWalking || playerLocomotion.isSprinting)
                    {
                        Debug.Log("Monster Found you!");
                    }
                    else
                    {
                        Debug.Log("Monster is very near but cant see you");
                    }
                }
                else
                {
                    Debug.Log("Monster cant see you");
                }
            }
        }
    }
}
