using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorArm : ArmStats
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
        damage = 2;
        speed = 100;
        armType = "Sword";
        purchaseCost = 1;

        canBleed = true;
        bleed = 1;

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

    public IEnumerator Schwing(GameObject enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
        enemyStats.health -= damage + bonusDamage;
        enemyStats.bleedDamage += bleed + bonusBleed;
        yield return new WaitForSeconds(0.2f);
        enemyStats.health -= damage + bonusDamage;
        enemyStats.bleedDamage += bleed + bonusBleed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.GetComponent<EnemyStats>().openToAttack == true)
        {
            StartCoroutine(Schwing(collision.gameObject));
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
