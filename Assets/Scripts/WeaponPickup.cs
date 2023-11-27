using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickup : Interectable
{
    public Weapon curentWeapon;
    public Weapon[] prefabs;
    bool find;
    private void Start()
    {
        find = false;
        SetCurrentWeapon();
    }

    public override void Interact()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        ActiveWeapon activeWeapon = player.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {
            
            //Destroy(prefab.gameObject);
            Weapon newWeapon = Instantiate(curentWeapon, activeWeapon.weaponSlots[(int)curentWeapon.slot]);
            find = false;
            foreach (var weapon in prefabs) 
            {
                if (activeWeapon.GetWeapon((int)curentWeapon.slot) == weapon)
                {
                    curentWeapon = weapon;
                    find = true;
                }
            }

            activeWeapon.Equip(newWeapon);
            if (!find) Destroy(gameObject);
            else
            {
                SetCurrentWeapon();
            }

        }
    }
    void SetCurrentWeapon()
    {
        if (GetComponent<BoxCollider>()) Destroy(GetComponent<BoxCollider>());
        GetComponent<MeshRenderer>().materials = curentWeapon.GetComponent<MeshRenderer>().materials;
        GetComponent<MeshFilter>().mesh = curentWeapon.GetComponent<MeshFilter>().mesh;

        gameObject.AddComponent<BoxCollider>();
    }

}
