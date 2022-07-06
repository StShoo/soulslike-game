using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTargetState : State
{
    private IdleState idleState;

    private void Awake()
    {
        idleState = GetComponent<IdleState>();
    }

    public override State Tick(MonsterManager monsterManager)
    {
        if (monsterManager.curentTarget != null)
        {
            idleState.FindATarget(monsterManager);
            MoveTowardsCurrentTarget(monsterManager);
            RotateTowardCurrentTarget(monsterManager);
            return this;
        }
        else
        {
            idleState.newIndexOfPoint = idleState.FindClosestIdleMovementPoint();
            idleState.stateChangedFlag = true;
            return idleState;
        }
    }

    private void MoveTowardsCurrentTarget(MonsterManager monsterManager)
    {
        monsterManager.animator.SetFloat("Vertical", 2, 0.2f, Time.deltaTime);
    }

    private void RotateTowardCurrentTarget(MonsterManager monsterManager)
    {
        monsterManager.monsterNavMeshAgent.enabled = true;
        
        monsterManager.monsterNavMeshAgent.SetDestination(monsterManager.curentTarget.transform.position);
        
        monsterManager.transform.rotation = Quaternion.Slerp(monsterManager.transform.rotation,
            monsterManager.monsterNavMeshAgent.transform.rotation,
            monsterManager.rotationSpeed/Time.deltaTime);
    }
}
