using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiReloadWeapon : MonoBehaviour
{
    public Animator rigController;
    public WeaponAnimationEvents animationEvents;
    public Transform leftHand;
    GameObject magazineHand;
    RaycastWeapon weapon;
    private void Start()
    {
        animationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
        weapon = GetComponentInChildren<RaycastWeapon>();
    }
    private void Update()
    {
        if (weapon == null) return;
        if (weapon.actualAmmo == 0 && !weapon.GetIsReloading())
        {
            Debug.Log(gameObject.name);
            ReloadAnimation();
        }
    }
    void ReloadAnimation()
    {
        weapon.AddAmmo(weapon.maxAmmo);
        if (weapon.GetIsReloading()) return;
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
        magazineHand = Instantiate(weapon.magazine, leftHand, true);
        weapon.magazine.SetActive(false);
    }
    void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();
        Destroy(droppedMagazine, 10f);
        magazineHand.SetActive(false);
    }
    void RefillMagazine()
    {
        magazineHand.SetActive(true);
    }
    void AttachMagazine()
    {
        Destroy(magazineHand);
        if(weapon)
        {
            weapon.magazine.SetActive(true);
            weapon.SetIsReloading(false);
            rigController.ResetTrigger("reload");
            weapon.Reload();
        }
    }
}
