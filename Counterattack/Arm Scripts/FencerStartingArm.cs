using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencerStartingArm : ArmStats
{
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
        damage = 4;
        speed = 100;
        armType = "Sword";
        purchaseCost = 1;

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
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.GetComponent<EnemyStats>().openToAttack == true)
        {
            collision.gameObject.GetComponent<EnemyStats>().health -= damage + bonusDamage + bonusSwordDamage;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //just in case you want some additonal effect, idk man, doubt it
    }

    public void LevelUp()
    {
        if (level == 2)
        {
            damage += 1;
        }
        if (level == 3)
        {
            speed += 50;
        }
        if (level == 4)
        {
            damage += 1;
        }
        if (level == 5)
        {
            speed += 150;
        }
    }
}
