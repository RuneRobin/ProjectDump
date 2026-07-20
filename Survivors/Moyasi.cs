using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moyasi : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    public float duration = 5;
    public float cooldown = 5;
    public float vinylCount = 1;

    public float cooldownBoost = -0.25f;
    public float healing = 15;

    private GameObject vinyl;
    private GameObject vinylPrefab;
    private Vector3 lastLocation;

    public void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        vinylPrefab = Resources.Load("Sprites/Bullets/Vinyl") as GameObject;
        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }
        StartCoroutine(AttackCooldown());
    }

    public void FixedUpdate()
    {

    }


    public IEnumerator AttackCooldown() //How long before character attacks
    {
        float radius = 0.5f;
        
        for (int i = 0; i < vinylCount; i++) //RADIAL SEPARATION CODE FOR THE LOVE OF GOD DON'T FORGET IT AGAIN
        {
            float circleposition = i / vinylCount;
            float x = Mathf.Sin(circleposition * Mathf.PI * 2.0f) * radius;
            float z = Mathf.Cos(circleposition * Mathf.PI * 2.0f) * radius;
            vinyl = Instantiate(vinylPrefab, transform.position + new Vector3(x,z,0), transform.rotation);
            vinyl.transform.parent = gameObject.transform;
            vinyl.GetComponent<VinylBullet>().originPoint = gameObject.transform;
            
        }
        
        yield return new WaitForSeconds(cooldown);        

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