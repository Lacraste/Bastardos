using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponSFXSO : ScriptableObject
{
    public AudioClip[] fire;
    public AudioClip[] hit;
    public AudioClip reload;
    public AudioClip equip;
}
