using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponIK : MonoBehaviour
{
    public Transform targetTransform;
    public Transform aimTransform;
    public Transform bone;
    public int iterations = 10;
    [Range(0,1)]
    public float weight = 1f;
    public bool canRot = false;
    private void LateUpdate()
    {
        if (!canRot) return;
        Vector3 targetPosition = targetTransform.position;
        for (int i = 0; i < iterations; i++)
        {
            AimAtTarget(bone, targetPosition, weight);
        }
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition, float weight)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendedRotation * bone.rotation; 
    }
}
