using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemScript : MonoBehaviour
{
    private TotemStack totemStack;
    public int totemType;

    public GameObject shield;
    private GameObject shieldPrefab;
    public float shieldOffset;

    private GameObject bullet;
    private GameObject bulletPrefab;

    private float timeOffset;
    private float t0Timer = 15f;
    private float t1Timer = 5f;
    private float t2Timer = 1f;
    private float t3Timer = 2f;
    private float t4Timer = 5f;

    private Vector2 diff;
    private GameObject player;
    public int bonusDamage = 1;
    public int burstCount = 2;
    public int shotCount = 5;
    public float accuracy = 75;

    // Start is called before the first frame update
    void Start()
    {
        timeOffset = Random.Range(0f, 1f);
        totemStack = transform.parent.GetComponent<TotemStack>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (totemType == 2 || totemType == 3)
        {
            bulletPrefab = Resources.Load("Enemy Assets/Laser") as GameObject;
        }

        if (totemType == 4)
        {
            ShieldInstancer();
        }

        
        StartCoroutine("Totem" + totemType,timeOffset);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        diff = (player.transform.position - transform.position).normalized;
    }

    public void ShieldInstancer()
    {
        shieldPrefab = Resources.Load("Enemy Assets/Shield") as GameObject;
        shield = Instantiate(shieldPrefab, transform.position - new Vector3(shieldOffset, 0), transform.rotation);
        shield.transform.parent = gameObject.transform;
    }

    private IEnumerator Totem0(float delay) //Heal
    {
        yield return new WaitForSeconds(t0Timer + delay);
        foreach (GameObject totem in totemStack.totemList)
        {
            EnemyStats ts = totem.GetComponent<EnemyStats>();
            ts.health += Mathf.CeilToInt(gameObject.GetComponent<EnemyStats>().health / 2); //heals all totems by half of it's current health
        }
        StartCoroutine(Totem0(0));
    }
    private IEnumerator Totem1(float delay) //Buff
    {
        yield return new WaitForSeconds(t1Timer + delay);
        foreach (GameObject totem in totemStack.totemList)
        {
            if (totem.GetComponent<TotemScript>().totemType != 1)
            {
                TotemScript ts = totem.GetComponent<TotemScript>();
                ts.bonusDamage += bonusDamage;
            }
        }
        StartCoroutine(Totem1(0));
    }
    private IEnumerator Totem2(float delay) //Shottie?
    {
        bulletPrefab.GetComponent<EnemyBehaviour>().damage = bonusDamage;
        float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Vector3 direction = diff * 20f; //so that when each bullet is being fired it doesn't update to wherever the mouse is but instead they all fire towards the first shot's location

        for (int i = 0; i < shotCount; i++)
        {
            float accOffset = Random.Range(-accuracy, accuracy);

            bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            Vector3 shootDirection = Quaternion.AngleAxis((Random.Range(0f, 1f) * accuracy) - (accuracy / Mathf.PI), Vector3.forward) * direction;
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection;

            bullet.transform.rotation = Quaternion.Euler(Vector3.forward * distance + new Vector3(0, 0, accOffset)); //obsolete?

            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(t2Timer + delay);
        StartCoroutine(Totem2(0));
    }
    private IEnumerator Totem3(float delay) //X shot burst
    {
        bulletPrefab.GetComponent<EnemyBehaviour>().damage = bonusDamage;
        for (var i = 0; i < burstCount; i++)
        {
            bullet = Instantiate(bulletPrefab, transform.position, transform.rotation); //laser fireball
            Rigidbody2D bulletRig = bullet.GetComponent<Rigidbody2D>(); //laser rigidbody

            float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            bulletRig.velocity = diff * 10;
            bullet.transform.rotation = Quaternion.Euler(Vector3.forward * distance);

            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(t3Timer + delay);
        StartCoroutine(Totem3(0));
    }
    private IEnumerator Totem4(float delay) //Shields
    {
        yield return new WaitForSeconds(t4Timer + delay);
        foreach(GameObject totem in totemStack.totemList)
        {
            TotemScript ts = totem.GetComponent<TotemScript>();
            if(ts.shield == null)
            {
                ts.ShieldInstancer();
            }
            else
            {
                EnemyStats sc = ts.shield.GetComponent<EnemyStats>();
                sc.maxHealth += 3;
                sc.health = sc.maxHealth;
                //explodes if it reaches a certain point and damages player?
            }
        }
        StartCoroutine(Totem4(0));
    }


    private void OnDestroy()
    {
        totemStack.totemList.Remove(gameObject);
    }
}
