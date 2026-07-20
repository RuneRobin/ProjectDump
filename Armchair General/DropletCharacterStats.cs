using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletCharacterStats : AllyScript
{
    float dropletSpeed = 1;
    float dropletPersonalSpace = 1;

    float dropletMaxHealth = 50;
    float dropletRegen = 0.5f;
    float dropletDamage = 300;
    float dropletInvinWindow = 0.5f;
    float dropletAttackDuration = 0.5f;
    float dropletCooldown = 2f;
    float dropletRange = 75f;
    float dropletBulletSpeed = 500f;

    /////////////////////////PROBABLY MOVE THIS TO A RANGE UNIT SCRIPT AT SOME POINT///////////////////////////
    public GameObject bullet;
    public bool entersRange;


    // Start is called before the first frame update
    protected override void Start()
    {
        

        speed = dropletSpeed;
        personalSpace = dropletPersonalSpace;
        maxHealth = dropletMaxHealth;
        regen = dropletRegen;
        damage = dropletDamage;
        invinWindow = dropletInvinWindow;
        attackDuration = dropletAttackDuration;
        cooldown = dropletCooldown;
        range = dropletRange;
        bulletSpeed = dropletBulletSpeed;

        bullet = Resources.Load<GameObject>("Bullets/Laser"); //change at some point to her own bullets, imagine not pawning off all your baggage to your future self couldn't be me

        base.Start(); //load the stats before this you silly billy
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyDistance <= range && entersRange == false) ///////////////////////ranged attack but melee if unit got that beeg range
        {
            entersRange = true;
            StartCoroutine(AttackCooldown());
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
