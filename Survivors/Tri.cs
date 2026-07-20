using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tri : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    private GameObject fireball;
    private GameObject fireballPrefab;
    public float cooldown = 3;
    public int fireCount = 3;
    public float fireSpeed = 4;
    public float coneSize = 30;

    private Vector3 diff;

    public void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        fireballPrefab = Resources.Load("Sprites/Bullets/Fireball") as GameObject;

        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }
        StartCoroutine(AttackCooldown());
    }

    public void FixedUpdate()
    {
        if (friendScript.FindClosestEnemy() != null || Input.GetMouseButton(1))
        {
            diff = friendScript.closestEnemyDirection;
        }
    }


    public IEnumerator AttackCooldown() //How long before character attacks
    {
        yield return new WaitUntil(() => diff != Vector3.zero); //waits until there's an enemy around

        int tofuBullBoost = Mathf.RoundToInt(fireCount * PlayerMovement.instance.tofuSupp);
        
            for (var i = 0; i < tofuBullBoost; i++)
            {
                fireball = Instantiate(fireballPrefab, transform.position, transform.rotation); //create fireball
                Rigidbody2D fireballRig = fireball.GetComponent<Rigidbody2D>(); //fireballs rigidbody

                float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

                Vector3 shootDirection = Quaternion.AngleAxis((i / (float)tofuBullBoost * coneSize) - (coneSize / Mathf.PI), Vector3.forward) * diff;

                fireballRig.velocity = shootDirection * fireSpeed;
                fireball.transform.rotation = Quaternion.Euler(Vector3.forward * distance);
            }
        
        yield return new WaitForSeconds(cooldown * PlayerMovement.instance.moyaSupp); //cooldown between each attack

        friendScript.jackBool = true;

        StartCoroutine(AttackCooldown()); //restart attack
    }

    #region Level Ups
    public void OnLevelUp()
    {
        currLevel++;

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