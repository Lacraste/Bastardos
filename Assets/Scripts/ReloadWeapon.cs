using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWeapon : MonoBehaviour
{
    public Animator rigController;
    public WeaponAnimationEvents animationEvents;
    public ActiveWeapon activeWeapon;
    public Transform leftHand;

    GameObject magazineHand;
    RaycastWeapon weapon;
    public AmmoWidget ammoWidget;
    private void Start()
    {
        animationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
    }
    private void Update()
    {
        weapon = activeWeapon.GetActiveRaycastWeapon();
        if (weapon == null) return;
        if (Input.GetKeyDown(KeyCode.R) || weapon.actualAmmo == 0)
        {
            ReloadAnimation();
        }
        if (weapon.isFiring)
        {
            ammoWidget.RefreshAmmo(weapon.actualAmmo, weapon.holsterAmmo);
        }
    }
    void ReloadAnimation()
    {
        if (weapon.holsterAmmo == 0 || weapon.GetIsReloading()) return;
        rigController.SetTrigger("reload");
        weapon.SetIsReloading(true);
        weapon.audioSource.PlayOneShot(weapon.sfxConfig.reload);
    }
    void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "detach_magazine":
                DetachMagazine();
                break;
            case "drop_magazine":
                DropMagazine();
                break;
            case "refill_magazine":
                RefillMagazine();
                break;
            case "attach_magazine":
                AttachMagazine();
                break;
        }
    }
    void DetachMagazine()
    {
        magazineHand = Instantiate(weapon.magazine,leftHand,true);
        weapon.magazine.SetActive(false);
    }
    void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();
        magazineHand.SetActive(false);
    }
    void RefillMagazine()
    {
        magazineHand.SetActive(true);
    }
    void AttachMagazine()
    {
        Destroy(magazineHand);
        weapon.magazine.SetActive(true);
        weapon.SetIsReloading(false);
        rigController.ResetTrigger("reload");
        weapon.Reload();
        ammoWidget.RefreshAmmo(weapon.actualAmmo, weapon.holsterAmmo);
    }
}
