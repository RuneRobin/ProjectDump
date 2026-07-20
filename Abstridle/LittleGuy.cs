using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ColourEnum;

public class LittleGuy : MonoBehaviour
{
    public int colour;

    public GameObject designatedPile;
    public Vector3 mainPile;

    private Rigidbody2D rb;
    private Vector3 destination; //Whether the little guy is going to the main pile or their designated pile
    private Vector3 prevLocation;
    private float timeToReach = 0f;
    private Vector2 direction;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        designatedPile = Master.instance.piles[colour];

        Master.instance.colourGuys[colour].Add(gameObject);
        Master.instance.UpdateAmountOfLittleGuys(colour);
        GetComponent<SpriteRenderer>().color = Master.instance.piles[colour].GetComponent<Image>().color; //Changes colour of little guy to match pile

        destination = mainPile;
        prevLocation = designatedPile.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //direction = (destination - transform.position).normalized;
        distance = Vector2.Distance(transform.position, destination);

        if (distance >= 0.2f)
        {
            //rb.MovePosition((Vector2)transform.position + new Vector2(direction.x * speed, direction.y * speed) * Time.deltaTime);
            transform.position = Vector3.Lerp(prevLocation, destination, timeToReach);
            timeToReach += Time.deltaTime*(designatedPile.GetComponent<Pile>().littleGuysSpeed * Master.instance.littleGuysGlobalSpeedMultiplier); //Reaches end in 2 seconds initially, equivalent to +1 every 4 seconds.
        }
        else if (distance < 0.2f)
        {
            rb.velocity = Vector3.zero;
            timeToReach = 0;

            if (destination == mainPile)
            {
                destination = designatedPile.transform.position;
                prevLocation = mainPile;
                designatedPile.GetComponent<Pile>().MoneyDig();
            }
            else
            {
                destination = mainPile;
                prevLocation = designatedPile.transform.position;
            }
        }
    }
}
