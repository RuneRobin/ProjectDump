using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatArm : ArmStats
{
    private Vector2 mousePosTarget;
    private Vector3 armToTargetDiff;

    public GameObject bullet;
    public GameObject bulletPrefab;

    //private Rigidbody2D rb;

    private float accuracy;

    private PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerScript.instance;
        player.currArmCollection.Add(gameObject);

        //rb = GetComponent<Rigidbody2D>();
        timeOffset = Random.Range(0, 100);

        startingPos = transform.localPosition;
        startingRot = transform.localRotation;

        level = 1;
        damage = 1;
        speed = 40;
        accuracy = 10f;
        armType = "Gun";
        purchaseCost = 1;

        bulletCount = 16;
        bulletPrefab = Resources.Load("Sprites/Items/GatBullet") as GameObject;
        firerate = 0.1f;

        isDurable = false;
        isWindUp = false;
        isFollowUp = false;
        autonomous = false;
        drone = false;

        bonusDamage = player.damageBoost;

        bonusFistDamage = player.bonusFistArmDamage;
        bonusFistSpeed = player.bonusFistArmSpeed;

        bonusSwordDamage = player.bonusSwordArmDamage;
        bonusCritChance = player.bonusSwordArmCritChance;
        bonusCritDamage = player.bonusSwordArmCritDamage;

        bonusGunDamage = player.bonusGunArmDamage;
        bonusGunBulletSpeed = player.bonusGunArmBulletSpeed;
        bonusFirerate = player.bonusGunArmFirerate;
        bonusBulletCount = player.bonusGunArmBulletCount;

        bonusDurability = player.bonusArmDurability;
        bonusWindUpReduction = player.bonusArmWindUpReduction;

        bonusBleed = player.bonusArmBleed;
        bonusBurn = player.bonusArmBurn;
        bonusShock = player.bonusArmShock;
        bonusFreeze = player.bonusArmFreeze;
        bonusVirus = player.bonusArmVirus;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelling == true)
        {
            LevelUp();
            levelling = false;
        }

        if (CharacterLoader.instance.currentlyFighting == true)
        {
            mousePosTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            armToTargetDiff = (mousePosTarget - (Vector2)transform.position).normalized;
            if (Input.GetMouseButtonDown(0) && isAttacking == false)
            {
                isAttacking = true;
                StartCoroutine(Attack());
            }
        }

        if (isAttacking == false) //hovering
        {
            Vector2 pos = transform.localPosition;
            float newY = startingPos.y + 0.3f * Mathf.Sin(Time.time + timeOffset);
            transform.localPosition = new Vector2(pos.x, newY);
        }
    }

    public IEnumerator Attack()
    {
        float diff = Mathf.Atan2(armToTargetDiff.y, armToTargetDiff.x) * Mathf.Rad2Deg;
        Vector3 direction = armToTargetDiff * (speed + bonusGunBulletSpeed); //so that when each bullet is being fired it doesn't update to wherever the mouse is but instead they all fire towards the first shot's location
        float accOffset = 0;
        gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * diff); //gun rotation

        for (int i = 0; i < bulletCount + bonusBulletCount; i++)
        {
            accOffset += Random.Range(-5f, 5);
            Mathf.Clamp(accOffset, -accuracy, accuracy);
            if (accOffset >= accuracy - 2)
            {
                accOffset = accOffset - 5f;
            }
            if (accOffset <= -accuracy + 2)
            {
                accOffset = accOffset + 5f;
            }
            bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<BulletHandler>().bulletDamage = damage + bonusDamage; //the damages

            Vector3 shootDirection = Quaternion.AngleAxis((Random.Range(0f, 1f) * accuracy) - (accuracy / Mathf.PI), Vector3.forward) * direction;
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection;

            bullet.transform.rotation = Quaternion.Euler(Vector3.forward * diff + new Vector3(0, 0, accOffset));
            yield return new WaitForSeconds(firerate / bonusFirerate);
        }
        isAttacking = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public void OnTriggerExit2D(Collider2D collision)
    {

    }

    public void LevelUp()
    {
        if (level == 2)
        {
            damage += 1;
        }
        if (level == 3)
        {
            damage += 1;
        }
        if (level == 4)
        {
            damage += 1;
        }
        if (level == 5)
        {
            damage += 1;
        }
    }
}