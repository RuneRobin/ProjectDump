using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThaneAnthem : MonoBehaviour
{
    public float slashDuration = 3;
    public bool slashTires = false;
    public float prepping = 0;
    public float prepTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        prepping += Time.deltaTime;
        if(prepping >= prepTime)
        {
            slashTires = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer != 7 && collision.gameObject.tag == "Racer" && collision.gameObject.tag != "Obstacle" && slashTires == true) //7 is big game, who is immune to all effects
        {
            slashTires = false;
            prepping = 0;
            collision.gameObject.GetComponent<RunnerScript>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            StartCoroutine(WompWomp(collision.gameObject));
        }
    }

    public IEnumerator WompWomp(GameObject victim)
    {
        victim.GetComponent<Rigidbody2D>().freezeRotation = false;

        yield return new WaitForSeconds(slashDuration);

        victim.GetComponent<RunnerScript>().enabled = true;
        victim.GetComponent<Rigidbody2D>().freezeRotation = true;
        victim.transform.rotation = Quaternion.Euler(0, 0, 0);
    }    
}
