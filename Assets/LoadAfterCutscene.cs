using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterCutscene : MonoBehaviour
{
    public string Scene;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(Scene);
    }

}
