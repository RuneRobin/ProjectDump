using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {


    public Text Mana;
    public Text Health;
    public Text Ability;
    PlatformChanger platChange;
	// Use this for initialization
	void Start ()
    {
        platChange = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlatformChanger>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Health.text = "Health: " + (int)platChange.health;
        Mana.text = "Mana: " + (int)platChange.Mana;
        Ability.text = "Ability: " + platChange.currentAbility;
	}
}
