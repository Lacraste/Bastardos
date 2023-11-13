using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActiveWeapon;

public class AiWeapons : MonoBehaviour
{
    Weapon currentWeapon;
    public Animator rigAnimator;
    MeshSockets sockets;
    private void Start()
    {
        sockets = GetComponent<MeshSockets>(); 
        Weapon existingWeapon = GetComponentInChildren<Weapon>();
        if (existingWeapon != null)
        {
          Equip(existingWeapon);
        }
    }
    public void Equip(Weapon weapon)
    {
        currentWeapon = weapon;
        //sockets.Attach(weapon.transform, MeshSockets.SocketID.Spine);
        rigAnimator.Play("equip_" + weapon.weaponName.ToString());
    }
}
