using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int speed = 1;
    public float distance;
    public float personalSpace = 1;
    public Transform target;
    private Rigidbody2D rb;
    

    public float health;
    public float maxHealth = 100;
    public float regen = 1;
    public float damage = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            Destroy(gameObject);
        }
        //distance = Vector2.Distance(transform.position, target.position);

        //if (distance > personalSpace)
        {
          //  transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (FindClosestEnemy() != null)
        {
            Vector3 direction = (FindClosestEnemy().transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
        
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
            }
        }
        return closest;
    }

}
