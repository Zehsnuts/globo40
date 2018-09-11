using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInFadeOut : MonoBehaviour
{
    public Image image;

    public float secondsToWait;

    void Start()
    {
        StartCoroutine(StartFadeOut(secondsToWait));
    }

    IEnumerator StartFadeOut(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        while (image.color.a > 0)
        {
            var tempColor = image.color;
            tempColor.a -= 0.01f;
            image.color = tempColor; 
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
