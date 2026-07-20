using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cap : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    public float cooldown = 1.5f;
    private List<GameObject> inTheAoE = new List<GameObject>();

    public float damage = 20f;
    private GameObject sevenSeas;
    public float seaSize = 3;

    // Start is called before the first frame update
    public void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        sevenSeas = Resources.Load("Sprites/Bullets/SevenSeas") as GameObject;

        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }

        StartCoroutine(AttackCooldown());
        sevenSeas = Instantiate(sevenSeas, transform.position, transform.rotation);
        sevenSeas.transform.parent = gameObject.transform;
        friendScript.sevenSeasFinder = sevenSeas.GetComponent<Collider2D>();
    }

    public void FixedUpdate()
    {
        sevenSeas.transform.localScale = Vector2.one * seaSize;

        foreach (GameObject friend in PlayerMovement.instance.friendArray) //find every friend and enables them to get on a boat if within the seas range
        {
            if(friend != null && friend.GetComponent<Friend>().sevenSeasFinder == null)
            {
                friend.GetComponent<Friend>().sevenSeasFinder = sevenSeas.GetComponent<Collider2D>();
            }
            
        }
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(cooldown * PlayerMovement.instance.moyaSupp);

        friendScript.jackBool = true;

        foreach(GameObject enemy in inTheAoE)
        {
            enemy.GetComponent<EnemyBehaviour>().health -= damage;
        }

        StartCoroutine(AttackCooldown());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && !inTheAoE.Contains(collision.gameObject))
        {
            inTheAoE.Add(collision.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(inTheAoE.Contains(collision.gameObject))
        {
            inTheAoE.Remove(collision.gameObject);
        }
    }

    public void OnDestroy()
    {
        Destroy(sevenSeas);
    }

    #region Level Ups
    public void OnLevelUp()
    {
        currLevel++;

        if(friendScript.level == 1)
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
        else if(friendScript.level == 8)
        {

        }
    }
    #endregion

}
