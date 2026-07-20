using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserArm : ArmStats
{
    private Vector2 mousePosTarget;
    private Vector3 armToTargetDiff;

    private Rigidbody2D rb;

    private PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerScript.instance;
        player.currArmCollection.Add(gameObject);

        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;

        timeOffset = Random.Range(0, 100);

        startingPos = transform.localPosition;
        startingRot = transform.localRotation;

        level = 1;
        damage = 2;
        speed = 100;
        armType = "Fist";
        purchaseCost = 1;

        canShock = true;
        shock = 2;

        isDurable = false;
        isWindUp = false;
        isFollowUp = false;
        autonomous = false;
        drone = false;

        bonusDamage = player.damageBoost;

        bonusFistDamage = player.bonusFistArmDamage;
        bonusFistSpeed = player.bonusFistArmSpeed;
        bonusCooldown = player.bonusFistArmCooldownReduction;

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
            Mathf.Clamp(cooldownTimer += Time.deltaTime, 0, 1 - bonusCooldown);
            mousePosTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            armToTargetDiff = (mousePosTarget - (Vector2)transform.position).normalized;
            if (Input.GetMouseButton(0) && isAttacking == false && cooldownTimer >= 1 - bonusCooldown)
            {
                isAttacking = true;
                Attack();
            }
        }

        if (isAttacking == false) //hovering
        {
            Vector2 pos = transform.localPosition;
            float newY = startingPos.y + 0.3f * Mathf.Sin(Time.time + timeOffset);
            transform.localPosition = new Vector2(pos.x, newY);
        }
    }

    public void Attack()
    {
        float distance = Mathf.Atan2(armToTargetDiff.y, armToTargetDiff.x) * Mathf.Rad2Deg;
        rb.simulated = true;
        rb.velocity = armToTargetDiff * (speed + bonusFistSpeed);
        gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * distance);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.GetComponent<EnemyStats>().openToAttack == true)
        {
            collision.gameObject.GetComponent<EnemyStats>().health -= damage + bonusDamage;
            collision.gameObject.GetComponent<EnemyStats>().shockStacks += shock + bonusShock;
            isAttacking = false;
            rb.velocity = Vector3.zero;
            rb.simulated = false;
            cooldownTimer = 0;
            gameObject.transform.localPosition = startingPos;
            gameObject.transform.localRotation = startingRot;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Arena")
        {
            isAttacking = false;
            rb.velocity = Vector3.zero;
            rb.simulated = false;
            cooldownTimer = 0;
            gameObject.transform.localPosition = startingPos;
            gameObject.transform.localRotation = startingRot;
        }
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