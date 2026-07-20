using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestScript : MonoBehaviour
{
    private UnitScript us;

    public float heal = 20f;

    private bool isCasting = false;
    private bool casted = false;

    private GameObject staffGameObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        us = gameObject.GetComponent<UnitScript>();
        us.job = 2;
        us.personalSpace = 6f;

        staffGameObject = Instantiate(MasterController.instance.staffPrefab, us.holster.transform.position, us.holster.transform.rotation);
        staffGameObject.transform.parent = us.holster.transform;
        //staffGameObject.GetComponent<StaffScript>().us = us;
        //staffGameObject.GetComponent<StaffScript>().staSc = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Staff Animation, accounting for flipped positions
        if(isCasting == true && casted == false)
        {
            float offset = 1;
            float offset2 = 0;

            if(us.isFlipped == true)
            {
                offset = -1;
                offset2 = 180f;
            }

            casted = true;
            staffGameObject.transform.position = gameObject.transform.position + new Vector3(gameObject.GetComponent<SpriteRenderer>().size.x/2*offset, 0, 0);
            staffGameObject.transform.rotation = Quaternion.Euler(0, offset2, 90);
        }
        else if(isCasting == false && casted == true)
        {
            casted = false;
            isCasting = false;
            staffGameObject.transform.position = us.holster.transform.position;
            staffGameObject.transform.rotation = us.holster.transform.rotation;
        }
    }

    public GameObject FindInjuredAlly()
    {
        List<GameObject> allies;
        if(gameObject.GetComponent<UnitScript>().army == "ArmyOne")
        {
            allies = MasterController.instance.armyOne;
        }
        else
        {
            allies = MasterController.instance.armyTwo;
        }
        GameObject mostInjured = null;
        float lowestHealth = Mathf.Infinity;
        foreach (GameObject ally in allies)
        {
            if (ally != null && ally != gameObject) //in case they are destroyed during the loop, and so they don't always target themselves
            {
                if(ally.GetComponent<UnitScript>().health < lowestHealth)
                {
                    mostInjured = ally;
                    lowestHealth = mostInjured.GetComponent<UnitScript>().health;
                }
            }
        }
        return mostInjured;
    }

    public IEnumerator PriestAttack()
    {
        yield return new WaitForSeconds(5f);
        isCasting = true;
        if (FindInjuredAlly() != null) //in case it is destroyed during this frame
        {
            FindInjuredAlly().GetComponent<UnitScript>().health += heal;
            Instantiate(MasterController.instance.healPrefab, FindInjuredAlly().transform.position, transform.rotation);
            MasterController.instance.ArmyHealthUpdater(us.army);
        }
        SceneConst.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(SceneConst.instance.SFXCollection[Random.Range(9,11)]);

        yield return new WaitForSeconds(1f);
        isCasting = false;
        gameObject.GetComponent<UnitScript>().attacking = false;
    }
}
