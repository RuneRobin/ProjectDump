using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour {

    Abilities abi;
    public new Camera camera;
    // Use this for initialization
    void Start ()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        abi = hit.collider.gameObject.GetComponent<Abilities>();
	}

    // Update is called once per frame
    void Update()
    {
        if(abi.gravityWell == false)
        {
            Destroy(gameObject);
        }
    }
           


    public void OnTriggerStay2D(Collider2D other)
    {

            if (other.gameObject.tag == "Player")
            {
                Debug.Log("here");
                other.transform.position = Vector3.MoveTowards(other.transform.position, gameObject.transform.position, 8 * Time.deltaTime);
            }
        }
    }

