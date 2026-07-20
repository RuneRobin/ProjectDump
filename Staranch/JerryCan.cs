using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryCan : MonoBehaviour
{
    private GameObject followsYouPrefab;
    private GameObject followsYou;
    private GameObject nextOne;

    public int materials;
    public float maxPreparing = 4;
    public float preparing = 4;

    // Start is called before the first frame update
    void Start()
    {
        followsYouPrefab = Resources.Load("FollowsYou") as GameObject;
        nextOne = gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(preparing < maxPreparing)
        {
            preparing += Time.deltaTime;
        }
        if(materials >= 3)
        {
            materials -= 3;
            MoreFollowers();
        }

        if(Input.GetKey("k"))
        {
            MoreFollowers();
        }
    }

    public void MoreFollowers()
    {
        followsYou = Instantiate(followsYouPrefab, transform.position, transform.rotation);
        FollowsYou fy = followsYou.GetComponent<FollowsYou>();
        fy.conductor = gameObject;

        //stats
        fy.speed = gameObject.GetComponent<RunnerScript>().speed;
        followsYou.layer = gameObject.layer;

        //set up for next one
        fy.oneAhead = nextOne;
        nextOne = followsYou;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle" && preparing >= maxPreparing && collision.gameObject.layer != gameObject.layer)
        {
            materials++;
            preparing = 0;
        }
    }

}
