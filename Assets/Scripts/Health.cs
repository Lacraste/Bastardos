using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 50;

    private float currentHealth;
    Ragdoll ragdoll;
    AiAgent agent;

    private void Start()
    {
        agent = GetComponent<AiAgent>();
        ragdoll = GetComponent<Ragdoll>();
        currentHealth = maxHealth;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
            hitBox.bodyPart = (BodyPart)System.Enum.Parse(typeof(BodyPart), hitBox.gameObject.tag);
            hitBox.blood = GetComponent<HitBox>().blood;
        }
    }
    public float GetHealth()
    {
        return currentHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        agent.stateMachine.ChangeState(AiStateId.Death);
    }
    protected virtual void OnStart()
    {

    }
    protected virtual void OnDeath()
    {

    }
    protected virtual void OnDamage()
    {
        ,
    }
}
