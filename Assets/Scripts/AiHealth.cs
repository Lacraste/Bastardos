using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AiHealth : Health
{
    Ragdoll ragdoll;
    AiAgent agent;

    protected override void OnStart()
    {
        agent = GetComponent<AiAgent>();
        ragdoll = GetComponent<Ragdoll>();

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
            hitBox.bodyPart = (BodyPart)System.Enum.Parse(typeof(BodyPart), hitBox.gameObject.tag);
            hitBox.blood = GetComponent<HitBox>().blood;
        }
    }
    protected override void OnDeath()
    {
        agent.stateMachine.ChangeState(AiStateId.Death);
    }
}
