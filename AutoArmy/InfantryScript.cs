using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryScript : MonoBehaviour
{
    public bool enemyInRange = false;
    public bool isAttacking = false;

    private GameObject swordGameObject;

    private UnitScript us;

    private float sb = 1f; //Stat Boost, change to attack speed later on if more things use this?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        us = gameObject.GetComponent<UnitScript>();
        us.job = 0; //pointless but shut up?
        us.personalSpace = 1.1f;

        swordGameObject = Instantiate(MasterController.instance.swordPrefab, us.holster.transform.position, us.holster.transform.rotation);
        swordGameObject.transform.parent = us.holster.transform;
        swordGameObject.GetComponent<SwordScript>().us = us;
        swordGameObject.GetComponent<SwordScript>().infSc = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttacking == false)
        {
            swordGameObject.transform.position = us.holster.transform.position;
            if(swordGameObject.transform.rotation != us.holster.transform.rotation)
            {
                swordGameObject.transform.rotation = us.holster.transform.rotation;
            }
        }
        else
        {
            if (us.isFlipped == false)
            {
                swordGameObject.transform.RotateAround(gameObject.transform.position, Vector3.back, Time.deltaTime * 100 * sb);
            }
            else
            {
                swordGameObject.transform.RotateAround(gameObject.transform.position, Vector3.forward, Time.deltaTime * 100 * sb);
            }
        }

        if (us.faction == "Highlander" && sb != gameObject.GetComponent<HighlanderSoldier>().statBoost) //exclusive attack cooldown buff for highlander
        {
            sb = gameObject.GetComponent<HighlanderSoldier>().statBoost;
        }
    }

    public IEnumerator MeleeAttack()
    {
        yield return new WaitForSeconds(Random.Range(0.5f,1f) /sb);
        float offset;
        float offset2;
        if(us.isFlipped == true)
        {
            offset = 135f;
            offset2 = -0.25f;
        }
        else
        {
            offset = 45f;
            offset2 = 0.25f;
        }
        swordGameObject.transform.rotation = Quaternion.Euler(0, 0, offset);
        swordGameObject.transform.position = new Vector3(gameObject.transform.position.x + offset2, gameObject.transform.position.y + 0.25f, 0);
        isAttacking = true;
        swordGameObject.GetComponent<BoxCollider2D>().enabled = true;//so the sword script's oncollisionenter doesn't activate while holstered
        SceneConst.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(SceneConst.instance.SFXCollection[Random.Range(18,20)]); //Sword Swing 1/2
        yield return new WaitForSeconds(1f/sb);
        isAttacking = false;
        swordGameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<UnitScript>().attacking = false;
    }
}
