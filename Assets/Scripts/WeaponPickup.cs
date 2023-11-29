using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickup : Interectable
{
    public GameObject curentWeapon;
    public GameObject[] prefabs;
    bool find;
    private void Start()
    {
        find = false;
        SetCurrentWeapon(curentWeapon.GetComponent<Weapon>());
    }

    public override void Interact()
    {
        base.Interact();
        var player = GameObject.FindGameObjectWithTag("Player");
        ActiveWeapon activeWeapon = player.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {
            Weapon cWeapon = curentWeapon.GetComponent<Weapon>();
            var newWeapon = curentWeapon;
            find = false;
            SetCurrentWeapon(activeWeapon.GetWeapon((int)cWeapon.slot));

            var InstanceWeapon = Instantiate(newWeapon, activeWeapon.weaponSlots[(int)cWeapon.slot]);
            activeWeapon.Equip(InstanceWeapon.GetComponent<Weapon>());
        }
    }
    public void SetCurrentWeapon(Weapon w)
    {
        foreach (var weapon in prefabs)
        {
            if (w.weaponName == weapon.GetComponent<Weapon>().weaponName)
            {
                curentWeapon = weapon;
                find = true;
            }
        }
        if (!find) Destroy(gameObject);
        else
        {
            ChangeMesh();
        }
    }
    public void ChangeMesh()
    {
        GetComponent<MeshRenderer>().materials = curentWeapon.GetComponent<MeshRenderer>().sharedMaterials;
        GetComponent<MeshFilter>().mesh = curentWeapon.GetComponent<MeshFilter>().sharedMesh;
        GetComponent<BoxCollider>().size = curentWeapon.GetComponent<BoxCollider>().size;
        GetComponent<BoxCollider>().center = curentWeapon.GetComponent<BoxCollider>().center;
    }

}
