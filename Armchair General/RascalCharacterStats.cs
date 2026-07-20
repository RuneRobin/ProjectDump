using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RascalCharacterStats : AllyScript
{
    float rascalSpeed = 1;
    float rascalPersonalSpace = 1;

    float rascalMaxHealth = 100;
    float rascalRegen = 0.5f;
    float rascalDamage = 2f;
    float rascalInvinWindow = 1f;
    float rascalAttackDuration = 0.2f;
    float rascalCooldown = 0.1f;
    float rascalRange = 10f;
    float rascalBulletSpeed = 50f;

    /////////////////////////PROBABLY MOVE THIS TO A RANGE UNIT SCRIPT AT SOME POINT///////////////////////////
    public GameObject bullet;
    public bool entersRange;
    public float accuracyOffset = 20;


    // Start is called before the first frame update
    protected override void Start()
    {


        speed = rascalSpeed;
        personalSpace = rascalPersonalSpace;
        maxHealth = rascalMaxHealth;
        regen = rascalRegen;
        damage = rascalDamage;
        invinWindow = rascalInvinWindow;
        attackDuration = rascalAttackDuration;
        cooldown = rascalCooldown;
        range = rascalRange;
        bulletSpeed = rascalBulletSpeed;

        bullet = Resources.Load<GameObject>("Bullets/fireball"); //change at some point to her own bullets, imagine not pawning off all your baggage to your future self couldn't be me

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
            Vector2 direction = Quaternion.Euler(0, 0, Random.Range(-accuracyOffset, accuracyOffset)) * (FindClosestEnemy().transform.position - transform.position).normalized;

            if (entersRange == true)
            {
                GameObject bulletPrefab;
                bulletPrefab = Instantiate(bullet, transform.position, transform.rotation);
                bulletPrefab.transform.right = direction; //rotates towards enemy, the fact this is all it takes wowow
                bulletPrefab.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
                bulletPrefab.GetComponent<BulletHandler>().bulletDamage = damage;
                bulletPrefab.GetComponent<BulletHandler>().pierces = true;
                yield return new WaitForSeconds(cooldown);
            }
        }
        entersRange = false;

    }
}
