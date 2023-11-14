using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    [HideInInspector]
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Ragdoll ragdoll;
    public Transform player;
    public AiWeapons weapons;

    public void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;

        ragdoll = GetComponent<Ragdoll>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        weapons = GetComponent<AiWeapons>();
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiAttackPlayerState());

        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    public void Update()
    {
        stateMachine.Update();
    }
}
