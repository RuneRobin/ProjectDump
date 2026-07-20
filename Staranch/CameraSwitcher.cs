using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public List<GameObject> allRacers;
    public List<GameObject> racePfps;
    public GameObject pfp;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject racer in GameObject.FindGameObjectsWithTag("Racer"))
        {
            allRacers.Add(racer);
        }

        for(int i = 0; racePfps.Count < allRacers.Count;i++)
        {
            racePfps.Add(Resources.Load("Sprites/" + allRacers[i].name + "PFP") as GameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation != Quaternion.Euler(0,0,0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if(Input.GetMouseButtonDown(0))
        {
            index++;
            if(index > allRacers.Count -1)
            {
                index = 0;
            }
            gameObject.transform.parent = allRacers[index].gameObject.transform;
            pfp.GetComponent<Image>().sprite = racePfps[index].GetComponent<SpriteRenderer>().sprite;
            transform.localPosition = new Vector3(0,0,-100);
        }
        if(Input.GetMouseButtonDown(1))
        {
            index--;
            if(index < 0)
            {
                index = allRacers.Count -1;
            }
            gameObject.transform.parent = allRacers[index].gameObject.transform;
            pfp.GetComponent<Image>().sprite = racePfps[index].GetComponent<SpriteRenderer>().sprite;
            transform.localPosition = new Vector3(0, 0, -100);
        }


    }
}
