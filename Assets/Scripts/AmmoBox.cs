using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : Interectable
{
    public int ammoAmount = 20;
    public override void Interact()
    {
        base.Interact();
        var player = GameObject.FindGameObjectWithTag("Player").transform;
        player.GetComponent<ActiveWeapon>().AddAmmo(ammoAmount);
        Destroy(gameObject);
    }

}
