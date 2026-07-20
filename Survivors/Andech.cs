using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Andech : MonoBehaviour
{
    private GameObject player;
    public List<Type> friendScripts;

    public float range;
    public float siphon = 0.01f;
    public float bloodBank;
    private GameObject bloodBlast;
    private GameObject siphonIndicator;
    private GameObject bloodParticle;
    private GameObject particlePrefab;

    private bool keepSuccin = true;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerMovement.instance.gameObject;
        range = PlayerMovement.instance.pickUpRange;
        bloodBlast = Resources.Load("Sprites/Bullets/BloodBlast") as GameObject;
        particlePrefab = Resources.Load("Sprites/Items/BloodParticle") as GameObject;
        siphonIndicator = Resources.Load("Sprites/Bullets/SiphonRangeIndicator") as GameObject;
        siphonIndicator = Instantiate(siphonIndicator, player.transform.position, player.transform.rotation);
        siphonIndicator.transform.localScale = Vector2.one * range;
        StartCoroutine(Timer());
        StartCoroutine(AttackCooldown());
    }

    // Update is called once per frame
    public void Update()
    {
        siphonIndicator.transform.position = player.transform.position;
        transform.position = player.transform.position + new Vector3(0, 1, 0);
    }

    public IEnumerator AttackCooldown()
    {

        friendScripts = new List<Type>(); //resets lists


        foreach (GameObject friend in PlayerMovement.instance.friendArray)
        {
                friendScripts.Add(friend.GetComponent<Friend>().swapScript.GetType()); //adds all scripts to a list
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Vector3 diff = enemy.transform.position - player.transform.position; //seeing as technically Andech is just slightly above the player
            float curDistance = diff.sqrMagnitude;
            if (curDistance < range)
            {
            enemy.GetComponent<EnemyBehaviour>().health -= enemy.GetComponent<EnemyBehaviour>().maxHealth * siphon; //deals %health making it much better in the late game
            bloodBank += enemy.GetComponent<EnemyBehaviour>().maxHealth * siphon;
            bloodParticle = Instantiate(particlePrefab, enemy.transform.position, enemy.transform.rotation);
            }
        }

        yield return new WaitForSeconds(2.5f);
        if(keepSuccin == true)
        {
            StartCoroutine(AttackCooldown());
        }
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(12.5f);
        keepSuccin = false;
        StopCoroutine(AttackCooldown());

        bloodBlast = Instantiate(bloodBlast, transform.position, transform.rotation);
        bloodBlast.transform.localScale = Vector2.one * range * 5;
        bloodBlast.GetComponent<Soundwave>().damage = bloodBank;

        foreach (GameObject friend in PlayerMovement.instance.friendArray)
        {
            friend.GetComponent<Friend>().health += bloodBank * 0.10f; //heals by an initial 10% of siphoned health
        }

        yield return null;
    }
}
