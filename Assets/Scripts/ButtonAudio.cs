using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Awake()
    {
        if (FindObjectsOfType<ButtonAudio>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    public void PlayBtSFX()
    {
        m_AudioSource.Play();
    }
}
