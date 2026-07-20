using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tofu : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    private GameObject tofuBeam;
    public float beamDamage = 1;

    public float duration = 5;
    public float cooldown = 5;

    public float bulletCount = 0.25f;
    public float damageBoost = 1;
    public float cooldownBoost = 1;


    public void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        tofuBeam = Resources.Load("Sprites/Bullets/TofuTofuBeam") as GameObject;
        tofuBeam = Instantiate(tofuBeam);
        tofuBeam.GetComponent<BeamBullet>().bulletDamage = beamDamage;
        tofuBeam.transform.parent = gameObject.transform;
        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }
        StartCoroutine(AttackCooldown());
    }

    public void OnDestroy()
    {
        Destroy(tofuBeam);
    }

    public void Update()
    {
        tofuBeam.GetComponent<LineRenderer>().SetPositions(new Vector3[] {gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)});
    }


    public IEnumerator AttackCooldown() //How long before character attacks
    {
        //Every number of seconds, Tofu will cheer on the rest of the friends, boosting their variables.

        PlayerMovement.instance.tofuSupp += bulletCount;
        yield return new WaitForSeconds(duration);


        PlayerMovement.instance.tofuSupp -= bulletCount;
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