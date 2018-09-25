using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class Abilities : MonoBehaviour
{
    public Camera camera;
    public GameObject player;

    public Material mat;
    public float platChangeMana;

    public float speed;
    PlatformerCharacter2D platChar2D;

    public bool jump = false;

    public bool gravityWell = false;

    public bool fire = false;
    public bool onFire = false;

    public bool slow = false;

    public bool invoked = false;

    public PhysicsMaterial2D bounce;

    public GameObject fireParticle;

    // Use this for initialization
    void Start ()
    {
        mat = gameObject.GetComponent<SpriteRenderer>().sharedMaterial;
        speed = player.GetComponent<PlatformerCharacter2D>().m_MaxSpeed;
        platChar2D = player.GetComponent<PlatformerCharacter2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        platChangeMana = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlatformChanger>().Mana;

        if (gravityWell == true)
        {
            Invoking();
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }

        if (jump == true)
        {
            Invoking();
        }

        if(fire == true)
        {
            Invoking();
            if(onFire == false)
            {
                onFire = true;
                Instantiate(fireParticle, GetComponentInParent<Transform>(), false);
            }
            
            
        }
        else if (fire == false && onFire == true)
        {
            onFire = false;
            Destroy(transform.Find("FireParticles(Clone)").gameObject);
            
        }

        if(slow == true)
        {
            Invoking();
        }

        if (platChangeMana <= 0.0f)
        {
            CancelInvoke("ManaDrain");
            CancelInvoke("Burning");
            CancelInvoke("SlowDown");
            invoked = false;
        }

    }

    public void Invoking()
    {
       
        if (invoked == false)
        {
          InvokeRepeating("ManaDrain", 0.0f, 0.1f);
          invoked = true;
        }
    }

    public void ResetAll()
    {
        jump = false;
        gravityWell = false;
        fire = false;
        slow = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        #region Jumping
        if (jump == true)
        {
            gameObject.GetComponent<Collider2D>().sharedMaterial = bounce;
        }
        else
        {
            gameObject.GetComponent<Collider2D>().sharedMaterial = null;
        } 
        #endregion
    }

    void OnCollisionStay2D(Collision2D c)
    {
        #region SlowDown
        if (c.gameObject.tag == "Player" && slow == true)
        {
            platChar2D.m_MaxSpeed = 4.0f;
        }

        if(c.gameObject.tag == "enemy" && slow == true)
        {
            c.gameObject.GetComponent<Enemy>().moveSpeed = 1.0f;
        }
        #endregion

        #region Fire
        if (c.gameObject.tag == "enemy" && fire == true)
        {
            c.gameObject.GetComponent<Enemy>().onFire = true;
        }
        
        if(c.gameObject.tag == "Player" && fire == true)
        {
            c.gameObject.GetComponentInChildren<PlatformChanger>().onFire = true;
        }
        #endregion
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        #region Gravity Well
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "enemy")
           {
            Vector2 newPos = gameObject.transform.position;

            newPos = new Vector2(newPos.x + (gameObject.GetComponent<BoxCollider2D>().size.x /2.0f), newPos.y + (gameObject.GetComponent<BoxCollider2D>().size.y * 2));

            //Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<CircleCollider2D>(), ignore:true);

            other.transform.position = Vector2.MoveTowards(other.transform.position, newPos, 4 * Time.deltaTime);
           }
        #endregion

    }

    public void OnCollisionExit2D(Collision2D other) // Cancel contact effects when not touching platform
    {
        if (other.gameObject.tag == "Player")
        {
            platChar2D.m_MaxSpeed = 10.0f; //negates slow by putting speed back to default
        }
        if(other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponent<Enemy>().moveSpeed = 3.0f;
        }
    }



    void ManaDrain()
    {
        
        if (jump == true)
        {GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlatformChanger>().Mana -= 30 * Time.deltaTime;}

        if(gravityWell == true)
        {GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlatformChanger>().Mana -= 50 * Time.deltaTime;}

        if(fire == true)
        { GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlatformChanger>().Mana -= 35 * Time.deltaTime;}

        if(slow == true)
        {GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlatformChanger>().Mana -= 25 * Time.deltaTime;}
    }

}
