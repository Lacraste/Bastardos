using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon:MonoBehaviour
{
    public int maxAmmo;
    public int actualAmmo;
    public int holsterAmmo;

    public bool isFiring;
    public Transform raycastTarget;
    public WeaponName weaponName;
    public ActiveWeapon.WeaponSlot slot;

    public WeaponRecoil recoil;
    public abstract void StartAttack();
    public abstract void StopAttack();
    public abstract void UpdateAttack(float time);
    public abstract void AddAmmo(int ammo);
}
