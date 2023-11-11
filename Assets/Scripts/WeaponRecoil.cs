using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    public PlayerController playerCam;
    public float verticalRecoil;
    public Vector2 horizontalRecoil;
    public Animator rigController;
    float inputLook;
    Vector2 lookMovement;
    float actualV;
    float actualH;

    public void GenerateRecoil(string nameWeapon)
    {
        actualH = playerCam.GetLookOffset().x + ((Random.Range(horizontalRecoil.x, horizontalRecoil.y)/100));
        actualV = playerCam.GetLookOffset().y - verticalRecoil /100;
        lookMovement = new Vector2(actualH, actualV );
        playerCam.SetLookOffset(lookMovement);

        rigController.Play("recoil_" + nameWeapon, 1, 0);
    }
}
