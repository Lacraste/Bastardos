using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActiveWeapon;

public class AiWeapons : MonoBehaviour
{
    Weapon currentWeapon;
    public Animator rigAnimator;
    public Transform targetObject;
    public Vector3 targetOffset;
    private void Start()
    {
        Weapon existingWeapon = GetComponentInChildren<Weapon>();
        if (existingWeapon != null)
        {
          Equip(existingWeapon);
        }
    }
    public void Equip(Weapon weapon)
    {
        currentWeapon = weapon;
        rigAnimator.Play("equip_" + weapon.weaponName.ToString());
    }
    public void SetTargetPosition(Vector3 pos)
    {
        targetObject.position = pos+ targetOffset;
    }

  
    public void DropWeapon()
    {
        if (currentWeapon)
        {
            currentWeapon.transform.SetParent(null);
            currentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.gameObject.AddComponent<Rigidbody>();
            currentWeapon = null;
        }
    }
}
