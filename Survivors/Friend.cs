using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public Transform previousFriendTransform;
    public int speed = 5;
    public float personalSpace = 1;
    public int currentFriend;
    public Vector2 positionInCircle;
    public Vector3 closestEnemyDirection;
    public float closestEnemyDistance;
    public bool goneWalkabout = false;

    public float health = 100;
    public float maxHealth = 100;
    public float regen = 0;
    public int level = 1;
    public bool isDamaged = false;
    public float invinWindow = 0.2f;


    //Cap's things
    public GameObject boat;
    public GameObject boatPrefab;
    private Vector2 boatOffset = new Vector2(0, -0.35f);
    public Collider2D sevenSeasFinder;

    public MonoBehaviour swapScript; //for Jacks script
    public bool jackBool = false;

    private void Start()
    {
        currentFriend = PlayerMovement.instance.friendCount; //Gets currentfriend number from master script in player character

        //previousFriendTransform = PlayerMovement.friends[PlayerMovement.friends.Count]; //minus because thing is 0 when should be one >:(
        PlayerMovement.instance.friendCount++; //Adds a new friend to the friend count

        //if (!gameObject.GetComponent<PlayerMovement>()) //checks to see that this friend isn't the player character
        {
            //PlayerMovement.friends.Add(gameObject); //The List version

            for(int i = 0; i < PlayerMovement.instance.friendArray.Length; i++) //The Array Version
            {
                if(PlayerMovement.instance.friendArray[i] == null)
                {
                    PlayerMovement.instance.friendArray[i] = gameObject;
                    i = PlayerMovement.instance.friendArray.Length; //ends it sooner
                }
            }
        }

        if(!gameObject.GetComponent<PlayerMovement>()) //checks to see that this friend isn't the player character
        {
            positionInCircle = transform.position;
        }

        FriendInit();
    }

    private void Update()
    {
        /*congaline script
        //distance = Vector2.Distance(transform.position, previousFriendTransform.position);

        //if (distance > personalSpace)
        //{
        //transform.position = Vector2.MoveTowards(transform.position, previousFriendTransform.position, speed * Time.deltaTime);
        }*/
        //^^^^^^^^^^NEVER delete this gem^^^^^^^^^^^^^^^^^^^//

        if(goneWalkabout == false && !gameObject.GetComponent<PlayerMovement>())
        {
            transform.position = positionInCircle;
        }

        if (health < maxHealth) // health regen
        {
            health += regen * Time.deltaTime;
        }

        if (FindClosestEnemy() != null && !Input.GetMouseButton(1)) //checks to see that they aren't out of formation for any reason and that this friend isn't the player character
        {
            closestEnemyDirection = (FindClosestEnemy().transform.position - transform.position).normalized; //returns the angle of the closest enemy near the character
        }
        else if(Input.GetMouseButton(1) || FindClosestEnemy() == null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            closestEnemyDirection = (mousePos - (Vector2)transform.position).normalized;
        }

        FriendUpdate();
    }

    public GameObject FindClosestEnemy() //finds closest enemy near the character
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        //Vector3 position = transform.position;
        foreach (GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = enemy;
                distance = curDistance;
                closestEnemyDistance = distance;
            }
        }
        return closest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == sevenSeasFinder && !transform.Find("Boat(Clone)"))
        {
            boat = Instantiate(boatPrefab, (Vector2)transform.position + boatOffset, transform.rotation);
            boat.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == sevenSeasFinder)
        {
            //collision = sevenSeasFinder;
            Destroy(boat);
        }
    }

    public void OnTriggerStay2D(Collider2D collision) //comes into contact with enemy
    {
        if(collision.gameObject.tag == "Enemy" && isDamaged == false)
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

    public virtual void FriendInit()
    {

    }


    public virtual void FriendUpdate()
    {
        
    }

}
