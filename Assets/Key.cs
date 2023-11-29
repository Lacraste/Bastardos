using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interectable
{
    public override void Interact()
    {
        base.Interact();
        GameObject.Find("Canvas").GetComponent<PauseManager>().ActivateKey();
        Destroy(gameObject);
    }
}
