using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseObject;
    public GameObject gameOverObject;
    bool paused = false;
    public GameObject interactionButton;
    public GameObject key;
    public bool hasKey;

    public UnityEvent onPause;
    public UnityEvent onUnpause;

    public bool playerDead;
    public void ActivateKey()
    {
        key.SetActive(true);
        hasKey = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (playerDead) return;
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            if (paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        paused = true;
        pauseObject.SetActive(true);
        onPause.Invoke();
    }
    public void Unpause()
    {
        Time.timeScale = 1f;
        paused = false;
        pauseObject.SetActive(false);
        onUnpause.Invoke();
    }
    public void GameOver()
    {
        playerDead = true;
        gameOverObject.SetActive(true);
    }
    public void ShowInteract(bool value)
    {
        interactionButton.SetActive(value);
    }
}
