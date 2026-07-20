using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowsYou : MonoBehaviour
{
    private float distance;
    public GameObject conductor;
    public GameObject oneAhead;
    public float personalSpace = 1;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, oneAhead.transform.position);

        if(speed != conductor.GetComponent<RunnerScript>().speed)
        {
            speed = conductor.GetComponent<RunnerScript>().speed;
        }

        if (distance > personalSpace)
        {
            transform.position = Vector2.MoveTowards(transform.position, oneAhead.transform.position, speed * Time.deltaTime);
        }

    }
}
