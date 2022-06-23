using System;
using System.Collections;
using System.Collections.Generic;
using SG;
using UnityEngine;

public class IdleState : State
{
    private PursueTargetState pursueTargetState;
    private PlayerLocomotion playerLocomotion;
    private Animator animator;

    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float detectionRadius = 30f;
    
    [SerializeField] private float distantDetectionRadius = 20f;
    [SerializeField] private float closeDetectionRadius = 10f;

    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetState>();
        playerLocomotion = FindObjectOfType<PlayerLocomotion>();
        animator = GetComponentInParent<Animator>();
    }

    public override State Tick(MonsterManager monsterManager)
    {
        if (monsterManager.curentTarget != null)
        {
            return pursueTargetState;
        }
        else
        {
            FindATarget(monsterManager);
            StopMovement();
            return this;
        }
    }

    public void FindATarget(MonsterManager monsterManager)
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
                        //Monster Found you!
                        monsterManager.curentTarget = player;
                        Debug.Log("Monster Found you!");
                    }
                    else
                    {
                        //Monster is near but can`t see you
                        monsterManager.curentTarget = null;
                        Debug.Log("Monster is near but can`t see you");
                    }
                }
                else if (distanceToTarget <= closeDetectionRadius)
                {
                    if (playerLocomotion.isWalking || playerLocomotion.isSprinting)
                    {
                        //Monster Found you!
                        monsterManager.curentTarget = player;
                        Debug.Log("Monster Found you!");
                    }
                    else
                    {
                        //Monster is very near but can`t see you
                        monsterManager.curentTarget = null;
                        Debug.Log("Monster is very near but can`t see you");
                    }
                }
                else
                {
                    //Monster can`t see you
                    monsterManager.curentTarget = null;
                    Debug.Log("Monster can`t see you");
                }
            }
        }
    }

    private void StopMovement()
    {
        animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
    }
}
