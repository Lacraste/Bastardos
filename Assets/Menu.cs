using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public void Play()
    {
        SceneManager.LoadScene("New Scene");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
