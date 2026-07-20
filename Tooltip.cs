using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string message;

    private void OnMouseEnter()
    {
        ItemPopUpInfo.instance.SetAndShowTooltip(message);
    }
    private void OnMouseExit()
    {
        ItemPopUpInfo.instance.HideTooltip();
    }
}
