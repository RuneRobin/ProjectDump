using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWellScript : MonoBehaviour
{

    private float pull;
    public float maxPull; //2 they can barely get out, 3 they get pulled back after like a second
    public float pullDamage;
    public List<GameObject> enemiesCaught;

    public Vector3 diff;

    // Start is called before the first frame update
    void Start()
    {
        enemiesCaught = new List<GameObject>();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if(enemiesCaught.Count != 0)
        {
            GetComponent<Rigidbody2D>().velocity = diff;
            Debug.Log("Yep");
        }else
        {
            GetComponent<Rigidbody2D>().velocity = diff * 2;
            Debug.Log("norp");
        }

        foreach(GameObject enemy in enemiesCaught)
        {
            Vector2 direction = (transform.position - enemy.transform.position).normalized;
            //Vector2 direction2 = (transform.position - enemy.GetComponent<EnemyBehaviour>().FindClosestEnemy().transform.position);

            enemy.GetComponent<EnemyBehaviour>().influence = direction * Mathf.Clamp(pull, 0, maxPull); 

            //if (Vector2.Dot(direction, direction2) > 0) //it has a greater value the more similar two vectors are. Always has a value from 1 to -1
            {
                //enemy.GetComponent<Rigidbody2D>().MovePosition((Vector2)enemy.transform.position + enemy.GetComponent<EnemyBehaviour>().influence * Time.fixedDeltaTime);
            }
        }
        pull += Time.fixedDeltaTime;
    }

    public void OnTriggerEnter2D(Collider2D enemy)
    {
        if(enemy.tag == "Enemy" && enemy is CircleCollider2D)
        {
            enemiesCaught.Add(enemy.gameObject);
        }
    }

    public void OnTriggerStay2D(Collider2D enemy)
    {
        if(enemy.tag == "Enemy")
        {
            enemy.GetComponent<EnemyBehaviour>().health -= pullDamage * Time.fixedDeltaTime;
            //enemy.GetComponent<Collider2D>().isTrigger = true;
        }
    }

    public void OnTriggerExit2D(Collider2D enemy)
    {
        if(enemy.tag == "Enemy" && enemiesCaught.Contains(enemy.gameObject))
        {
            enemiesCaught.Remove(enemy.gameObject);
            enemy.GetComponent<EnemyBehaviour>().influence = Vector2.zero;
            enemy.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
