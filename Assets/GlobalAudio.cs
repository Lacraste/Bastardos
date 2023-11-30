using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudio : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public void PlaySfx(AudioClip[] clips)
    {
        int r= Random.Range(0, clips.Length);
        sfxSource.PlayOneShot(clips[r]);
    }
    public void PlaySfx(AudioClip clip) 
    {
        sfxSource.PlayOneShot(clip);
    }
    public void ChangeMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
}

