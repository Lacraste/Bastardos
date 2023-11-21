using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum BodyPart
{
    Members,
    Chest,
    Head
} 
public class HitBox : MonoBehaviour
{
    public ParticleSystem blood;
    public Health health;
    public BodyPart bodyPart;
  
    public void OnHit(RaycastWeapon weapon, Vector3 direction, Vector3 position, Vector3 forward)
    {
        var damage = ((int)bodyPart +1) * weapon.weapon.bulletDamage;
        health.TakeDamage(damage);
        blood.transform.position = position;
        blood.transform.forward = forward;

        blood.Play();
    }
    public void OnHit(float Damage, Vector3 position)
    {
        health.TakeDamage(Damage);
        blood.transform.position = position;
        blood.Play();

    }
}
