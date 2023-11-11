using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BonecoAlvo : MonoBehaviour
{
    Quaternion initial, final;
    float rot;
    void Start()
    {
        final = Quaternion.Euler(-60, transform.rotation.y, transform.rotation.z);
        initial = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
    }

    void Update()
    {
        
    }
    public void Down()
    {
        //GameObject.Find("Contador").GetComponent<Contador>().enemy++;
        StopAllCoroutines();
        StartCoroutine(DownTarget());
    }
    IEnumerator DownTarget()
    {
        while (transform.rotation != final)
        {
            yield return null;
            transform.rotation = Quaternion.Lerp(transform.rotation,final, 10 * Time.deltaTime);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(UpTarget());
    }
    IEnumerator UpTarget()
    {
        while (transform.rotation != initial)
        {
            yield return null;
            transform.rotation = Quaternion.Lerp(transform.rotation,initial, 5 * Time.deltaTime);
        }
    }
}
