using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiliCharacterStats : AllyScript
{
    float liliSpeed = 2;
    float liliPersonalSpace = 1;

    float liliMaxHealth = 100;
    float liliRegen = 5f;
    float liliDamage = 300;
    float liliInvinWindow = 0.5f;
    float liliAttackDuration = 0.5f;
    float liliCooldown = 1f;
    float liliRange = 1f;
    float liliBulletSpeed = 500f;

    /////////////////////////PROBABLY MOVE THIS TO A RANGE UNIT SCRIPT AT SOME POINT///////////////////////////
    public GameObject bullet;
    public bool entersRange;


    // Start is called before the first frame update
    protected override void Start()
    {
        speed = liliSpeed;
        personalSpace = liliPersonalSpace;
        maxHealth = liliMaxHealth;
        regen = liliRegen;
        damage = liliDamage;
        invinWindow = liliInvinWindow;
        attackDuration = liliAttackDuration;
        cooldown = liliCooldown;
        range = liliRange;
        bulletSpeed = liliBulletSpeed;
        isHealer = true; //sets this unit to follow allies instead of enemies

        bullet = Resources.Load<GameObject>("Bullets/Laser"); //change at some point to her own bullets, imagine not pawning off all your baggage to your future self couldn't be me

        base.Start(); //load the stats before this you silly billy
    }

    // Update is called once per frame
    void Update()
    {
        //if (enemyDistance <= range && entersRange == false) ///////////////////////ranged attack but melee if unit got that beeg range
        {
            //entersRange = true;
            //StartCoroutine(AttackCooldown());
        }
    }

    public IEnumerator AttackCooldown() /////////////////////////////RANGED ATTACK////////////////////////////////////////////
    {
        if (FindClosestEnemy() != null)
        {
            Vector2 direction = (FindClosestEnemy().transform.position - transform.position).normalized;

            if (entersRange == true)
            {
                GameObject bulletPrefab;
                bulletPrefab = Instantiate(bullet, transform.position, transform.rotation);
                bulletPrefab.transform.right = direction; //rotates towards enemy, the fact this is all it takes wowow
                bulletPrefab.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
                bulletPrefab.GetComponent<BulletHandler>().bulletDamage = damage;

                yield return new WaitForSeconds(cooldown);
            }
        }
        entersRange = false;

    }
}
