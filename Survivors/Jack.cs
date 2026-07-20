using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Jack : MonoBehaviour
{
    public Friend friendScript;

    public float duration = 10;
    public float cooldown = 1;
    public List<Type> friendScripts;
    public Type impersonation;

    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(AttackCooldown());
    }


    // Update is called once per frame
    public  void FixedUpdate()
    {
        
    }

    public IEnumerator AttackCooldown()
    {
        
        yield return new WaitForSeconds(cooldown); //short cooldown between switches
        
        friendScripts = new List<Type>(); //resets list

        foreach (GameObject friend in PlayerMovement.instance.friendArray)
        {
            if (friend != null && friend.name != gameObject.name) //stops himself from adding himself to the list
            {
                friendScripts.Add(friend.GetComponent<Friend>().swapScript.GetType()); //adds all scripts to a list
            }
            
        }

        if (friendScripts.Count > 1) //to make sure it doesnt get rid of its only script in a 2 man scenario
        {
            friendScripts.Remove(impersonation); //so it doesn't roll the same one again
        }

        if (impersonation != null) //to avoid step one problems
        {
            friendScript.jackBool = false;
            yield return new WaitUntil(() => friendScript.jackBool == true);
            Destroy(gameObject.GetComponent(impersonation)); //destroys current script
            //friendScript.stopCoroutine = true;
        }

        impersonation = friendScripts[UnityEngine.Random.Range(0, friendScripts.Count)]; //chooses a random script from list of available scripts

        gameObject.AddComponent(impersonation); //adds it
        
        yield return new WaitForSeconds(duration); //how long the impersonation lasts for
        
        StartCoroutine(AttackCooldown());
        
    }

    #region Level Ups
    public void OnLevelUp()
    {
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