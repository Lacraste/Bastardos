using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioUtils
{
    public static AudioClip RandomAudioClip(AudioClip[] clips)
    {
        int randomNumber = Random.Range(0, clips.Length);
        return clips[randomNumber];
    }
}
