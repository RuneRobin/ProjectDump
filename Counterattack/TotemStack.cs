using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemStack : MonoBehaviour
{
    private int stackNumber;
    public List<GameObject> totemList;
    private Transform lastTotem;
    private GameObject totemPrefab;
    private GameObject totemInstance;
    private int totemType;
    private int totemUpLimit = 0;
    private int totemDownLimit = 0;

    // Start is called before the first frame update
    void Start()
    {
        totemPrefab = Resources.Load("Enemy Assets/Totem") as GameObject;
        totemList = new List<GameObject>();
        lastTotem = transform;

        stackNumber = Random.Range(3, 9); //amount of totem segments in the stack

        for(int i = 0; i < stackNumber; i++)
        {
            float offset = totemPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
            totemInstance = Instantiate(totemPrefab, lastTotem.position + new Vector3(0,offset), transform.rotation);
            totemInstance.GetComponent<TotemScript>().shieldOffset = offset * 1.5f;
            totemList.Add(totemInstance);
            totemInstance.transform.parent = gameObject.transform;

            totemType = Random.Range(0 + totemDownLimit, 5 - totemUpLimit);
            totemInstance.GetComponent<TotemScript>().totemType = totemType;
            if (totemType == 0)
            {
                totemDownLimit++;
            }
            if(totemType == 4)
            {
                totemUpLimit++;
            }
            
            lastTotem = totemInstance.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(totemList.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
