using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shift : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    public float cooldown = 7;
    public float wellSize = 2.5f;
    public float gravityPull = 1.5f;
    public float gravityDamage = 20;

    public GameObject gravityWell;
    public GameObject wellPrefab;

    private Vector3 diff;


    // Start is called before the first frame update
    void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        wellPrefab = Resources.Load("Sprites/Bullets/GravityWell") as GameObject;
        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }
        StartCoroutine(AttackCooldown());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (friendScript.FindClosestEnemy() != null || Input.GetMouseButton(1))
        {
            diff = friendScript.closestEnemyDirection;
        }
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitUntil(() => diff != Vector3.zero); //waits until there's an enemy around
        gravityWell = Instantiate(wellPrefab, transform.position, transform.rotation);
        gravityWell.transform.localScale = Vector2.one * wellSize;
        gravityWell.GetComponent<GravityWellScript>().maxPull = gravityPull;
        gravityWell.GetComponent<GravityWellScript>().pullDamage = gravityDamage;
        gravityWell.GetComponent<GravityWellScript>().diff = diff;

        float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        gravityWell.GetComponent<Rigidbody2D>().velocity = diff * 2;
        gravityWell.transform.rotation = Quaternion.Euler(Vector3.forward * distance);
        
        friendScript.jackBool = true;
        yield return new WaitForSeconds(cooldown * PlayerMovement.instance.moyaSupp);

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
