using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    //public GameObject healthBar;
    //public Slider healthSlider;
    public Gradient gradient;
    private GradientColorKey[] colorKey;
    public int stamina;
    public int damage;
    public float vulnerability = 1;
    public int speed = 1;

    public bool attacking = false;
    public bool openToAttack = false;
    public float woOTimer;
    public int woOExtensionStacks;
    public int woOExtensionResistance = 1;
    public bool physicallyParried = false;

    public int gold;
    public float scrapDropChance = 15f;
    public float blueprintDropChance = 1f;
    public GameObject scrap;

    public string enemyColour;
    public string enemyType;
    public string rewardPool;

    public bool isBurning = false;
    public float burnTimer;
    public float extinguishTimer;
    public int burnDamage;

    public bool isBleeding = false;
    public float bleedTimer;
    public int bleedDamage;

    public bool isShocked = false;
    public int maxShockStacks = 50;
    public int shockStacks;

    public bool isFrozen = false;
    public float freezeTimer;
    public int maxFreezeStacks = 20;
    public int freezeStacks;

    public bool isHacked = false;
    public int bonusMaxVirusStacks;
    public int virusStacks;


    private void Awake()
    {
        CharacterLoader.instance.activeEnemies.Add(gameObject);

        #region gradient
        gradient = new Gradient();
        colorKey = new GradientColorKey[2];
        colorKey[0] = new GradientColorKey(Color.white, 0.0f);
        colorKey[1] = new GradientColorKey(GetComponent<SpriteRenderer>().color, 1.0f);
        
        var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(1.0f, 1.0f);
        alphas[1] = new GradientAlphaKey(1.0f, 1.0f);

        gradient.SetKeys(colorKey,alphas);
        #endregion
    }
    // Start is called before the first frame update
    void Start()
    {
        bonusMaxVirusStacks = PlayerScript.instance.bonusArmMaxVirus;

        #region Difficulty Level
        int difficultyLevel = Master.instance.difficulty;

        maxHealth = maxHealth * Mathf.CeilToInt(0.5f * difficultyLevel);
        damage = damage + difficultyLevel * 2;
        speed = speed + difficultyLevel;
        #endregion

        health = maxHealth;

        if(CharacterLoader.instance.currAreaAndNode.Contains("Area 1"))
        {
            enemyType = "Square";
        }
        if (CharacterLoader.instance.currAreaAndNode.Contains("Area 2"))
        {
            enemyType = "Circle";
        }
        if (CharacterLoader.instance.currAreaAndNode.Contains("Area 3"))
        {
            enemyType = "Triangle";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }

        UpdateColor();

        //if(!name.Contains("Minion") && tag != "Shield" && healthSlider.value != health)
        {
            //healthSlider.value = health;
        }

        #region WoO
        if (tag == "Shield")
        {
            openToAttack = true;
        }
        else
        {
            if (woOTimer > 0)
            {
                woOTimer -= Time.unscaledDeltaTime;
                openToAttack = true;
            }

            if (woOTimer <= 0 && openToAttack == true)
            {
                openToAttack = false;
                woOTimer = 0;
                woOExtensionStacks = 0;
                woOExtensionResistance = 1;
            }
        }

        if(woOExtensionStacks >= woOExtensionResistance) //for fist type arms to extend duration on hit
        {
            woOExtensionStacks -= woOExtensionResistance;
            woOExtensionResistance++;
            woOTimer += PlayerScript.instance.bonusFistArmWoOExtension;
        }
        #endregion

        if (tag != "Shield")
        {
            #region Bleed Damage
            if (bleedDamage > 0 && bleedTimer >= 1)
            {
                health -= bleedDamage;
                bleedDamage -= 1;
                bleedTimer = 0;
                //add fancy effect to show that bleeding
            }

            if (bleedDamage > 0 && isBleeding == false)
            {
                isBleeding = true;
            }
            if (bleedDamage == 0 && isBleeding == true)
            {
                isBleeding = false;
                bleedTimer = 0;
            }

            if (isBleeding == true)
            {
                bleedTimer += Time.unscaledDeltaTime;
            }
            #endregion

            #region Burn Damage

            if (burnDamage > 0 && isBurning == false)
            {
                isBurning = true;
            }

            if (burnTimer >= 3)
            {
                health -= burnDamage;
                burnTimer = 0;
                //add fancy effect to show that burning
            }

            if (extinguishTimer >= 10)
            {
                isBurning = false;
                burnTimer = 0;
                extinguishTimer = 0;
                burnDamage = 0;
            }

            if (isBurning == true)
            {
                burnTimer += Time.unscaledDeltaTime;
                extinguishTimer += Time.unscaledDeltaTime;
            }
            #endregion

            #region Shock Status
            if (shockStacks > maxShockStacks)
            {
                shockStacks = maxShockStacks;
            }

            if (shockStacks > 0 && isShocked == false)
            {
                isShocked = true;
            }
            if (shockStacks == 0 && isShocked == true)
            {
                isShocked = false;
            }

            if (isShocked == true)
            {
                //when this attacks, take damage equal to jolt stacks
                if (attacking == true)
                {
                    health -= shockStacks;
                    //fancy numbers to show damage
                    shockStacks = 0;
                    isShocked = false;
                    attacking = false; //remove this when you figure out how to make enemies attack <----------------------------------------------------------------------------------------------
                }

            }
            #endregion

            #region Freeze Status
            if (freezeStacks > maxFreezeStacks)
            {
                freezeStacks = maxFreezeStacks;
                isFrozen = true;
            }

            if (isFrozen == true)
            {
                freezeTimer += Time.unscaledDeltaTime;
                //WoO
            }

            if (freezeTimer >= 3)
            {
                freezeTimer = 0;
                isFrozen = false;
                freezeStacks = 0;
                maxFreezeStacks += Mathf.CeilToInt(maxFreezeStacks * 0.5f);
            }
            #endregion

            #region Virus Status
            if (virusStacks > 0 && isHacked == false)
            {
                isHacked = true;
            }
            if (virusStacks == 0 && isHacked == true)
            {
                isHacked = false;
            }

            if (virusStacks > 10 + bonusMaxVirusStacks)
            {
                virusStacks = 10 + bonusMaxVirusStacks;
            }
            #endregion
        }

        if (health <= 0 + virusStacks) //Virus Status
        {
            if (Random.Range(0,101) <= scrapDropChance)
            {//Square,Circle,Triangle
                Instantiate(Resources.Load("Sprites/Items/" + enemyType + "Scrap") as GameObject, transform.position, transform.rotation);
            }
            if(Random.Range(0,101) < blueprintDropChance)
            {
                foreach (string bp in UpgradeLoader.instance.unlockableBlueprints.Keys) //checks to see if there are any blueprints left to unlock
                {
                    if (UpgradeLoader.instance.unlockableBlueprints[bp].isUnlocked == false)
                    {
                        Instantiate(Resources.Load("Sprites/Items/Blueprint") as GameObject, transform.position, transform.rotation);
                        break; //Stops after one
                    }
                }
            }
            Destroy(gameObject);
        }
    }

    private void UpdateColor()
    {
        float nHealth = (float)health / maxHealth;

        Color color = gradient.Evaluate(nHealth);

        GetComponent<SpriteRenderer>().color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //for any enemy shields this is attached to
        if(collision.tag == "Arm" && !collision.GetComponent<ArmStats>().armType.Contains("Gun") && tag == "Shield") 
        {
            ArmStats stats = collision.gameObject.GetComponent<ArmStats>();

            health -= stats.damage;
            
            if (stats.armType.Contains("Fist"))
            {
                stats.isAttacking = false;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                collision.transform.position = stats.startingPos;
                collision.transform.rotation = stats.startingRot;
            }
        }

        if(collision.tag == "Player")
        {
            if(collision.name.Contains("Parry"))
            {
                if (name == "Replicator") //For anything that requires the player to parry the enemies physical body
                {
                    if (Input.GetMouseButton(1) && collision.GetComponent<ParryHitbox>().isNowBlocking == false)
                    {
                        physicallyParried = true;
                        collision.GetComponent<ParryHitbox>().invulTimer = 0.1f;
                        collision.GetComponent<ParryHitbox>().invulWindow = true;//brief invulnerability after a successful parry
                    }

                    if (!Input.GetMouseButton(1) || collision.GetComponent<ParryHitbox>().isNowBlocking == false)
                    {
                        collision.GetComponent<ParryHitbox>().TakeDamage(damage);
                    }

                    if (collision.GetComponent<ParryHitbox>().isNowBlocking == true)
                    {
                        collision.GetComponent<ParryHitbox>().MinusBlock(1);
                    }
                }
            }

            if(collision.name.Contains("Block"))
            {

            }

            if(collision.name.Contains("Dodge"))
            {

            }
        }
    }

    private void OnDestroy()
    {
        CharacterLoader.instance.activeEnemies.Remove(gameObject);
        Master.instance.enemiesDefeated++;
        Master.instance.currentEnemyCount--;
        //Destroy(healthBar);
    }
}
