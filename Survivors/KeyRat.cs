using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRat : MonoBehaviour
{
    public Friend friendScript;

    public float cooldown = 1;
    public float damage = 100;
    //public GameObject target;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private GameObject smokeScreen;
    private GameObject smokePrefab;


    // Start is called before the first frame update
    public void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        smokePrefab = Resources.Load("Sprites/Bullets/Smokescreen") as GameObject;
        OnLevelUp(); //for when Jack gets the script
        StartCoroutine(AttackCooldown());
        
    }

    public void FixedUpdate()
    {
        if (friendScript.FindClosestEnemy() != null && friendScript.goneWalkabout == true) //seeks closest enemy for stabbings
        {
            rb.MovePosition(transform.position + friendScript.closestEnemyDirection * friendScript.speed * Time.deltaTime);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
            friendScript.isDamaged = true;
        }

        if (friendScript.closestEnemyDistance <= gameObject.GetComponent<CircleCollider2D>().radius && friendScript.goneWalkabout == true) //Key stabbers for big damages
        {
            try
            {
                friendScript.FindClosestEnemy().GetComponent<EnemyBehaviour>().health -= damage;
            }
            catch
            {
                /*smokeScreen = Instantiate(smokePrefab, transform.position, transform.rotation);
                friendScript.goneWalkabout = false;
                gameObject.tag = "Ally";
                friendScript.jackBool = true;
                StartCoroutine(AttackCooldown());*/

                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
            }
            //ask jake if this is redundant
            smokeScreen = Instantiate(smokePrefab, transform.position, transform.rotation);
            friendScript.goneWalkabout = false;
            gameObject.tag = "Ally";
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
            friendScript.jackBool = true;
            StartCoroutine(AttackCooldown());
        }

        if(friendScript.FindClosestEnemy() == null)
        {
            gameObject.tag = "Ally";
            friendScript.goneWalkabout = false;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
            friendScript.jackBool = true;
            StartCoroutine(AttackCooldown());
        }

    }

    public IEnumerator AttackCooldown()
    {
            yield return new WaitForSeconds(cooldown * PlayerMovement.instance.moyaSupp);
            yield return new WaitUntil(() => friendScript.FindClosestEnemy() != null); //waits until there's an enemy around
            friendScript.isDamaged = false;
            friendScript.goneWalkabout = true;
            gameObject.tag = "Untagged";
            smokeScreen = Instantiate(smokePrefab, transform.position, transform.rotation);

        yield break;

    }

    #region Level Ups
    public void OnLevelUp()
    {
        if (friendScript.level == 1)
        {

        }
        else if (friendScript.level == 2)
        {


        }
        else if (friendScript.level == 3)
        {


        }
        else if (friendScript.level == 4)
        {


        }
        else if (friendScript.level == 5)
        {


        }
        else if (friendScript.level == 6)
        {


        }
        else if (friendScript.level == 7)
        {


        }
        else if (friendScript.level == 8)
        {

        }
    }
    #endregion

}
