using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician : MonoBehaviour
{
    private EnemyStats enemyStats;

    private Vector2 offset;

    private GameObject player;
    private GameObject magPrefab;
    private GameObject mag;
    private GameObject cardPrefab;
    private GameObject card;
    public float cardSpeed = 5;
    public float cooldown = 1.5f;

    public bool original = true;
    private int randomLocation;
    private Vector3 attackLocation;
    private bool zeroTaken = false;
    private bool oneTaken = false;
    private bool twoTaken = false;
    private bool threeTaken = false;
    private int activeMags = 0;

    private Vector2 diff;

    private int tiredValue = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ParryPlayer");
        cardPrefab = Resources.Load("Enemy Assets/Laser") as GameObject;
        enemyStats = gameObject.GetComponent<EnemyStats>();

        offset = GetComponent<SpriteRenderer>().bounds.size;

        if (original == true)
        {
            magPrefab = gameObject;
            randomLocation = Random.Range(0, 4);

            if(randomLocation == 0)
            {
                transform.position = new Vector2(player.transform.position.x + -offset.x * 8, player.transform.position.y + offset.y * 8);
                attackLocation = transform.position;
                zeroTaken = true;
            }
            if (randomLocation == 1)
            {
                transform.position = new Vector2(player.transform.position.x + offset.x * 8, player.transform.position.y + offset.y * 8);
                attackLocation = transform.position;
                oneTaken = true;
            }
            if (randomLocation == 2)
            {
                transform.position = new Vector2(player.transform.position.x + -offset.x * 8, player.transform.position.y + -offset.y * 8);
                attackLocation = transform.position;
                twoTaken = true;
            }
            if (randomLocation == 3)
            {
                transform.position = new Vector2(player.transform.position.x + offset.x * 8, player.transform.position.y + -offset.y * 8);
                attackLocation = transform.position;
                threeTaken = true;
            }
            StartCoroutine(MoreMagicians());
        }
        StartCoroutine(Attack(tiredValue));
    }

    public void FixedUpdate()
    {
        diff = (player.transform.position - transform.position).normalized;
    }

    private IEnumerator Attack(int energy)
    {
        card = Instantiate(cardPrefab, transform.position, transform.rotation); //laser fireball
        Rigidbody2D laserRig = card.GetComponent<Rigidbody2D>(); //laser rigidbody

        float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        laserRig.velocity = diff * cardSpeed;
        card.transform.rotation = Quaternion.Euler(Vector3.forward * distance);

        yield return new WaitForSeconds(cooldown);

        gameObject.transform.position -= (attackLocation - player.transform.position) / 8;

        yield return new WaitForSeconds(Random.Range(0f,2f));

        if (energy > 1)
        {
            StartCoroutine(Attack(energy--));
        }
        else
        {
            enemyStats.woOTimer = tiredValue;
            yield return new WaitForSeconds(tiredValue);
            StartCoroutine(Attack(tiredValue));
        }
    }

    private IEnumerator MoreMagicians() //the patient needs more magicians to live
    {
        if(activeMags < 3 && original == true)
        {
            for(int i = 3; i > activeMags; i--)
            {
                if(zeroTaken == false)
                {
                    mag = Instantiate(magPrefab, new Vector2(player.transform.position.x + -offset.x * 8, player.transform.position.y + offset.y * 8), transform.rotation);
                    mag.GetComponent<Magician>().original = false;
                    mag.GetComponent<Magician>().attackLocation = mag.transform.position;
                    zeroTaken = true;
                }
                else if(oneTaken == false)
                {
                    mag = Instantiate(magPrefab, new Vector2(player.transform.position.x + offset.x * 8, player.transform.position.y + offset.y * 8), transform.rotation);
                    mag.GetComponent<Magician>().original = false;
                    mag.GetComponent<Magician>().attackLocation = mag.transform.position;
                    oneTaken = true;
                }
                else if (twoTaken == false)
                {
                    mag = Instantiate(magPrefab, new Vector2(player.transform.position.x + -offset.x * 8, player.transform.position.y + -offset.y * 8), transform.rotation);
                    mag.GetComponent<Magician>().original = false;
                    mag.GetComponent<Magician>().attackLocation = mag.transform.position;
                    twoTaken = true;
                }
                else if (threeTaken == false)
                {
                    mag = Instantiate(magPrefab, new Vector2(player.transform.position.x + offset.x * 8, player.transform.position.y + -offset.y * 8), transform.rotation);
                    mag.GetComponent<Magician>().original = false;
                    mag.GetComponent<Magician>().attackLocation = mag.transform.position;
                    threeTaken = true;
                }
            }
        }
        yield return null;
    }
}
