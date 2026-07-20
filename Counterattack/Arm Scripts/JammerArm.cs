using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JammerArm : ArmStats
{
    private Vector2 mousePosTarget;
    private Vector3 armToTargetDiff;

    public List<GameObject> enemyProj = new List<GameObject>();

    public float chanceToDud = 5f;

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

        isDurable = false;
        isWindUp = false;
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

        foreach (GameObject proj in GameObject.FindGameObjectsWithTag("EnemyProjectile"))
        {
            if (!enemyProj.Contains(proj))
            {
                enemyProj.Add(proj);
                float chance = Random.Range(0f, 100f);
                if (chance <= chanceToDud)
                {
                    proj.GetComponent<EnemyBehaviour>().damage = 0;
                    proj.GetComponent<SpriteRenderer>().material.color = new Color(0, 0, 0, 255); //black
                }
            }
        }

        for(int i = enemyProj.Count - 1;i >= 0; i--)
        {
            if(enemyProj[i] == null)
            {
                enemyProj.RemoveAt(i);
            }
        }

        mousePosTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        armToTargetDiff = (mousePosTarget - (Vector2)transform.position).normalized;

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
            chanceToDud += 5;
        }
        if (level == 3)
        {
            chanceToDud += 5;
        }
        if (level == 4)
        {
            chanceToDud += 5;
        }
        if (level == 5)
        {
            chanceToDud += 5;
        }
    }
}
