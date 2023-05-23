using System;
using System.Collections;
using System.Collections.Generic;
using SG;
using UnityEngine;

public class IdleState : State
{
    public Transform[] movementPathPoints;

    protected PursueTargetState pursueTargetState;
    protected PlayerLocomotion playerLocomotion;
    protected Animator animator;
    
    [SerializeField] protected LayerMask detectionLayer;
    [SerializeField] protected float detectionRadius = 30f;
    
    [SerializeField] protected float distantDetectionRadius = 20f;
    [SerializeField] protected float closeDetectionRadius = 10f;

    public bool stateChangedFlag;
    public int newIndexOfPoint;

    protected int i;
    protected void Awake()
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
            MoveTowardsNextIdleMovementPoint(monsterManager);
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

    protected virtual void MoveTowardsNextIdleMovementPoint(MonsterManager monsterManager)
    {
        if (stateChangedFlag)
        {
            i = newIndexOfPoint;
            stateChangedFlag = false;
        }
        Debug.Log("Index: " + i);

        monsterManager.animator.SetFloat("Vertical", 1, 0.2f, Time.deltaTime);
        
        monsterManager.monsterNavMeshAgent.enabled = true;
        monsterManager.monsterNavMeshAgent.SetDestination(movementPathPoints[i].transform.position);
        
        monsterManager.transform.rotation = Quaternion.Slerp(monsterManager.transform.rotation,
            monsterManager.monsterNavMeshAgent.transform.rotation,
            monsterManager.rotationSpeed/Time.deltaTime);
        
        Vector3 pointDirection = transform.position - movementPathPoints[i].transform.position;
        float distanceToPoint = Mathf.Sqrt(pointDirection.x * pointDirection.x +
                                           pointDirection.z * pointDirection.z);
        
        
        if (distanceToPoint <= 1f)
        {
            if (i != movementPathPoints.Length - 1)
            {
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }

    public int FindClosestIdleMovementPoint()
    {
        int indexOfClosestPoint = 0;
        float minDistanceToPoint = 10000f;
        for (int j = 0; j < movementPathPoints.Length; j++)
        {

            Vector3 pointDirection = transform.position - movementPathPoints[j].transform.position;
            float distanceToPoint = Mathf.Sqrt(pointDirection.x * pointDirection.x +
                                               pointDirection.z * pointDirection.z);

            if (minDistanceToPoint > distanceToPoint)
            {
                minDistanceToPoint = distanceToPoint;
                indexOfClosestPoint = j;
            }
        }
        return indexOfClosestPoint;
    }

    protected void StopMovement()
    {
        animator.SetFloat("Vertical", 0, 0f, Time.deltaTime);
    }
}
