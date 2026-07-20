using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimslyCharacterStats : AllyScript
{
    float grimslySpeed = 1;
    float grimslyPersonalSpace = 1;

    float grimslyMaxHealth = 1000;
    float grimslyRegen = 2f;
    float grimslyDamage = 50f;
    float grimslyInvinWindow = 1f;
    float grimslyAttackDuration = 0.2f;
    float grimslyCooldown = 1f;
    float grimslyRange = 2f;

    // Start is called before the first frame update
    protected override void Start()
    {
        speed = grimslySpeed;
        personalSpace = grimslyPersonalSpace;
        maxHealth = grimslyMaxHealth;
        regen = grimslyRegen;
        damage = grimslyDamage;
        invinWindow = grimslyInvinWindow;
        attackDuration = grimslyAttackDuration;
        cooldown = grimslyCooldown;
        range = grimslyRange;

        base.Start(); //load the stats before this you silly billy
    }

    // Update is called once per frame
    void Update()
    {
        
        {
            if (enemyDistance <= personalSpace + gameObject.GetComponent<CircleCollider2D>().radius && isDamaging == false) //attacks enemy when close enough through hitbox MELEE
            {
                Debug.Log("MELEE PUNCH enemy");
                isDamaging = true;
                StartCoroutine(Hurting());
                //yield return new WaitForSeconds(cooldown);
            }

        }
    }
    public IEnumerator Hurting() //hurting an enemy MELEE
    {
        GameObject currHit;
        currHit = hitbox;
        currHit = Instantiate(currHit, gameObject.transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
        currHit.GetComponent<Hitbox>().parentObj = gameObject;
        yield return new WaitForSeconds(cooldown);
        isDamaging = false;
    }





}