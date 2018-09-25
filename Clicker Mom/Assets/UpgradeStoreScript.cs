using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStoreScript : MonoBehaviour
{
    public Button RockOut;
    ClickScript clickScript;

    public Button walkAdv;

	// Use this for initialization
	void Start ()
    {
        clickScript = RockOut.GetComponent<ClickScript>();

        Button walkAdvBtn = walkAdv.GetComponent<Button>();
        walkAdvBtn.onClick.AddListener(WalkAdv);

        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void WalkAdv()
    {
        if(clickScript.Revenue >= 300)
        {
            clickScript.Revenue = clickScript.Revenue - 300;
            clickScript.WalkAdvBought = true;
            Destroy(walkAdv);
        }
    }
}
