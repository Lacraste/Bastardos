using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitMarker : MonoBehaviour
{
    Image image;
    float alpha;
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void Hit()
    {
       StopAllCoroutines();
        StartCoroutine(HitCoroutine());
    }
    IEnumerator HitCoroutine()
    {
        image.enabled = true;
        yield return null;
        alpha = 1;
        while (alpha > 0.1)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return new WaitForEndOfFrame();
            alpha = Mathf.Lerp(alpha, 0, Time.deltaTime * 5);
        }
        image.enabled = false;
    }
}
