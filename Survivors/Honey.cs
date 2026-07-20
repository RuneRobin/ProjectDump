using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honey : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    public float cooldown = 5f;
    private GameObject honeySummon;
    private GameObject summonPrefab;
    public int summonCount = 3;
    public int summonsActive = 0;

    public float summonHealth = 3;
    public float summonSpeed = 2;
    public float summonDamage = 5;
    public float summonLifespan = 5;

    // Start is called before the first frame update
    void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        summonPrefab = Resources.Load("Sprites/Bullets/Minion") as GameObject;
        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }
        StartCoroutine(AttackCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitUntil(() => friendScript.closestEnemyDirection != Vector3.zero);
        //yield return new WaitUntil(() => summonsActive < summonCount);//waits until there's an enemy around
        for (int i = 0; i < summonCount; i++)
        {
            if (summonsActive < summonCount)
            {
                honeySummon = Instantiate(summonPrefab, transform.position, transform.rotation);
                honeySummon.GetComponent<SummonScript>().health = summonHealth;
                honeySummon.GetComponent<SummonScript>().speed = summonSpeed;
                honeySummon.GetComponent<SummonScript>().damage = summonDamage;
                honeySummon.GetComponent<SummonScript>().lifespan = summonLifespan;
                honeySummon.GetComponent<SummonScript>().summoner = gameObject;
                summonsActive++;

                yield return new WaitForSeconds(0.2f);
            }
        }

        
        //yield return new WaitForSeconds(cooldown * PlayerMovement.instance.moyaSupp);
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
