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

    public bool isHoming = false;
    public bool parried = false;
    public bool isDestructable = true;

    public int damage = 1;

    public Vector2 influence;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(CharacterLoader.instance.currentlyFighting == false)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        direction = (FindClosestEnemy().transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, target.position);
        if (FindClosestEnemy() != null && isHoming == true)
        {
            if (distance > personalSpace)
            {
                rb.MovePosition((Vector2)transform.position + (new Vector2(direction.x * speed, direction.y * speed) + influence) * Time.deltaTime);
            }
            else
            {
                rb.MovePosition(transform.position);
            }
        }

        if(damage == 0 && gameObject.tag == "EnemyProjectile")
        {
            if(distance < 4f)
            {
                Destroy(gameObject);
            }
        }

    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] allies;
        allies = GameObject.FindGameObjectsWithTag("Player");
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

}