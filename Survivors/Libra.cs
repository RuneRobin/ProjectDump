using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libra : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    private GameObject star;
    private GameObject starPrefab;
    public float cooldown = 1;
    public int starCount = 1;
    //public float starSpeed = 2;
    public Vector3 sky;

    public void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        starPrefab = Resources.Load("Sprites/Bullets/Star") as GameObject;
        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }
        StartCoroutine(AttackCooldown());
    }


    public IEnumerator AttackCooldown()
    {
        //float starRadius = Random.Range(-5f, 5f);

        for (var i = 0; i < Mathf.RoundToInt(starCount * PlayerMovement.instance.tofuSupp); i++)
        {
            float starX = Random.Range(-5f, 5f);
            float starY = Random.Range(5f, 10f);
            sky = new Vector3(starX, starY, 15);
            star = Instantiate(starPrefab, gameObject.transform.position + sky, transform.rotation); //create bullet

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
