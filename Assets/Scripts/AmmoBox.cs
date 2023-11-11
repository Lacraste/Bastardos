using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoAmount = 20;
    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {
            activeWeapon.AddAmmo(ammoAmount);
            Destroy(GetComponentInParent<Transform>().gameObject);
        }
    }
}
