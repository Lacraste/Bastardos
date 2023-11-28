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

    public WeaponPickup pickupObject;
    public float inaccuracy = 0.1f;
    private void Start()
    {
        Weapon existingWeapon = GetComponentInChildren<Weapon>();
        if (existingWeapon != null)
        {
            Equip(existingWeapon);
        }
    }
    private void Update()
    {
        if(targetObject && currentWeapon)
        {
            Vector3 target = targetObject.position;
            target += Random.insideUnitSphere * inaccuracy;
            //target += Random.insideUnitSphere * inaccuracy;
            currentWeapon.UpdateWeapon(Time.deltaTime,target, true);
        }
    }
    public void SetFiring(bool fire)
    {
        Vector3 target = targetObject.position + targetOffset;
        if (fire) currentWeapon.StartAttack(target, true);
        else
        {
            currentWeapon.StopAttack();
        }
    }
    public void Equip(Weapon weapon)
    {
        currentWeapon = weapon;
        rigAnimator.Play("equip_" + weapon.weaponName.ToString());
    }
    public void SetTargetPosition(Vector3 pos)
    {
        targetObject.position = pos + targetOffset;
    }

  
    public void DropWeapon()
    {
        if (currentWeapon)
        {
            var pick = Instantiate(pickupObject,currentWeapon.transform);
            pick.transform.localPosition = Vector3.zero;
            pick.transform.localEulerAngles = Vector3.zero;
            pick.transform.SetParent(null);
            pick.GetComponent<WeaponPickup>().SetCurrentWeapon(currentWeapon);
            Destroy(currentWeapon.gameObject);
            /*currentWeapon.transform.SetParent(null);
            currentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.gameObject.AddComponent<Rigidbody>();
            currentWeapon = null;*/
            
        }
    }
}
