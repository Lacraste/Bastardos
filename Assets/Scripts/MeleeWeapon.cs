using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MeleeWeapon : Weapon
{
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public override void AddAmmo(int ammo)
    {
        throw new System.NotImplementedException();
    }

    public override void StartAttack()
    {
        //audioSource.PlayOneShot(RandomAudioClip(attackSFX));
        isFiring = true;
    }

    public override void StopAttack()
    {
        isFiring = false;
        UpdateAttack(Time.deltaTime);
    }
    public override void UpdateAttack(float time)
    {
        GetComponentInParent<Animator>().SetBool("Attack", isFiring);
    }
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.transform.tag == "Enemy")
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
}
