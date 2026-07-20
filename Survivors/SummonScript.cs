using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonScript : MonoBehaviour
{

    public float health = 1;
    public float speed = 1;
    public float damage = 1;
    public float lifespan = 1;


    public Sprite[] listOfSprites;

    private bool isDamaged = false;
    private float invinWindow = 1f;

    private bool isDamaging = false;
    public float attackDelay = 0.5f;

    public GameObject summoner;

    // Start is called before the first frame update
    void Start()
    {
        Sprite randomSprite = listOfSprites[Random.Range(0,listOfSprites.Length)];
        gameObject.GetComponent<SpriteRenderer>().sprite = randomSprite;
        StartCoroutine(Lifespan());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (FindClosestEnemy() != null)
        {
            Vector3 direction = (FindClosestEnemy().transform.position - transform.position).normalized;
            gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position + direction * speed * Time.deltaTime);
        }

        if (health <= 0)
        { Destroy(gameObject); }
        
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] allies;
        allies = GameObject.FindGameObjectsWithTag("Enemy");
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

    public void OnDestroy()//might add fun on destroy effects here
    {
        if(summoner && summoner.GetComponent<Honey>())
        { 
            summoner.GetComponent<Honey>().summonsActive--; //finds honey by name and minus ones the active summons. Might need to change this if name gets changed
        }  
    }

    public IEnumerator Lifespan()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }

    public void OnTriggerStay2D(Collider2D collision) //comes into contact with enemy
    {
        if (collision.gameObject.tag == "Enemy" && isDamaged == false)
        {
            isDamaged = true;
            health--;
            StartCoroutine(Hurt());

        }

        if(collision.gameObject.tag == "Enemy" && isDamaging == false)
        {
            isDamaging = true;
            collision.GetComponent<EnemyBehaviour>().health -= damage;
            StartCoroutine(Hurting());
        }


    }

    public IEnumerator Hurt() //hurt by an enemy
    {
        yield return new WaitForSeconds(invinWindow);
        isDamaged = false;
    }

    public IEnumerator Hurting()
    {
        yield return new WaitForSeconds(attackDelay);
        isDamaging = false;

    }

}
