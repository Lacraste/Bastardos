using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    Camera m_Camera;
    Ray ray;
    RaycastHit hit;

    Vector3 offset;
    private void Start()
    {
        m_Camera = Camera.main;
    }
    private void Update()
    {
        offset = m_Camera.transform.forward;
        ray.origin = m_Camera.transform.position + offset;
        ray.direction = m_Camera.transform.forward;
        if(Physics.Raycast(ray, out hit)) {
            transform.position = hit.point;
        } 
        else
        {
            transform.position = ray.origin + ray.direction * 100.0f;
        }

    }
}
