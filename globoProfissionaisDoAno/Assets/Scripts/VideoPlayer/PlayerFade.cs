using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFade : MonoBehaviour
{
    RectTransform rT;
    float initY;

    private void Start()
    {
        rT = transform.GetComponent<RectTransform>();
        initY = rT.anchoredPosition.y;
        rT.anchoredPosition += new Vector2(0, -25f);
    }

    public void StartFadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInInterface());
    }

    IEnumerator FadeInInterface()
    {
        bool isPosY = false;
        while (!isPosY)
        {
            if (rT.anchoredPosition.y < initY)
                rT.anchoredPosition += new Vector2(0, 1f);
            else
                isPosY = true;

            yield return new WaitForEndOfFrame();
        }
    }

    public void StartFadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutInterface());
    }

    IEnumerator FadeOutInterface()
    {
        yield return new WaitForSeconds(1);

        rT.anchoredPosition += new Vector2(0, -25f);
    }
}
