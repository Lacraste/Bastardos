using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidBodies;
    Animator anim;
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
        DeactivateRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeactivateRagdoll()
    {
        foreach (var r in rigidBodies)
        {
            r.isKinematic = true;
        }
        anim.enabled = true;

    }
    public void ActivateRagdoll()
    {
        foreach (var r in rigidBodies)
        {
            r.isKinematic = false;

        }
        anim.enabled = false;

    }
}
