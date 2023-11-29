using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Interectable : MonoBehaviour
{
    public AudioClip clip;
    public virtual void Interact()
    {
        GameObject.Find("Audio").GetComponent<GlobalAudio>().PlaySfx(clip);
    }
}
