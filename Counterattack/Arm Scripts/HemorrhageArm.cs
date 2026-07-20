using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HemorrhageArm : ArmStats
{
    private int hemorrhage;
    private int refundBleed;

    private PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerScript.instance;
        player.currArmCollection.Add(gameObject);

        //unique to sword type arms
        CharacterLoader.instance.swordTypeArmList.Add(gameObject);
        player.SwordRotationPlacement();

        timeOffset = Random.Range(0, 100);

        level = 1;
        damage = 3;
        speed = 100;
        armType = "Sword";
        purchaseCost = 1;

        //wind up variable to make winding bigger or smaller depending buffs or otherwise
        windUp = 10;
        windUpTimer = windUp;
        timerFill = Instantiate(timerFill, transform.position + new Vector3(0,1,0), Quaternion.Euler(0,0,0));
        timerFill.transform.SetParent(CharacterLoader.instance.gameObject.transform.Find("WindUpTimers"));

        canBleed = true;
        bleed = 4;
        refundBleed = 0;

        isDurable = false;
        isWindUp = true;
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
            windUpTimer -= Time.unscaledDeltaTime;
            if (windUpTimer < 0)
            {
                windUpTimer = 0;
            }
        }

        if (CharacterLoader.instance.currentlyFighting == false && windUpTimer != windUp)
        {
            windUpTimer = windUp;
        }

        timerFill.fillAmount = windUpTimer / windUp;
        timerFill.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.GetComponent<EnemyStats>().openToAttack == true && windUpTimer <= 0)
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats.bleedDamage += bleed + bonusBleed;
            enemyStats.health -= damage + bonusDamage;
            hemorrhage = enemyStats.bleedDamage * (enemyStats.bleedDamage + 1) / 2; //1+2+3+4+ etc....
            enemyStats.bleedDamage = Mathf.CeilToInt(enemyStats.bleedDamage / 2 * refundBleed); //refund bleed at level 5 to give half the bleed stacks back when swinging
            enemyStats.health -= hemorrhage;
            hemorrhage = 0;
            windUpTimer = windUp - bonusWindUpReduction;
            player.timesArmsHaveWoundUp++;
            //me when the damage
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

    }

    public void LevelUp()
    {
        if (level == 2)
        {
            damage += 1;
            bleed += 1;
        }
        if (level == 3)
        {
            damage += 2;
            bleed += 2;
        }
        if (level == 4)
        {
            damage += 4;
            bleed += 3;
        }
        if (level == 5)
        {
            refundBleed = 1;
        }
    }
}