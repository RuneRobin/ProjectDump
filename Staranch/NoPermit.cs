using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPermit : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Racer" && collision.gameObject != transform.parent.gameObject)
        {
            if (collision.GetComponent<PinkDragonblaster>()) //6:Gum Works
            {
                collision.GetComponent<PinkDragonblaster>().enabled = false;
            }
            else if (collision.GetComponent<SloopyVonTrigletop>()) //8:Blue Faster
            {
                collision.GetComponent<SloopyVonTrigletop>().enabled = false;
            }
            else if (collision.GetComponent<ThaneAnthem>()) //9:Rat Racer
            {
                collision.GetComponent<ThaneAnthem>().enabled = false;
            }
            else if (collision.GetComponent<Clopernicus>()) //10:Blood Letter
            {
                collision.GetComponent<Clopernicus>().enabled = false;
            }
            else if (collision.GetComponent<EightLampsLater>()) //11:Scrumble Buck
            {
                collision.GetComponent<EightLampsLater>().enabled = false;
            }
            else if (collision.GetComponent<SeaFoamGreen>()) //12:Sail for Sales
            {
                collision.GetComponent<SeaFoamGreen>().enabled = false;
            }
            else if (collision.GetComponent<WilesAplomb>()) //13:Spatial Diorama
            {
                collision.GetComponent<WilesAplomb>().enabled = false;
            }
            else if (collision.GetComponent<JerryCan>()) //14:Witch Day Shines
            {
                collision.GetComponent<JerryCan>().enabled = false;
            }
            else if (collision.GetComponent<StallionCustard>()) //15:Tankard Whole
            {
                collision.GetComponent<StallionCustard>().enabled = false;
            }
            else if (collision.GetComponent<Crumb>()) //16: Mime The Volume
            {
                collision.GetComponent<Crumb>().enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Racer" && collision.gameObject != transform.parent.gameObject)
        {
            if (collision.GetComponent<PinkDragonblaster>()) //6:Gum Works
            {
                collision.GetComponent<PinkDragonblaster>().enabled = true;
            }
            else if (collision.GetComponent<SloopyVonTrigletop>()) //8:Blue Faster
            {
                collision.GetComponent<SloopyVonTrigletop>().enabled = true;
            }
            else if (collision.GetComponent<ThaneAnthem>()) //9:Rat Racer
            {
                collision.GetComponent<ThaneAnthem>().enabled = true;
            }
            else if (collision.GetComponent<Clopernicus>()) //10:Blood Letter
            {
                collision.GetComponent<Clopernicus>().enabled = true;
            }
            else if (collision.GetComponent<EightLampsLater>()) //11:Scrumble Buck
            {
                collision.GetComponent<EightLampsLater>().enabled = true;
            }
            else if (collision.GetComponent<SeaFoamGreen>()) //12:Sail for Sales
            {
                collision.GetComponent<SeaFoamGreen>().enabled = true;
            }
            else if (collision.GetComponent<WilesAplomb>()) //13:Spatial Diorama
            {
                collision.GetComponent<WilesAplomb>().enabled = true;
            }
            else if (collision.GetComponent<JerryCan>()) //14:Witch Day Shines
            {
                collision.GetComponent<JerryCan>().enabled = true;
            }
            else if (collision.GetComponent<StallionCustard>()) //15:Tankard Whole
            {
                collision.GetComponent<StallionCustard>().enabled = true;
            }
            else if (collision.GetComponent<Crumb>()) //16: Mime The Volume
            {
                collision.GetComponent<Crumb>().enabled = true;
            }
        }
    }

}
