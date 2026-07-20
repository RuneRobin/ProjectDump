using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clopernicus : MonoBehaviour
{
    public List<GameObject> targets;
    public List<int> bloodSucked;

    public int bloodNeededForAdaptation = 50;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject racer in GameObject.FindGameObjectsWithTag("Racer"))
        {
            if(racer != gameObject && racer.layer != 7) //7 is BigGame
            {
                targets.Add(racer);
            }
        }

        for(int i = 0; i < targets.Count; i++)
        {
            bloodSucked.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(targets.Contains(collision.gameObject) && gameObject.GetComponent<Clopernicus>().enabled == true)
        {
            int index = targets.IndexOf(collision.gameObject);

            bloodSucked[index]++;

            if(bloodSucked[index] >= bloodNeededForAdaptation)
            {
                if(collision.gameObject.layer == 6) //6:Gum Works
                {
                    gameObject.AddComponent<PinkDragonblaster>();
                    targets.RemoveAt(index);
                    bloodSucked.RemoveAt(index);
                }
                else if(collision.gameObject.layer == 8) //8:Blue Faster
                {
                    gameObject.AddComponent<SloopyVonTrigletop>();
                    targets.RemoveAt(index);
                    bloodSucked.RemoveAt(index);
                }
                else if (collision.gameObject.layer == 9) //9:Rat Racer
                {
                    gameObject.AddComponent<ThaneAnthem>();
                    targets.RemoveAt(index);
                    bloodSucked.RemoveAt(index);
                }
                else if (collision.gameObject.layer == 11) //11:Scrumble Buck
                {
                    gameObject.AddComponent<EightLampsLater>();
                    targets.RemoveAt(index);
                    bloodSucked.RemoveAt(index);
                }
                else if (collision.gameObject.layer == 12) //12:Sail for Sales
                {
                    gameObject.AddComponent<SeaFoamGreen>();
                    targets.RemoveAt(index);
                    bloodSucked.RemoveAt(index);
                }
                else if (collision.gameObject.layer == 13) //13:Spatial Diorama
                {
                    gameObject.AddComponent<WilesAplomb>();
                    targets.RemoveAt(index);
                    bloodSucked.RemoveAt(index);
                }
                else if (collision.gameObject.layer == 14) //14:Witch Day Shines
                {
                    gameObject.AddComponent<JerryCan>();
                    targets.RemoveAt(index);
                    bloodSucked.RemoveAt(index);
                }
                else if (collision.gameObject.layer == 15) //15:Tankard Whole
                {
                    gameObject.AddComponent<StallionCustard>();
                    targets.RemoveAt(index);
                    bloodSucked.RemoveAt(index);
                }
                else if (collision.gameObject.layer == 16) //16: Mime The Volume
                {
                    gameObject.AddComponent<Crumb>();
                    targets.RemoveAt(index);
                    bloodSucked.RemoveAt(index);
                }
            }
        }
    }
}
