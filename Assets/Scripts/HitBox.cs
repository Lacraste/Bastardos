using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HitBox : MonoBehaviour
{
    public ParticleSystem blood;
    public Health health;
    public void OnHit(RaycastWeapon weapon, Vector3 direction, Vector3 position, Vector3 forward)
    {
        health.TakeDamage(weapon.weapon.bulletDamage);
        blood.transform.position = position;
        blood.transform.forward = forward;

        blood.Play();

        /*
        foreach (var particle in blood) 
        { 
            blood.Emit(1);
        }*/
    }
    public void OnHit(float Damage, Vector3 position)
    {
        health.TakeDamage(Damage);
        blood.transform.position = position;
        blood.Play();

    }
}
