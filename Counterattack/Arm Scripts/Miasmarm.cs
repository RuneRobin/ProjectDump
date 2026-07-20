using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miasmarm : ArmStats
{
    private Vector2 mousePosTarget;
    private Vector3 armToTargetDiff;

    private PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerScript.instance;
        player.currArmCollection.Add(gameObject);

        timeOffset = Random.Range(0, 100);

        startingPos = transform.localPosition;
        startingRot = transform.localRotation;

        level = 1;
        damage = 0;
        speed = 30;
        armType = "Utility";
        purchaseCost = 1;

        canBleed = true;
        bleed = 1;
        canBurn = true;
        burn = 1;
        canShock = true;
        shock = 1;
        canFreeze = true;
        freeze = 1;
        canVirus = true;
        virus = 1;

        windUp = 9;
        windUpTimer = windUp;
        timerFill = Instantiate(timerFill, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(0, 0, 0));
        timerFill.transform.SetParent(CharacterLoader.instance.gameObject.transform.Find("WindUpTimers"));


        isDurable = false;
        isWindUp = true;
        isFollowUp = false;
        autonomous = true;
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
            windUpTimer -= Time.unscaledDeltaTime;
            if (windUpTimer < 0)
            {
                windUpTimer = 0;
            }

            mousePosTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            armToTargetDiff = (mousePosTarget - (Vector2)transform.position).normalized;

        }
        if (CharacterLoader.instance.currentlyFighting == false && windUpTimer != windUp - bonusWindUpReduction)
        {
            windUpTimer = windUp - bonusWindUpReduction;
        }

        if(windUpTimer == 0)
        {
            player.timesArmsHaveWoundUp++;
            windUpTimer = windUp - bonusWindUpReduction;
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                EnemyStats stats = enemy.GetComponent<EnemyStats>();
                stats.bleedDamage += bleed;
                stats.burnDamage += burn;
                stats.extinguishTimer = 0;
                stats.shockStacks += shock;
                stats.freezeStacks += freeze;
                stats.virusStacks += virus;
            }
        }

        timerFill.fillAmount = windUpTimer / windUp;
        timerFill.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);

        if (isAttacking == false) //hovering
        {
            Vector2 pos = transform.localPosition;
            float newY = startingPos.y + 0.3f * Mathf.Sin(Time.time + timeOffset);
            transform.localPosition = new Vector2(pos.x, newY);
        }
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
