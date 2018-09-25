using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public Sprite playerSprite;

	// Use this for initialization
	void Start ()
    {
        playerSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
