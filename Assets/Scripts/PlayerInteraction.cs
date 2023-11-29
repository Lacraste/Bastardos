using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using StarterAssets;


public class PlayerInteraction : MonoBehaviour
{
    private Camera myCam;
    private Interectable currentInterectable;
    public PauseManager pauseManager;
    private bool view;

    void Start()
    {
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInterectable();
    }
    void CheckInterectable()
    {
        RaycastHit hit;
        Vector3 rayOrigin = myCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.2f));
        if (Physics.Raycast(rayOrigin, myCam.transform.forward, out hit, 2f))
        {
            Interectable interectable = hit.collider.GetComponent<Interectable>();
            if (interectable != null)
            {
                if (!view)
                {
                    view = true;
                    pauseManager.ShowInteract(true);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    interectable.Interact();
                }
            }
            else if (view)
            {
                pauseManager.ShowInteract(false);
                view = false;
            }
        }
        else if (view)
        {
            view = false;
            pauseManager.ShowInteract(false);
        }
    }
}

