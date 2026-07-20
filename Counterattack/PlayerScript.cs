using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;
    
    public int maxHealth = 100;
    public int health;
    public int maxBlock = 10;
    public int block;
    public int blockRegen = 1;
    public float blockRegenTimer;
    public float blockRegenInterval = 5;
    public float speed = 5;

    public float pickUpRange = 3;
    public List<GameObject> pickUps;

    //public float parryWindow;

    public int money;
    public int enemiesDefeated;

    float horizontal;
    float vertical;

    private GameObject startingArm;
    private GameObject armSpawnArea;

    public Slider healthSlider;
    public Slider blockSlider;

    public bool vulnerable = false;
    public List<GameObject> currArmCollection = new List<GameObject>();
    public List<bool> youreOpen = new List<bool>();

    public int timesArmsHaveWoundUp; //For Clockwork arm
    public int timesArmsHaveMissed; //For Tantrum arm

    #region Permanent Upgrades
    //////////////// PERMANENT UPGRADES ////////////////

    public int healthBoost;
    public int regenBoost;
    public int armourBoost;
    public int countBoost;
    public int bSpeedBoost;
    public int sSwingSpeedBoost;
    public int mSpeedBoost;
    public int sizeBoost;
    public int moneyBoost;
    //public int expBoost;
    public int maxItemsBoost; //to be deleted
    public int luckBoost;
    public int damageBoost;

    public string characterToBecome;

    //////////////// PERMANENT UPGRADES ////////////////
    #endregion

    #region Bonus Stats

    //Fist Arms
    public int bonusFistArmSpeed;
    public int bonusFistArmDamage;
    public float bonusFistArmWoOExtension = 0.1f;
    public float bonusFistArmCooldownReduction;
    //something else

    //Sword Arms
    public int bonusSwordArmSpeed; //does not need to be in ArmStats as rotation speed is done in this script.
    public int bonusSwordArmDamage;
    public float bonusSwordArmCritChance;
    public float bonusSwordArmCritDamage;

    //Gun Arms
    public int bonusGunArmBulletSpeed;
    public int bonusGunArmDamage;
    public float bonusGunArmFirerate = 1f;
    public int bonusGunArmBulletCount;

    //Arm Types
    public float bonusArmWindUpReduction;
    public int bonusArmDurability;

    //Statuses
    public int bonusArmFreeze;

    public int bonusArmBurn;

    public int bonusArmBleed;

    public int bonusArmShock;

    public int bonusArmVirus;
    public int bonusArmMaxVirus;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayer(); //load permanent upgrades and the like

        healthSlider = GameObject.Find("Health Bar").GetComponent<Slider>();
        blockSlider = GameObject.Find("Block Bar").GetComponent<Slider>();

        maxHealth += healthBoost;
        health = maxHealth;
        block = maxBlock;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        gameObject.tag = "Player";

        armSpawnArea = GameObject.Find("ArmSpawnArea");
        armSpawnArea.transform.parent = transform;

        if(name == "ParryPlayer") //Fencer Triangle
        {
            startingArm = Instantiate(Resources.Load("Weapons/Basic Sword Arm") as GameObject, (Random.insideUnitCircle * (armSpawnArea.GetComponent<CircleCollider2D>().radius * armSpawnArea.GetComponent<Transform>().localScale.x)) + (Vector2)armSpawnArea.transform.position, transform.rotation);
        }
        if(name == "BlockPlayer") //Brawler Square
        {
            startingArm = Instantiate(Resources.Load("Weapons/Basic Fist Arm") as GameObject, (Random.insideUnitCircle * (armSpawnArea.GetComponent<CircleCollider2D>().radius * armSpawnArea.GetComponent<Transform>().localScale.x)) + (Vector2)armSpawnArea.transform.position, transform.rotation);

        }
        if(name == "DodgePlayer") //Deadeye Circle
        {
            startingArm = Instantiate(Resources.Load("Weapons/Basic Gun Arm") as GameObject, (Random.insideUnitCircle * (armSpawnArea.GetComponent<CircleCollider2D>().radius * armSpawnArea.GetComponent<Transform>().localScale.x)) + (Vector2)armSpawnArea.transform.position, transform.rotation);

        }
    }

    // Update is called once per frame
    void Update()
    {
        IsVulnerable();

        horizontal = Input.GetAxisRaw("Horizontal"); //movement
        vertical = Input.GetAxisRaw("Vertical");

        if (health < 1)
        {
            SceneManager.LoadScene("Stage Over Scene"); //Game Over
        }

        if(health > maxHealth)
        {
            health = maxHealth;
        }

        if(CharacterLoader.instance.currentlyFighting == false && block != maxBlock)
        {
            block = maxBlock;
        }
    }

    private void FixedUpdate()
    {
        if(block < maxBlock)
        {
            blockRegenTimer += Time.unscaledDeltaTime;
        }
        if(block > maxBlock)
        {
            block = maxBlock;
        }
        if(blockRegenTimer >= blockRegenInterval)
        {
            blockRegenTimer = 0;
            block += blockRegen;
            SetBlock(block);
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * speed, vertical * speed); //player movement

        //Sword Rotation Speed
        foreach(GameObject sword in CharacterLoader.instance.swordTypeArmList) //rotates swords around player
        {
            sword.transform.RotateAround(gameObject.transform.position, Vector3.back, Time.deltaTime * (sword.GetComponent<ArmStats>().speed + bonusSwordArmSpeed));
        }

        //pickUps = GameObject.FindGameObjectsWithTag("PickUp");
        if (pickUps.Count > 0) //sets up pick ups to come towards the player
        {
            foreach (GameObject pickUp in pickUps)
            {
                if ((Vector3.Distance(pickUp.transform.position, transform.position) <= pickUpRange && pickUp.GetComponent<Pickupable>().inRange == false) || CharacterLoader.instance.currentlyFighting == false)
                {
                    pickUp.GetComponent<Pickupable>().inRange = true;
                }
            }
        }
    }

    private void IsVulnerable() //to make players think instead of just spamming weapons whenever they please, this bool will make them unable to parry but perhaps still block while their arms are attacking
    {
        foreach (GameObject arm in currArmCollection)
        {
            if(arm.GetComponent<ArmStats>().isAttacking == true)
            {
                vulnerable = true;
                return;
            }
        }
        vulnerable = false;
        return;
    }

    public PlayerScript()
    {
        instance = this;
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadUpgrades();

        healthBoost = data.health;
        regenBoost = data.blockRegen;
        armourBoost = data.armour;
        countBoost = data.bulletCount;
        bSpeedBoost = data.bulletSpeed;
        sSwingSpeedBoost = data.swordSwingSpeed;
        mSpeedBoost = data.moveSpeed;
        sizeBoost = data.size;
        moneyBoost = data.moneyMultiplier;
        //expBoost = data.exp;
        maxItemsBoost = data.maxNumberOfItems;
        luckBoost = data.luck;
        damageBoost = data.damage;

        characterToBecome = data.selectedCharacterName;


    }

    #region health and block sliders
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = maxHealth;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    public void SetMaxBlock(int block)
    {
        blockSlider.maxValue = maxBlock;
    }

    public void SetBlock(int block)
    {
        blockSlider.value = block;
    }
    #endregion


    public void SwordRotationPlacement()
    {
        float radius = 5f;

        List<GameObject> swordList = CharacterLoader.instance.swordTypeArmList;

        for (int i = 0; i < swordList.Count; i++) //RADIAL SEPARATION CODE FOR THE LOVE OF GOD DON'T FORGET IT AGAIN
        {
            float circleposition = i / (float)swordList.Count;
            float x = Mathf.Sin(circleposition * Mathf.PI * 2.0f) * radius;
            float z = Mathf.Cos(circleposition * Mathf.PI * 2.0f) * radius;
            swordList[i].transform.position = gameObject.transform.position + new Vector3(x, z, 0);
            swordList[i].transform.parent = gameObject.transform;

            Vector3 armToTargetDiff = (gameObject.transform.position - swordList[i].transform.position).normalized;
            float distance = Mathf.Atan2(armToTargetDiff.y, armToTargetDiff.x) * Mathf.Rad2Deg;

            swordList[i].transform.rotation = Quaternion.Euler(Vector3.forward * distance);
        }
    }

    public void UpgradeUpdater(string upgrade)
    {
        if (upgrade == "BonusFistArmSpeed")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().armType.Contains("Fist"))
                {
                    arm.GetComponent<ArmStats>().bonusFistSpeed = bonusFistArmSpeed;
                }
            }
        }
        else if (upgrade == "BonusFistArmDamage")
        {
            foreach(GameObject arm in currArmCollection)
            {
                if(arm.GetComponent<ArmStats>().armType.Contains("Fist"))
                {
                    arm.GetComponent<ArmStats>().bonusFistDamage = bonusFistArmDamage;
                }
            }
        }
        else if (upgrade == "BonusFistArmWoOExtension")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().armType.Contains("Fist"))
                {
                    //arm.GetComponent<ArmStats>().extension = bonusFistArmWoOExtension;
                }
            }
        }

        else if (upgrade == "BonusSwordArmDamage")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().armType.Contains("Sword"))
                {
                    arm.GetComponent<ArmStats>().bonusSwordDamage = bonusSwordArmDamage;
                }
            }
        }
        else if (upgrade == "BonusSwordArmCritChance")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().armType.Contains("Sword"))
                {
                    arm.GetComponent<ArmStats>().bonusCritChance = bonusSwordArmCritChance;
                }
            }
        }
        else if (upgrade == "BonusSwordArmCritDamage")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().armType.Contains("Sword"))
                {
                    arm.GetComponent<ArmStats>().bonusCritDamage = bonusSwordArmCritDamage;
                }
            }
        }

        else if (upgrade == "BonusGunArmBulletSpeed")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().armType.Contains("Gun"))
                {
                    arm.GetComponent<ArmStats>().bonusGunBulletSpeed = bonusGunArmBulletSpeed;
                }
            }
        }
        else if (upgrade == "BonusGunArmDamage")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().armType.Contains("Gun"))
                {
                    arm.GetComponent<ArmStats>().bonusGunDamage = bonusGunArmDamage;
                }
            }
        }
        else if (upgrade == "BonusGunArmFirerate")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().armType.Contains("Gun"))
                {
                    arm.GetComponent<ArmStats>().bonusFirerate = bonusGunArmFirerate;
                }
            }
        }
        else if (upgrade == "BonusGunArmBulletCount")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().armType.Contains("Gun"))
                {
                    arm.GetComponent<ArmStats>().bonusBulletCount = bonusGunArmBulletCount;
                }
            }
        }

        else if (upgrade == "BonusWindUpReduction")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().isWindUp == true)
                {
                    arm.GetComponent<ArmStats>().bonusWindUpReduction = bonusArmWindUpReduction;
                }
            }
        }
        else if (upgrade == "BonusDurability")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().isDurable == true)
                {
                    arm.GetComponent<ArmStats>().bonusDurability = bonusArmDurability;
                }
            }
        }

        else if (upgrade == "BonusArmFreeze")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().canFreeze == true)
                {
                    arm.GetComponent<ArmStats>().bonusFreeze = bonusArmFreeze;
                }
            }
        }
        else if (upgrade == "BonusArmBurn")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().canBurn)
                {
                    arm.GetComponent<ArmStats>().bonusBurn = bonusArmBurn;
                }
            }
        }
        else if (upgrade == "BonusArmBleed")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().canBleed == true)
                {
                    arm.GetComponent<ArmStats>().bonusBleed = bonusArmBleed;
                }
            }
        }
        else if (upgrade == "BonusArmShock")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().canShock == true)
                {
                    arm.GetComponent<ArmStats>().bonusShock = bonusArmShock;
                }
            }
        }
        else if (upgrade == "BonusArmVirus")
        {
            foreach (GameObject arm in currArmCollection)
            {
                if (arm.GetComponent<ArmStats>().canVirus == true)
                {
                    arm.GetComponent<ArmStats>().bonusVirus = bonusArmVirus;
                }
            }
        }
    }
}
