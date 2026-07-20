using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public bool inRange;
    private PlayerScript player;
    private float catchUp;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerScript.instance;
        player.pickUps.Add(gameObject);
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
        if(collision.tag == "Player")
        {
            if(name.Contains("Coin"))
            {
                player.money += 1 * (1 + player.moneyBoost);
            }
            if(name.Contains("Square"))
            {
                ItemDisplay.instance.squareScrap++;
            }
            if (name.Contains("Circle"))
            {
                ItemDisplay.instance.circleScrap++;
            }
            if (name.Contains("Triangle"))
            {
                ItemDisplay.instance.triangleScrap++;
            }
            if(name.Contains("Blueprint"))
            {
                UpgradeLoader.instance.BlueprintUnlocker();
            }
            player.pickUps.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
