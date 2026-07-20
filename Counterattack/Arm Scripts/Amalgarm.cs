using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amalgarm : ArmStats
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

        //unique to sword type arms, need to think how to handle amalgarm
        //CharacterLoader.instance.swordTypeArmList.Add(gameObject);
        //player.SwordRotationPlacement();

        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;

        timeOffset = Random.Range(0, 100);

        startingPos = transform.localPosition;
        startingRot = transform.localRotation;

        level = 1;
        damage = 5;
        speed = 30;
        armType = "FistSwordGunUtility";
        purchaseCost = 1;

        windUp = 0;
        windUpTimer = windUp;
        timerFill = Instantiate(timerFill, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(0, 0, 0));
        timerFill.transform.SetParent(CharacterLoader.instance.gameObject.transform.Find("WindUpTimers"));

        durability = 1;

        canBleed = true;
        bleed = 0;
        canBurn = true;
        burn = 0;
        canShock = true;
        shock = 0;
        canFreeze = true;
        freeze = 0;
        canVirus = true;
        virus = 0;

        
        isDurable = true;
        isWindUp = true;
        isFollowUp = true;
        autonomous = true;
        drone = true;

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
        if(levelling == true)
        {
            LevelUp();
            levelling = false;
        }

        if (CharacterLoader.instance.currentlyFighting == true)
        {
            Mathf.Clamp(cooldownTimer += Time.deltaTime, 0, 1 - bonusCooldown);
            windUpTimer -= Time.unscaledDeltaTime;
            if (windUpTimer < 0)
            {
                windUpTimer = 0;
            }

            mousePosTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            armToTargetDiff = (mousePosTarget - (Vector2)transform.position).normalized;
            if (Input.GetMouseButton(0) && isAttacking == false && windUpTimer == 0 && cooldownTimer >= 1 - bonusCooldown)
            {
                isAttacking = true;
                Attack();
            }
        }
        if(CharacterLoader.instance.currentlyFighting == false && windUpTimer != windUp - bonusWindUpReduction)
        {
            windUpTimer = windUp - bonusWindUpReduction;
        }

        timerFill.fillAmount = windUpTimer / (windUp + 1);
        timerFill.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);

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
        rb.velocity = armToTargetDiff * (speed + bonusFistSpeed + bonusGunBulletSpeed + player.bonusSwordArmSpeed);
        gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * distance);

        player.timesArmsHaveWoundUp++;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
        if (collision.tag == "Enemy" && enemy.openToAttack == true)
        {
            if(Random.Range(0,101) <= critChance + bonusCritChance) //Sword Crits
            {
                //crit effect?
                enemy.health -= Mathf.CeilToInt((damage + bonusDamage + bonusSwordDamage + bonusFistDamage + bonusGunDamage) * (critDamage + bonusCritDamage));
            }
            else
            {
                enemy.health -= damage + bonusDamage + bonusSwordDamage + bonusFistDamage + bonusGunDamage;
            }

            enemy.woOExtensionStacks++; //fist WoO extension stacks
            
            enemy.burnDamage += burn + bonusBurn;
            enemy.extinguishTimer = 0;
            enemy.bleedDamage += bleed + bonusBleed;
            enemy.shockStacks += shock + bonusShock;
            enemy.freezeStacks += freeze + bonusFreeze;
            enemy.virusStacks += virus + bonusVirus;

            isAttacking = false;
            rb.velocity = Vector3.zero;
            rb.simulated = false;
            cooldownTimer = 0;
            gameObject.transform.localPosition = startingPos;
            gameObject.transform.localRotation = startingRot;
            windUpTimer = windUp - bonusWindUpReduction;
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

            windUpTimer = windUp - bonusWindUpReduction;
        }
    }

    public void LevelUp()
    {
        if(level == 2)
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
            bleed += 1;
            burn += 1;
            shock += 1;
            freeze += 1;
            virus += 1;
        }
    }
}
