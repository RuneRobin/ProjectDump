using System.Collections;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public float speed = 1;
    public float damage = 1;
    public float armour = 0;
    public string army;
    public string enemyArmy;
    public string faction;
    public int job; //0 = infantry, 1 = archer, 2 = priest
    public int positionInArmy;

    public bool attacking = false;

    private Vector3 direction;
    public float distance;
    private Transform target;
    public float personalSpace = 1;
    private Rigidbody2D rb;

    public GameObject holster;
    public bool isFlipped = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        army = gameObject.tag;
        health = maxHealth;

        if(army == "ArmyOne")
        {
            enemyArmy = "ArmyTwo";
        }
        else
        {
            enemyArmy = "ArmyOne";
        }

        holster = new GameObject("Holster");
        holster.transform.parent = gameObject.transform;
        holster.transform.position = gameObject.transform.position + new Vector3(-gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 4, -gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 4, 0);
        holster.transform.rotation = Quaternion.Euler(0, 0, -15f);
    }

    // Update is called once per frame
    void Update()
    {        
        if (FindClosestEnemy() != null && MasterController.instance.formationSetupTimer <= 0)
        {
            direction = (FindClosestEnemy().transform.position - transform.position).normalized;
            distance = Vector2.Distance(transform.position, target.position);
            //rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
            if (distance > personalSpace)
            {
                //rb.velocity = Vector2.MoveTowards(rb.velocity, new Vector2(direction.x * speed, direction.y * speed), 5 * Time.fixedDeltaTime);
                rb.MovePosition((Vector2)transform.position + (new Vector2(direction.x * speed, direction.y * speed)) * Time.deltaTime);
            }
            else
            {
                //rb.velocity = new Vector2(0,0);
                rb.MovePosition(transform.position);

                if(attacking == false) 
                {
                    if (job == 0) //Infantry class Attack
                    {
                        attacking = true;
                        if (gameObject.GetComponent<InfantryScript>())
                        {
                            StartCoroutine(gameObject.GetComponent<InfantryScript>().MeleeAttack());
                        }
                    }
                    else if(job == 1) //Archer class Attack
                    {
                        attacking = true;
                        if (gameObject.GetComponent<ArcherScript>())
                        {
                            StartCoroutine(gameObject.GetComponent<ArcherScript>().ArcherAttack());
                        }
                    }
                    else if (job == 2) //Priest class Attack
                    {
                        attacking = true;
                        if (gameObject.GetComponent<PriestScript>())
                        {
                            StartCoroutine(gameObject.GetComponent<PriestScript>().PriestAttack());
                        }
                    }
                }
            }
        }

        if (FindClosestEnemy() != null)
        {
            if (FindClosestEnemy().transform.position.x < transform.position.x && gameObject.transform.rotation != Quaternion.Euler(0, 180, 0) && isFlipped == false)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
                isFlipped = true;
            }
            else if (FindClosestEnemy().transform.position.x > transform.position.x && gameObject.transform.rotation != Quaternion.Euler(0, 0, 0) && isFlipped == true)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                isFlipped = false;
            }
        }

        if(health > maxHealth) //in case of overhealing, revisit here for overheal mechanics lol
        {
            health = maxHealth;
            MasterController.instance.ArmyHealthUpdater(army);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] allies;
        allies = GameObject.FindGameObjectsWithTag(enemyArmy);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject ally in allies)
        {
            Vector3 diff = ally.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = ally;
                distance = curDistance;
                target = closest.transform;
            }
        }
        return closest;
    }


    public void OnHitting(UnitScript target)
    {
        float sB = 1; //Stat Boost (Primarily from Highlanders and The Circus)
        float bD = 0; //Bonus Damage (Primarily from Elementals)

        if (faction == "Undead")
        {
            health += gameObject.GetComponent<UndeadSoldier>().lifesteal;
        }
        else if(faction == "Highlander")
        {
            sB = gameObject.GetComponent<HighlanderSoldier>().statBoost;
        }
        else if (faction == "Elemental")
        {
            int r = Random.Range(0, 4);
            
            if (r == 0)
            {
                bD += gameObject.GetComponent<ElementalSoldier>().fireBoost;
            }
            else if (r == 1)
            {
                bD += gameObject.GetComponent<ElementalSoldier>().waterBoost;
            }
            else if (r == 2)
            {
                bD += gameObject.GetComponent<ElementalSoldier>().earthBoost;
            }
            else if (r == 3)
            {
                bD += gameObject.GetComponent<ElementalSoldier>().airBoost;
            }
        }
        else if (faction == "Circus")
        {
            bD += gameObject.GetComponent<CircusClown>().comeback;
            gameObject.GetComponent<CircusClown>().comeback = 0;
        }
        else if (faction == "Robot")
        {
            //nothing as of yet
        }

        target.health -= ((damage + bD) * sB) - target.armour; //((damage + bonus damage)) * stat boost) - armour
        if(target.health <= 0)
        {
            target.health = 0; //to avoid early victories due to overkill
            OnKilling();
        }
        MasterController.instance.ArmyHealthUpdater(target.army);
        target.OnBeingHit(this);
    }

    public void OnBeingHit(UnitScript attacker) //attacker used for counterattack related interactions
    {
        if(faction == "Circus")
        {
            gameObject.GetComponent<CircusClown>().comeback += 0.25f;
        }
    }

    public void OnKilling()
    {
        if(faction == "Elemental")
        {
            gameObject.GetComponent<ElementalSoldier>().powerCharge++;
        }
    }

    public void OnKilled()
    {
        //Highlander Specific Boost
        if (faction == "Highlander")
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(army))
            {
                g.GetComponent<HighlanderSoldier>().statBoost += 0.1f;
            }
        }
    }

    public void OnHealed()
    {

    }



    public void OnDestroy()
    {
        //instant death
        if(health > 0)
        {
            if (army == "ArmyOne")
            {
                MasterController.instance.armyOneCurrentHPValue -= health;
                MasterController.instance.armyOne.Remove(gameObject);
            }
            else
            {
                MasterController.instance.armyTwoCurrentHPValue -= health;
                MasterController.instance.armyTwo.Remove(gameObject);
            }
        }

        if(health < 0)
        {
            health = 0;
            MasterController.instance.ArmyHealthUpdater(faction);
        }

        if (SceneConst.instance)
        {
            SceneConst.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(SceneConst.instance.SFXCollection[Random.Range(7, 9)]);
        }

        OnKilled();
    }
}
