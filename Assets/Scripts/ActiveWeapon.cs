using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.Animations;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary,
        Secondary
    }

    bool btPressed = false;
    public StarterAssetsInputs _input;

    public Transform crossHairTarget;
    public Transform[] weaponSlots;
    public Animator rigController;
    

    Weapon[] weapons = new Weapon[2];
    int activeWeaponIndex;

    bool isHolstered = false;
    public AmmoWidget ammoWidget;

    private PlayerController playerController;

    void Start()
    {/*
        anim = GetComponent<Animator>();
        overrides = anim.runtimeAnimatorController as  AnimatorOverrideController;
       */ Weapon[] existingWeapons = GetComponentsInChildren<Weapon>();
        if (existingWeapons != null )
        {
            foreach(Weapon existingWeapon in existingWeapons) Equip(existingWeapon);
        }
        StartCoroutine(ActivateWeapon((int)WeaponSlot.Primary));
        playerController = GetComponent<PlayerController>();
        //SetActiveWeapon((int)WeaponSlot.Primary);
    }
    public Weapon GetWeapon(int index)
    {
        if ( index < 0 || index>= weapons.Length) return null;
        return weapons[index];
    }

    void Update()
    {
        if (playerController.pause) return;

        var weapon = GetWeapon(activeWeaponIndex);
        if (weapon != null && !isHolstered)
        {
            ammoWidget.SetIsFiring(weapon.isFiring);

            weapon.UpdateWeapon(Time.deltaTime,crossHairTarget.position);
            if (_input.shoot && btPressed == false)
            {
                weapon.StartAttack(crossHairTarget.position);
                btPressed = true;
            }
            else if (!_input.shoot && btPressed == true)
            {
                weapon.StopAttack();
                btPressed = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetActiveWeapon(WeaponSlot.Primary);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetActiveWeapon(WeaponSlot.Secondary);
            }
        }
    }
    public void Equip(Weapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.slot;
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon){
            Destroy(weapon.gameObject);
            SetActiveWeapon(newWeapon.slot);
            rigController.Play("equip_" + newWeapon.weaponName.ToString());
            ammoWidget.SetIconWeapon(newWeapon.weaponName, newWeapon.recoil != null);
            newWeapon.holsterAmmo = weapon.holsterAmmo;
        }
        weapon = newWeapon;
        weapon.actualAmmo = newWeapon.maxAmmo;
        if (weapon.recoil != null)
        {
            weapon.recoil.playerCam = GetComponent<PlayerController>();
            weapon.recoil.rigController = rigController;
        }
        ammoWidget.RefreshAmmo(weapon.actualAmmo, weapon.holsterAmmo);
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        weapons[weaponSlotIndex] = weapon;
    }
    void SetActiveWeapon(WeaponSlot weaponSlotIndex)
    {
        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlotIndex;
      
        if (holsterIndex == activateIndex)
        {
            holsterIndex = -1;
            return;
        }
        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }
    IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIndex = activateIndex;
    }
    IEnumerator HolsterWeapon(int index)
    {
        isHolstered = true;
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holster_weapon", true);
            weapon.StopAttack();
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime<1f);
        }
    }
    IEnumerator ActivateWeapon(int index)
    {
        var weapon = GetWeapon(index);

        if (weapon)
        {
            weapon.equipSound();
            ammoWidget.RefreshAmmo(weapon.actualAmmo, weapon.holsterAmmo);
            ammoWidget.SetIconWeapon(weapon.weaponName, weapon.recoil != null);
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + weapon.weaponName.ToString());
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
            isHolstered = false;
        }
    }
    public RaycastWeapon GetActiveRaycastWeapon()
    {
        try
        {
            return (RaycastWeapon)GetWeapon(activeWeaponIndex);
        }
        catch
        {
            return null;
        }
    }
    public void AddAmmo(int ammo)
    {
        var weapon = weapons[0];
        weapons[0].AddAmmo(ammo);
        ammoWidget.RefreshAmmo(weapon.actualAmmo, weapon.holsterAmmo);
    }
}
