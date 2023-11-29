using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 50;
    private bool dead;
    public float currentHealth;
    public AudioClip[] deathSounds;
    public AudioSource audioSource;

    private void Start()
    {
        if (GetComponent<AudioSource>()) audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;
        OnStart();
    }
    public float GetHealth()
    {
        return currentHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        OnDamage();
        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            Die();
        }
    }
    public void Die()
    {
        if (deathSounds != null)
        {
            audioSource.PlayOneShot(AudioUtils.RandomAudioClip(deathSounds));
        }
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
