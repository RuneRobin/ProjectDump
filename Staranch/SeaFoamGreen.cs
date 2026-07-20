using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaFoamGreen : MonoBehaviour
{
    private GameObject fish;
    private GameObject fishPrefab;

    public int fishCaught = 0;
    public float CatchChance = 10f;
    public float reeling = 0;
    public int timeToCatch = 10;

    // Start is called before the first frame update
    void Start()
    {
        fishPrefab = Resources.Load("Mariana Paste") as GameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        reeling += Time.deltaTime;
        if(reeling >= timeToCatch)
        {
            reeling = 0;

            if (Random.Range(0, 101) < CatchChance)
            {
                fish = Instantiate(fishPrefab, transform.position, transform.rotation);
                //fish.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
                fish.layer = gameObject.layer;
                fishCaught++;
                CatchChance = 1f + fishCaught;
            }
            else
            {
                CatchChance *= 1.3f;
            }
        }
    }
}
