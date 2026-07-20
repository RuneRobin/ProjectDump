using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpillsBills : MonoBehaviour
{
    public GameObject origin;
    public float lifeSpan = 5;
    private float timer;

    private List<GameObject> targets;

    // Start is called before the first frame update
    void Start()
    {
        targets = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        timer += Time.deltaTime;
        if(timer >= lifeSpan)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer != origin.layer && collision.gameObject.tag == "Racer" && (collision.gameObject.GetComponent<RunnerScript>().slow != 2 || !targets.Contains(collision.gameObject)))
        {
            targets.Add(collision.gameObject);
            collision.gameObject.GetComponent<RunnerScript>().slow = 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != origin.layer && collision.gameObject.tag == "Racer")
        {
            targets.Remove(collision.gameObject);
            collision.gameObject.GetComponent<RunnerScript>().slow = 0;
        }
    }

    private void OnDestroy()
    {
        if (targets != null)
        {
            foreach (GameObject racer in targets)
            {
                racer.GetComponent<RunnerScript>().slow = 0;
            }
            targets.Clear();
        }
    }

}
