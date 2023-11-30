using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    StarterAssetsInputs st;

    public Animator animFindKey;
    public void ActivateKey()
    {
        key.SetActive(true);
        hasKey = true;
        animFindKey.gameObject.SetActive(false);
    }
    private void Start()
    {
        st = FindObjectOfType<StarterAssetsInputs>();
        st.SetCursorState(true);
        st.cursorLocked = true;
        st.cursorInputForLook = true;
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

        st.SetCursorState(false);
        st.cursorLocked = false;
        st.look = new Vector2(0, 0);
        st.cursorInputForLook = false;
    }
    public void Unpause()
    {
        Time.timeScale = 1f;
        paused = false;
        pauseObject.SetActive(false);
        onUnpause.Invoke();

        st.SetCursorState(true);
        st.cursorLocked = true;
        st.cursorInputForLook = true;
    }
    public void GameOver()
    {
        st.SetCursorState(false);
        st.cursorLocked = false;
        st.look = new Vector2(0, 0);
        st.cursorInputForLook = false;

        playerDead = true;
        gameOverObject.SetActive(true);
    }
    public void ShowInteract(bool value)
    {
        interactionButton.SetActive(value);
    }
    public void FindKey()
    {
        animFindKey.Play("PopUP");
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        st.SetCursorState(true);
        st.cursorLocked = true;
        st.cursorInputForLook = true;
    }

}
