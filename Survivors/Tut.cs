using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    private GameObject laser;
    private GameObject laserPrefab;
    public float cooldown = 1;
    public float laserCount = 1;
    public float laserSpeed = 5;

    private Vector3 diff;


    // Start is called before the first frame update
    public void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        laserPrefab = Resources.Load("Sprites/Bullets/Laser") as GameObject;
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


    public IEnumerator AttackCooldown()
    {
        yield return new WaitUntil(() => diff != Vector3.zero); //waits until there's an enemy around

        for (var i = 0; i < Mathf.RoundToInt(laserCount * PlayerMovement.instance.tofuSupp); i++)
        {
            laser = Instantiate(laserPrefab, transform.position, transform.rotation); //laser fireball
            Rigidbody2D laserRig = laser.GetComponent<Rigidbody2D>(); //laser rigidbody

            float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            laserRig.velocity = diff * laserSpeed;
            laser.transform.rotation = Quaternion.Euler(Vector3.forward * distance);

            laser.transform.Translate(new Vector3(0f, Random.Range(-0.5f, 0.5f))); //adds variation by Y axis relative to local space

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(cooldown * PlayerMovement.instance.moyaSupp);

        friendScript.jackBool = true;

        StartCoroutine(AttackCooldown());
        
    }

    #region Level Ups
    public void OnLevelUp()
    {
        currLevel++;

        if (currLevel == 1)
        {

        }
        else if (currLevel == 2)
        {


        }
        else if (currLevel == 3)
        {


        }
        else if (currLevel == 4)
        {


        }
        else if (currLevel == 5)
        {


        }
        else if (currLevel == 6)
        {


        }
        else if (currLevel == 7)
        {


        }
        else if (currLevel == 8)
        {

        }
    }
    #endregion

}
