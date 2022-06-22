using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private PursueTargetState pursueTargetState;
    
    [SerializeField] private bool hasTarget;

    private void Awake()
    {
        pursueTargetState = GetComponent<PursueTargetState>();
    }

    public override State Tick()
    {
        if (hasTarget)
        {
            Debug.Log("We have found a target!");
            return pursueTargetState;
        }else
        {
            Debug.Log("We have lost a target!");
            return this;
        }
    }
}
