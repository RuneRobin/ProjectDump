using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    public GameObject targetZone;
    public Transform currTargetPos;
    public bool unitPlacer = false;
    public GameObject currUnit;
    public bool summonBlock = false;

    //////////////UNITS/////////////////////
    public GameObject grimsly;
    public int grimslyCount = 1;

    public GameObject droplet;
    public int dropletCount = 1;

    public GameObject lili;
    public int liliCount = 1;

    public GameObject rascal;
    public int rascalCount = 1;
    //////////////UNITS/////////////////////



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        MoveHere();
    }

    public void MoveHere()
    {
        if(Input.GetKeyDown("q")) //switches between placing mode and follow mode
        {
            unitPlacer = !unitPlacer;
        }

        if (unitPlacer == false)
        {
            if (Input.GetMouseButton(0)) //places marker for units to follow
            {
                Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                screenPos.z = 0;

                if (!GameObject.FindGameObjectWithTag("TargetZone"))
                {
                    currTargetPos = Instantiate(targetZone, screenPos, Quaternion.identity).transform;

                }
                else
                {
                    currTargetPos.position = screenPos;
                }


            }
        }
        else
        {
            if(Input.GetMouseButton(0) && summonBlock == false) //summons unit
            {
                Vector3 screenPos2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));//gets the point in the world relative to screen
                screenPos2.z = 0;

                if(currUnit.name == "Grimsly" && grimslyCount > 0)
                {
                    Instantiate(currUnit, screenPos2, Quaternion.identity);
                    grimslyCount--;
                }
                else if (currUnit.name == "Droplet" && dropletCount > 0)
                {
                    Instantiate(currUnit, screenPos2, Quaternion.identity);
                    dropletCount--;
                }
                else if (currUnit.name == "Lili" && liliCount > 0)
                {
                    Instantiate(currUnit, screenPos2, Quaternion.identity);
                    liliCount--;
                }
                else if (currUnit.name == "Rascal" && rascalCount > 0)
                {
                    Instantiate(currUnit, screenPos2, Quaternion.identity);
                    rascalCount--;
                }
            }
        }
        

    }
}
