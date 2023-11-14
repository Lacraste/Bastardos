using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoWidget : MonoBehaviour
{
    public GameObject[] icons;
    public TextMeshProUGUI ammoText;
    public void RefreshAmmo(int actualAmmo, int holsterAmmo)
    {
        ammoText.text = $"{actualAmmo} <size=20><alpha=#50><voffset=1em>{holsterAmmo}";
    }
    public void SetIconWeapon(WeaponName weaponsType, bool haveAmmo)
    {
        foreach (var weapon in icons)
        {
            weapon.SetActive(false);
        }
        icons[(int)weaponsType].SetActive(true);
        ammoText.gameObject.SetActive(haveAmmo);
    }
}
