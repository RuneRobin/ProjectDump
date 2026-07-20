using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheArm : ArmStats
{
    public int moreBleed;

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
        damage = 7;
        speed = 75;
        armType = "Sword";
        purchaseCost = 1;

        windUp = 12;
        windUpTimer = windUp;
        timerFill = Instantiate(timerFill, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(0, 0, 0));
        timerFill.transform.SetParent(CharacterLoader.instance.gameObject.transform.Find("WindUpTimers"));

        canBleed = true;
        bleed = 1;

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

        if (CharacterLoader.instance.currentlyFighting == false && (windUpTimer != windUp - bonusWindUpReduction || moreBleed > 0))
        {
            windUpTimer = windUp - bonusWindUpReduction;
            moreBleed = 0;
        }

        timerFill.fillAmount = windUpTimer / windUp;
        timerFill.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.GetComponent<EnemyStats>().openToAttack == true && windUpTimer <= 0)
        {
            collision.gameObject.GetComponent<EnemyStats>().health -= damage + bonusDamage;
            collision.gameObject.GetComponent<EnemyStats>().bleedDamage += bleed + bonusBleed + moreBleed;
            moreBleed += level;
            windUpTimer = windUp - bonusWindUpReduction;
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
