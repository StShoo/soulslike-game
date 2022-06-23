using System;
using System.Collections;
using System.Collections.Generic;
using SG;
using UnityEngine;
using UnityEngine.AI;

public class MonsterManager : MonoBehaviour
{
    [Header("States")]
    public State startingState;
    [SerializeField]
    private State currentState;

    [Header("Current Target")] 
    public PlayerManager curentTarget;

    [Header("Rotation Speed")] 
    public float rotationSpeed = 5f;
    
    public Animator animator;
    public NavMeshAgent monsterNavMeshAgent;
    public Rigidbody monsterRigibody;

    private void Awake()
    {
        currentState = startingState;
        animator = GetComponent<Animator>();
        monsterRigibody = GetComponent<Rigidbody>();
        monsterNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void Update()
    {
        monsterNavMeshAgent.velocity = Vector3.zero;
    }

    private void HandleStateMachine()
    {
        State nextState;
        
        if (currentState != null)
        {
            nextState = currentState.Tick(this);

            if (nextState != null)
            {
                currentState = nextState;
            }
        }
    }
}
