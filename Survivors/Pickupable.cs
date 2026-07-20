using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public bool inRange;
    private PlayerMovement player;
    private float catchUp;

    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerMovement.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(inRange == true)
        {
            gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position + (player.gameObject.transform.position - transform.position) * catchUp * Time.deltaTime);
            catchUp+= 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ally")
        {
            if (name.Contains("XPellet"))
            {
                PlayerMovement.instance.exp += 1;
            }
            if(name.Contains("Coin"))
            {
                Master.instance.moneyCollectedDuringRun += 1;
            }
            if(name.Contains("Rescue"))
            {
                canvas.gameObject.transform.Find("Rescue Menu").gameObject.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
