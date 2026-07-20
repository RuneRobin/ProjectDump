using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ryan : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    public float cooldown = 3;
    public float swipeCount = 1;

    private GameObject swipe;
    private GameObject swipePrefab;

    private Vector3 diff;

    // Start is called before the first frame update
    public void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        swipePrefab = Resources.Load("Sprites/Bullets/Swipe") as GameObject;
        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }
        StartCoroutine(AttackCooldown());
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (friendScript.FindClosestEnemy() != null || Input.GetMouseButton(1)) //fucking finally this shit is fixed
        {
            diff = friendScript.closestEnemyDirection;
        }
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitUntil(() => diff != Vector3.zero); //waits until there's an enemy around

        float swipeCombo = 0;

        for (var i = 0; i < Mathf.RoundToInt(swipeCount * PlayerMovement.instance.tofuSupp); i++)
            {
            swipe = Instantiate(swipePrefab, transform.position, transform.rotation);

            float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            swipe.GetComponent<Rigidbody2D>().velocity = diff * 5;
            swipe.transform.rotation = Quaternion.Euler(Vector3.forward * (distance + 90f));
            swipe.transform.localScale += new Vector3(swipeCombo, 0, 0);
            swipeCombo += 5;

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
