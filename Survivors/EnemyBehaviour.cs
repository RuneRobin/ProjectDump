using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed = 1;
    public float distance;
    public float personalSpace = 1;
    public Transform target;
    private Rigidbody2D rb;
    public GameObject expDrop;
    public GameObject money;

    public float health;
    public float maxHealth = 100;
    public float regen = 0;
    public float damage = 15;

    public Vector2 influence;
    public Vector3 direction;

    #region Status Conditions
    public bool isShocked = false;

    public bool bleeding = false;
    public float bleedTimer;

    public bool boiling = false;
    public float boilingTimer = 0;
    public int boilingStacks = 0;
    public int boilingMaxStacks = 3;
    public float boilingTicks = 0;
    public float boilingDamage;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        expDrop = Resources.Load<GameObject>("Sprites/Items/XPellet");
        money = Resources.Load<GameObject>("Sprites/Items/Coin");
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < maxHealth) // health regen
        {
            health += regen * Time.deltaTime;
        }

        if(health <= 0)
        {
            if(Random.Range(0f,100f) <= 15)
            {
                Instantiate(money, transform.position, transform.rotation);
            }
            Instantiate(expDrop, transform.position, transform.rotation);

            if (bleeding == true)
            {
                GameObject.Find("Key").GetComponent<KeyMaid>().StartCoroutine("FreeAttack");

            }

            Master.instance.enemiesDefeated++;
            Destroy(gameObject);
        }

        speed = Mathf.Clamp(speed, 0, speed);
        //

        //if (distance > personalSpace)
        {
          //  transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (FindClosestEnemy() != null)
        {
            direction = (FindClosestEnemy().transform.position - transform.position).normalized;
            distance = Vector2.Distance(transform.position, target.position);
            //rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
            if (distance > personalSpace)
            {
                //rb.velocity = Vector2.MoveTowards(rb.velocity, new Vector2(direction.x * speed, direction.y * speed), 5 * Time.fixedDeltaTime);
                rb.MovePosition((Vector2)transform.position + (new Vector2(direction.x * speed, direction.y * speed) + influence) * Time.deltaTime);
            }
            else
            {
                //rb.velocity = new Vector2(0,0);
                rb.MovePosition(transform.position);
            }
        }

        #region Bleeding Status Effect
        //BLEED STATUS EFFECT
        if (bleeding == true && bleedTimer < 5)
        {
            health -= (maxHealth / 100) * Time.fixedDeltaTime;
            bleedTimer += Time.fixedDeltaTime;
        }
        #endregion

        #region Boiling Status Effect
        //BOILING STATUS EFFECT
        if (boiling == false) //unboiling the target per second, reducing boiling damage stacking
        {
            if(boilingTimer > 0)
            {
                boilingTimer -= Time.fixedDeltaTime;
                if(boilingTimer < 0)
                {
                    boilingTimer = 0;
                }
            }
            if(boilingTimer < boilingStacks && boilingStacks > 0)
            {
                boilingStacks--;
            }
        }
        if(boiling == true && boilingTicks < 1) //counting for the tick of damage
        {
            boilingTicks += Time.fixedDeltaTime;
            if(boilingTimer < boilingMaxStacks)
            {
                boilingTimer += Time.fixedDeltaTime;
                if(boilingTimer > boilingMaxStacks)
                {
                    boilingTimer = boilingMaxStacks;
                }
            }
        }
        if(boilingTimer >= 1+boilingStacks && boilingStacks < boilingMaxStacks) //longer it stays boiling the more stacks it gets, increasing damage
        {
            boilingStacks++;
        }
        if(boilingTicks >= 1)
        {
            health -= boilingDamage * (1 + boilingStacks);
            boilingDamage = 0;
            boilingTicks = 0;
        }

        float col = 1f - boilingStacks / 3f; // converts 0 - 3 to 1 - 0
        col = 0.3f + col * 0.7f; // 0.3 is the minimum brightness, 0.7 is (1 - minimum)
        GetComponent<SpriteRenderer>().color = new Color(col, col, col, 1f);
        #endregion

    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] allies;
        allies = GameObject.FindGameObjectsWithTag("Ally");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject ally in allies)
        {
            Vector3 diff = ally.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = ally;
                distance = curDistance;
                target = closest.transform;
            }
        }
        return closest;
    }

    public IEnumerator Shocked()
    {
        speed = 0;

        yield return new WaitForSeconds(3); //Change to dynamically increase status durations

        speed = 1;

        yield return new WaitForSeconds(3);

        isShocked = false;
    }

}