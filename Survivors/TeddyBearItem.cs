using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBearItem : MonoBehaviour
{
    public int level = 0;


    public void TeddyBear()
    {
        Debug.Log("I EAT MEN COCK");
        level++;
        if(level == 1)
        {           
            PlayerMovement.instance.healthBoost += 100;
        }
        if(level == 2)
        {
            PlayerMovement.instance.healthBoost += 100;
        }
        if(level == 3)
        {
            PlayerMovement.instance.healthBoost += 100;

        }
    }
}
