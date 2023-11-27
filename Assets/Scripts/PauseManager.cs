using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseObject;
    bool paused = false;
    public GameObject interactionButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }
    public void Unpause()
    {
        Time.timeScale = 1f;
        paused = false;
        pauseObject.SetActive(false);
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        paused=true;

        pauseObject.SetActive(true);
    }
    public void ShowInteract(bool value)
    {
        Debug.Log("Teste" + value);
        interactionButton.SetActive(value);
    }
}
