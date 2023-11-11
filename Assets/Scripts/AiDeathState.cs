using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{
    public void Enter(AiAgent agent)
    {
        agent.ragdoll.ActivateRagdoll();
        agent.navMeshAgent.enabled = false;
    }

    public void Exit(AiAgent agent)
    {
    }

    public AiStateId GetId()
    {
        return AiStateId.Death;
    }

    public void Update(AiAgent agent)
    {
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
