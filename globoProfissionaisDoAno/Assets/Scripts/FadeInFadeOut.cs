using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInFadeOut : MonoBehaviour
{
    public Image image; 

    void Start()
    {
        StartCoroutine(StartFadeOut(0.01f));
    }

    IEnumerator StartFadeOut(float seconds)
    {
        yield return new WaitForSeconds(2);
        while (image.color.a > 0)
        {
            var tempColor = image.color;
            tempColor.a -= 0.01f;
            image.color = tempColor; 
            yield return new WaitForSeconds(seconds);
        }

        Destroy(gameObject);
    }
}
