using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunSO : ScriptableObject
{
    public bool automatic;
    public float fireRate;
    public float bulletDamage;
    public float bulletSpeed;
    public float bulletDrop;
    public ParticleSystem hitEffect;
    public TrailRenderer shotTrail; 

}
