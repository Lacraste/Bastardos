using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : Interectable
{
    public int ammoAmount = 20;
    /*private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {
            activeWeapon.AddAmmo(ammoAmount);
            Destroy(GetComponentInParent<Transform>().gameObject);
        }
    }*/
    public override void Interact()
    {
        var player = GameObject.FindGameObjectWithTag("Player").transform;
        player.GetComponent<ActiveWeapon>().AddAmmo(ammoAmount);
        Destroy(gameObject);
    }

}
