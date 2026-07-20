using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Denim : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    public float cooldown = 8;
    private GameObject denimBarrier;
    public float barrierMaxHealth = 100;
    public float barrierHealth = 100;
    public float barrierRegen = 1;
    public float barrierSize = 22.5f;
    public float barrierPush = 5;
    private bool coolingDown = false;

    public void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        denimBarrier = Resources.Load("Sprites/Bullets/BarrierCollider") as GameObject;
        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }
        denimBarrier = Instantiate(denimBarrier, PlayerMovement.instance.transform.position, transform.rotation);
        denimBarrier.transform.parent = PlayerMovement.instance.transform;
    }


    public void FixedUpdate()
    {
        if (barrierHealth <= 0 && coolingDown == false) //shield breaking
        {
            denimBarrier.SetActive(false);
            StartCoroutine(refreshCooldown());
        }

        if(barrierHealth < barrierMaxHealth && coolingDown == false) //regen
        {
            barrierHealth += barrierRegen * Time.deltaTime;
        }

        friendScript.jackBool = true;
    }

    public IEnumerator refreshCooldown()
    {
        coolingDown = true;
        yield return new WaitForSeconds(cooldown * PlayerMovement.instance.moyaSupp);
        barrierHealth = barrierMaxHealth;
        denimBarrier.SetActive(true);
        coolingDown = false;
    }

    public void OnDestroy()
    {
        Destroy(denimBarrier);
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
