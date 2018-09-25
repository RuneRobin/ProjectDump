using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlatformChanger : MonoBehaviour {

    public string currentAbility = "empty";
    GameObject[] allPlats;

    public GameObject gravityWell;

    public float health = 100.0f;
    public float Mana = 100;

    float burning;
    public bool onFire = false;

    // Use this for initialization
    void Start ()
    {
        allPlats = GameObject.FindGameObjectsWithTag("platform");
        InvokeRepeating("ManaRegen", 0.0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        #region Script Cycle
        if (Input.GetKeyDown("q"))
        {
            if(currentAbility == "empty")
            {
                currentAbility = "jump";
            }
            else if(currentAbility == "jump")
            {
                currentAbility = "gravityWell";
            }
            else if(currentAbility == "gravityWell")
            {
                currentAbility = "fire";
            }
            else if(currentAbility == "fire")
            {
                currentAbility = "slow";
            }
            else if(currentAbility == "slow")
            {
                currentAbility = "empty";
            }
        }
        #endregion


        #region Cleanse All Alchemy
        if(Input.GetKey("e") || Mana <= 0) //deactivates all spells
        {
            foreach (GameObject p in allPlats)
            {
                
                p.GetComponent<Abilities>().ResetAll();
                p.GetComponent<SpriteRenderer>().sharedMaterial = p.GetComponent<Abilities>().mat;

            }

        }
        #endregion

        if (Mana >= 100)
        { Mana = 100; }
        
        if(health < 1)
        {
            SceneManager.LoadScene("Menu");
        }
        


        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider.tag == "platform")
            {
                SpriteRenderer rend = hit.collider.gameObject.GetComponent<SpriteRenderer>();

                if (currentAbility == "jump" && hit.collider.gameObject.GetComponent<Abilities>().jump == false)
                {
                    hit.collider.gameObject.GetComponent<Abilities>().jump = true;
                    rend.material.SetColor("_Color", Color.Lerp(Color.yellow, hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color, 0.5f));
                }
                else if(currentAbility == "gravityWell" && hit.collider.gameObject.GetComponent<Abilities>().gravityWell == false)
                {
                    hit.collider.gameObject.GetComponent<Abilities>().gravityWell = true;
                    rend.material.SetColor("_Color", Color.Lerp(Color.magenta, hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color, 0.5f));
                }
                else if(currentAbility == "fire" && hit.collider.gameObject.GetComponent<Abilities>().fire == false)
                {
                    hit.collider.gameObject.GetComponent<Abilities>().fire = true;
                    rend.material.SetColor("_Color", Color.Lerp(Color.red, hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color, 0.5f));
                }
                else if(currentAbility == "slow" && hit.collider.gameObject.GetComponent<Abilities>().slow == false)
                {
                    hit.collider.gameObject.GetComponent<Abilities>().slow = true;
                    rend.material.SetColor("_Color", Color.Lerp(Color.grey, hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color, 0.5f));
                }
            }
            
            
            
        }

        #region BurningUp
        if (onFire == true)
        {
            burning += Time.deltaTime * 1.0f;
            health -= Time.deltaTime * 1.0f;
            if (burning >= 3.0f)
            {
                onFire = false;
                burning = 0;
            }
        }
        #endregion
    }


    void ManaRegen()
    { Mana += 50 * Time.deltaTime; }

}
