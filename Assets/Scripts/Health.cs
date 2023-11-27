using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 50;

    public float currentHealth;
   

    private void Start()
    {
        currentHealth = maxHealth;
        OnStart();
    }
    public float GetHealth()
    {
        return currentHealth;
    }
    public void TakeDamage(float damage)
    {
        OnDamage();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        OnDeath();
    }
    protected virtual void OnStart()
    {

    }
    protected virtual void OnDeath()
    {

    }
    protected virtual void OnDamage()
    {
    }
}
