using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiState
{
    Health health;
    public void Enter(AiAgent agent)
    {
        health = agent.GetComponent<Health>();
    }

    public void Exit(AiAgent agent)
    {
    }

    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }

    public void Update(AiAgent agent)
    {
        Vector3 playerDirection = agent.player.position - agent.transform.position;
        if(playerDirection.magnitude > agent.config.maxSightDistance)
        {
            return;
        }
        Vector3 agentDirection = agent.transform.forward;
        playerDirection.Normalize();
        float dotproduct = Vector3.Dot(playerDirection, agentDirection);
        if(dotproduct > 0 )
        {
            agent.stateMachine.ChangeState(AiStateId.AttackPlayer);
        }
        if (health.GetHealth() < health.maxHealth)
        {
            agent.stateMachine.ChangeState(AiStateId.AttackPlayer);
        }

    }
  
}
