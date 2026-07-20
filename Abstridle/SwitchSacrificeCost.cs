using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSacrificeCost : MonoBehaviour
{
    public List<Text> costList = new List<Text>();

    public void SwitchCost()
    {
        for(int i = 0; i < costList.Count; i++)
        {
            costList[i].text = "-50";
            costList[i].color = Master.instance.colourMoneyText[i].color;
        }
    }
}
