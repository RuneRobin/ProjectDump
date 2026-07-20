using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Master : MonoBehaviour
{
    public static Master instance;

    public GameObject[] buttons;
    public TextMeshProUGUI mainScreenText;

    private Library lb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        lb = Library.instance;

        lb.CurrentChoice(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
        }
    }
}
