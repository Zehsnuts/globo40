using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuElement : MonoBehaviour
{
    public bool isOpen;

    public RectTransform leftArrow;
    public RectTransform downArrow;


    private void Awake()
    {
        isOpen = false;
    }

    public void OnClick()
    {
        FindObjectOfType<ExpandableMenu>().InterpretButton(this);
    }

    public void CloseButton()
    {
        if (leftArrow != null)
            leftArrow.gameObject.SetActive(true);

        if (downArrow != null)
            downArrow.gameObject.SetActive(false);

        isOpen = false;
    }

    public void OpenButton()
    {
        if (leftArrow != null)
            leftArrow.gameObject.SetActive(false);

        if (downArrow != null)
            downArrow.gameObject.SetActive(true);

        isOpen = true;
    }


    public void OnMouseEnter()
    {
        //FindObjectOfType<MousePointer>().OnMouseEnter();
    }

    public void OnMouseExit()
    {
        //FindObjectOfType<MousePointer>().OnMouseExit();
    }

}
