using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guy18TarotCycler : MonoBehaviour
{
    public static Guy18TarotCycler instance;
    private RoomController rc;
    public List<GameObject> tarotCycler = new List<GameObject>();
    private GameObject currTarot;
    private bool tarotAppearing = true;
    public float bonusVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        rc = RoomController.instance;
        currTarot = tarotCycler[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rc.allGuysActive[17] == true)
        {
            if(bonusVelocity > 0)
            {
                bonusVelocity -= bonusVelocity/100*Time.deltaTime * 20;
            }
            gameObject.transform.Rotate(0.0f, 1.0f + bonusVelocity, 0.0f, Space.World);

            if (tarotAppearing == true && currTarot.GetComponent<Image>().color.a < 1)
            {
                currTarot.GetComponent<Image>().color += new Color(1f, 1f, 1f, 0.01f);
                if (currTarot.GetComponent<Image>().color.a >= 1)
                {
                    tarotAppearing = false;
                }
            }
            if (tarotAppearing == false && currTarot.GetComponent<Image>().color.a > 0)
            {
                currTarot.GetComponent<Image>().color -= new Color(1f, 1f, 1f, 0.01f);
                if (currTarot.GetComponent<Image>().color.a <= 0)
                {
                    tarotAppearing = true;
                    

                    int i = tarotCycler.IndexOf(currTarot) + 1; //cycles through all 12 elements in list, if it reaches the last one it resets to 0
                    if (i == 12)
                    {
                        i = 0;
                    }
                    currTarot = tarotCycler[i];
                }
            }
        }
    }
}
