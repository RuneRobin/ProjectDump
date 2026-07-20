using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyScript : MonoBehaviour
{
    public float speed;
    public float enemyDistance; //distance to the closest enemy
    public float allyDistance; //guess
    public float personalSpace;
    public GameObject target;
    private Rigidbody2D rb;

    public float health;
    public float maxHealth;
    public float regen;
    public float damage;
    public float invinWindow;
    public bool isDamaged = false;
    public bool isDamaging = false;
    public GameObject hitbox;
    public float attackDuration;
    public float cooldown;
    public float range;
    public float bulletSpeed;

    //unit differences
    public bool isHealer = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if(health <= 0)
        {
            Destroy(gameObject); //replace this with something fancier
        }

        if (health < maxHealth) // health regen
        {
            health += regen * Time.deltaTime;
        }

        if (health > maxHealth) //stops overhealing
        {
            health = maxHealth;
        }

        ////////////////////////////////////////////

        if (FindClosestEnemy() != null && isHealer == false) //moving towards closest enemy
        {
            Vector3 direction = (FindClosestEnemy().transform.position - transform.position).normalized;
            if (enemyDistance > range)
            {
                rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
            }            
        }
        else if(FindClosestAlly() != null && isHealer == true) //no seriously, guess
        {
            Vector3 direction = (FindClosestAlly().transform.position - transform.position).normalized;
            if (allyDistance > range)
            {
                rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
            }
        }
    }

    public GameObject FindClosestEnemy() //for units that attack
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float dis = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < dis)
            {
                closest = enemy;
                dis = curDistance;
                enemyDistance = dis;
            }
        }
        return closest;
    }

    public GameObject FindClosestAlly() //for units that heal
    {
        GameObject[] allies;
        allies = GameObject.FindGameObjectsWithTag("Ally");
        GameObject closest = null;
        float lowestHealth = Mathf.Infinity;
        Vector3 position = transform.position;
        
        foreach (GameObject ally in allies)
        {           
            Vector3 diff = ally.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            AllyScript targetHealth = ally.GetComponent<AllyScript>();
            if (targetHealth.health < lowestHealth && targetHealth.health != targetHealth.maxHealth) //returns the ally with the lowest health
            {
                closest = ally;
                lowestHealth = targetHealth.health;
                allyDistance = curDistance; //these 3 return the guy in question dw about these
            }
            else if (targetHealth.health != targetHealth.maxHealth) //returns the only damaged ally in the field
            {
                closest = ally;
                lowestHealth = targetHealth.health;
                allyDistance = curDistance;
            }
            else if(targetHealth.health < lowestHealth) //returns lowest health ally while all allies are full HP
            {
                closest = ally;
                lowestHealth = targetHealth.health;
                allyDistance = curDistance;
            }
        }
        return closest;
    }

    private void OnMouseOver()//to give the player the ability to swipe and summon many units but not all stacked on top of each other.
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<MasterController>().summonBlock = true;
    }

    private void OnMouseExit()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<MasterController>().summonBlock = false;
    }

    public void OnCollisionStay2D(Collision2D collision) //comes into contact with enemy
    {
        if (collision.gameObject.tag == "Enemy" && isDamaged == false)
        {
            isDamaged = true;
            health -= collision.gameObject.GetComponent<EnemyBehaviour>().damage;
            StartCoroutine(Hurt());
        }
    }

    public IEnumerator Hurt() //hurt by an enemy
    {
        yield return new WaitForSeconds(invinWindow);
        isDamaged = false;
    }
    
}
