using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolleyBullet : MonoBehaviour
{

    private Vector2 marco;
    private Vector2 polo;
    public bool backToSender = false;
    private GameObject parryPlayer;
    private GameObject volleyPig;

    private float speeding = 1;

    // Start is called before the first frame update
    void Start()
    {
        parryPlayer = GameObject.FindGameObjectWithTag("Player");
        volleyPig = GameObject.Find("VolleyPig");
        speeding = volleyPig.GetComponent<Volley>().volleySpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        marco = (parryPlayer.transform.position - transform.position).normalized;
        polo = (volleyPig.transform.position - transform.position).normalized;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Volley vol = collision.GetComponent<Volley>();

        if(collision.name.Contains("Player") && backToSender == false && Input.GetMouseButton(1)) //towards player
        {
            rb.velocity = Vector2.zero;
            backToSender = true;
            speeding *= 1.3f;
            rb.velocity = polo * speeding;
        }
        if(collision.name == "VolleyPig" && backToSender == true) //towards enemy
        {
            rb.velocity = Vector2.zero;
            backToSender = false;
            rb.velocity = marco * speeding;

            if (vol.intercepts == 0)
            {
                vol.chanceofFail += 15f;
            }
            if (vol.intercepts > 0)
            {
                vol.intercepts--;
            }
            if (vol.chanceofFail > 0 && Random.Range(0, 100f) <= vol.chanceofFail)
            {
                collision.GetComponent<EnemyStats>().health -= gameObject.GetComponent<EnemyBehaviour>().damage;
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        volleyPig.GetComponent<EnemyStats>().woOTimer = 5; //change this to depend how it was destroyed
    }
}
