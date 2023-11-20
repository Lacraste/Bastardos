using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AiAttackPlayerState : AiState
{
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = 15f;
        agent.weapons.SetFiring(true);
    }

    public void Exit(AiAgent agent)
    {

    }

    public AiStateId GetId()
    {
        return AiStateId.AttackPlayer;
    }

    public void Update(AiAgent agent)
    {
        agent.navMeshAgent.destination = agent.player.position;
        agent.weapons.SetTargetPosition(agent.player.position);

        Vector3 targetPos = new Vector3(agent.weapons.targetObject.position.x, agent.transform.position.y, agent.weapons.targetObject.position.z);
        Vector3 lookDir = targetPos - agent.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookDir);

        // Rotação apenas no eixo Y (vertical)
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f), 5 * Time.deltaTime);
    }
}
