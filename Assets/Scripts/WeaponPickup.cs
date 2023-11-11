using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon prefab;
    //public GameObject activeHud;
    //public GameObject deactivateHud1, deactivateHud2;
    public Vector3 pos;

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {
           /* activeHud.SetActive(true);
            deactivateHud1.SetActive(false);
            deactivateHud2.SetActive(false);*/
            Weapon newWeapon = Instantiate(prefab,activeWeapon.weaponSlots[(int)prefab.slot]);

            activeWeapon.Equip(newWeapon);
        }
    }
}
