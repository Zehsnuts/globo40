using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    public GameObject toolTip;

    bool showTooltip;

    private void Awake()
    {
        showTooltip = false;
        toolTip.SetActive(showTooltip);
    }

    public void OnMouseEnter()
    {
        showTooltip = true;
        toolTip.SetActive(showTooltip);

    }

    public void OnMouseExit()
    {
        showTooltip = false;
        toolTip.SetActive(showTooltip);
    }
}
