using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jail : Interectable
{
    public PauseManager key;
    public void Start()
    {
        key = FindFirstObjectByType<PauseManager>();
    }
    public override void Interact()
    {
        if(key.hasKey) Finish();
        else
        {
            key.FindKey();
        }
    }
    public void Finish()
    {
        SceneManager.LoadScene("Menu");
    }
}
