using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jake : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    public GameObject spore;
    private GameObject sporePrefab;
    public float cooldown = 5;
    public int sporeCount = 6;
    public float sporeSpeed = 3;
    public float coneSize = 30;

    private Vector3 diff;

    // Start is called before the first frame update
    public void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }
        sporePrefab = Resources.Load("Sprites/Bullets/Spore") as GameObject;
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
        int tofuBullBoost = Mathf.RoundToInt(sporeCount * PlayerMovement.instance.tofuSupp);

        for (var i = 0; i < tofuBullBoost; i++)
        {
            spore = Instantiate(sporePrefab, transform.position, transform.rotation);

            Rigidbody2D sporeRig = spore.GetComponent<Rigidbody2D>(); //fireballs rigidbody
            float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Vector3 shootDirection = Quaternion.AngleAxis((Random.Range(0f, 1f) * coneSize) - (coneSize / Mathf.PI), Vector3.forward) * diff;

            sporeRig.velocity = shootDirection * sporeSpeed;
            spore.transform.rotation = Quaternion.Euler(Vector3.forward * distance);

            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(cooldown * PlayerMovement.instance.moyaSupp);

        friendScript.jackBool = true;

        StartCoroutine(AttackCooldown());
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
