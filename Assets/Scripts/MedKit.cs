using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : Interectable
{
    public int healthToHeal = 30;
    public override void Interact()
    {
        base.Interact();
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<PlayerHealth>().Heal(healthToHeal);
        Destroy(gameObject);
    }
}
