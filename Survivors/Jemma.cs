using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jemma : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    private GameObject bottlePrefab;
    private GameObject bottle;
    private GameObject splashZone;
    public float cooldown = 3;
    public float flaskCount = 1;
    public float flaskSpeed = 1;
    public float radius = 1;

    // Start is called before the first frame update
    public void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        bottlePrefab = Resources.Load("Sprites/Bullets/Milk Bottle") as GameObject;
        splashZone = Resources.Load("Sprites/Bullets/SplashZone") as GameObject;
        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }
        StartCoroutine(AttackCooldown());
    }


    public IEnumerator AttackCooldown() //How long before character attacks
    {
        for (var i = 0; i < Mathf.RoundToInt(flaskCount * PlayerMovement.instance.tofuSupp); i++)
        {

            bottle = Instantiate(bottlePrefab, gameObject.transform.position, transform.rotation); //create bullet

            float randomAngle = Random.Range(0, Mathf.PI*2);
            float x = Mathf.Cos(randomAngle) * radius;
            float y = Mathf.Sin(randomAngle) * radius;

            Vector2 direction = new Vector2(x, y);

            bottle.GetComponent<Rigidbody2D>().velocity = direction * flaskSpeed;

            StartCoroutine(bottleDistance(bottle));

        }

        yield return new WaitForSeconds(cooldown * PlayerMovement.instance.moyaSupp);

        friendScript.jackBool = true;

        StartCoroutine(AttackCooldown());
    }

    public IEnumerator bottleDistance(GameObject currBottle)
    {
        yield return new WaitForSeconds(1.5f);
        splashZone = Instantiate(splashZone, currBottle.transform.position, transform.rotation);
        Destroy(currBottle);
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
