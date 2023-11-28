using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public UnityEvent OnPlayerDeath;
    public TextMeshProUGUI lifeText;
    public Animator lifeIcon;
    public Image bloodVignette;
    protected override void OnStart()
    {
        lifeText.text = currentHealth.ToString();
    }
    protected override void OnDeath()
    {
        OnPlayerDeath?.Invoke();
    }
    protected override void OnDamage()
    {
        var color = bloodVignette.color;
        bloodVignette.color = new Color(color.r, color.g,color.b,(maxHealth/currentHealth) * 0.1f);
        if (maxHealth == currentHealth) bloodVignette.color = new Color(color.r, color.g, color.b, 0);
        lifeIcon.SetTrigger("Damage");
        lifeText.text = currentHealth.ToString();
    }
    public void Heal(int heal)
    {
        if(currentHealth + heal< maxHealth) currentHealth += heal;
        else
        {
            currentHealth = maxHealth;
        }
        lifeText.text = currentHealth.ToString();
        var color = bloodVignette.color;
        bloodVignette.color = new Color(color.r, color.g, color.b, (maxHealth / currentHealth) * 0.1f);
        if (maxHealth == currentHealth) bloodVignette.color = new Color(color.r, color.g, color.b, 0);
    }
}
