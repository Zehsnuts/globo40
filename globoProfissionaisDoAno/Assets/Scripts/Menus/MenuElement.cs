using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuElement : MonoBehaviour
{
    public bool isOpen;

    private void Awake()
    {
        isOpen = false;
    }

    public void OnClick()
    {
        FindObjectOfType<ExpandableMenu>().InterpretButton(this);
    }

    public void OnMouseEnter()
    {
        FindObjectOfType<MousePointer>().OnMouseEnter();
    }

    public void OnMouseExit()
    {
        FindObjectOfType<MousePointer>().OnMouseExit();
    }

}
