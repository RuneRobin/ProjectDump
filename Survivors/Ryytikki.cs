using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ryytikki : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    public float cooldown = 5;
    //public float duration = 5;
    //public float statSteal = 5;

    private GameObject mine;
    private GameObject minePrefab;


    // Start is called before the first frame update
    void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        minePrefab = Resources.Load("Sprites/Bullets/Mine") as GameObject;
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
        yield return new WaitUntil(() => friendScript.FindClosestEnemy() != null); //waits until there's an enemy around

        mine = Instantiate(minePrefab, transform.position, transform.rotation);

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
