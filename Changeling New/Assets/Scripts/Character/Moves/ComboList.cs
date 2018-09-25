using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboList : MonoBehaviour {

    public int comboOne;
    public int comboTwo;
    public int comboThree;
    public int comboFour;
    public int comboFive;
    public Dropdown dropOne;
    public Dropdown dropTwo;
    public Dropdown dropThree;
    public Dropdown dropFour;
    public Dropdown dropFive;
    //AttackChangeDropdownMenu ACDM;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        comboOne = dropOne.value;
        comboTwo = dropTwo.value;
        comboThree = dropThree.value;
        comboFour = dropFour.value;
        comboFive = dropFive.value;
    }
}
