using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public void Play()
    {
        SceneManager.LoadScene("intro");
        PlaySFX();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        PlaySFX();
    }
    public void Quit()
    {PlaySFX();
        Application.Quit();
    }
    public void PlaySFX()
    {
        GameObject.Find("AudioBts").GetComponent<ButtonAudio>().PlayBtSFX();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("NewScene");
    }
}
