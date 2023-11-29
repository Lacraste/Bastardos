using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MeleeWeapon : Weapon
{
    public WeaponAnimationEvents animationEvents;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
    }
    public override void AddAmmo(int ammo)
    {
    }
    public void OnAnimationEvent(string name)
    {
        if (name == "sfx_attack") audioSource.PlayOneShot(AudioUtils.RandomAudioClip(sfxConfig.fire));
    }

    public override void StartAttack(Vector3 target, bool enemyFire = false)
    {
        isFiring = true;
    }

    public override void StopAttack()
    {
        isFiring = false;
        UpdateAttack(Time.deltaTime, Vector3.zero);
    }
    public override void UpdateAttack(float time, Vector3 target, bool enemyFire = false)
    {
        GetComponentInParent<Animator>().SetBool("Attack", isFiring);
    }
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.GetComponent<HitBox>() )
        {
            collision.gameObject.GetComponent<HitBox>().OnHit(15, collision.ClosestPoint(transform.position));
            audioSource.PlayOneShot(RandomAudioClip(sfxConfig.hit));
        }
    }
    public AudioClip RandomAudioClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public override void equipSound()
    {
        audioSource.PlayOneShot(sfxConfig.equip);
    }

    public override void UpdateWeapon(float time, Vector3 target, bool enemyFire = false)
    {
        if(isFiring)UpdateAttack(time, target);
    }
}
