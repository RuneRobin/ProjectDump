using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMan : MonoBehaviour
{
    private EnemyStats enemyStats;

    public float timer = 15f;
    public int nOfAttacks = 20;

    private GameObject hb;
    private Vector2 diff;

    private GameObject bloomBulletPrefab;
    private GameObject laserPrefab;
    private GameObject dragonfirePrefab;
    private GameObject featherPrefab;

    private int tiredValue = 7;

    // Start is called before the first frame update
    void Start()
    {
        hb = GameObject.Find("ParryPlayer");

        bloomBulletPrefab = Resources.Load("Enemy Assets/BloomBulletPrefab") as GameObject;
        laserPrefab = Resources.Load("Enemy Assets/Laser") as GameObject;
        dragonfirePrefab = Resources.Load("Enemy Assets/Fireball") as GameObject;
        featherPrefab = Resources.Load("Enemy Assets/Feather") as GameObject;
        enemyStats = gameObject.GetComponent<EnemyStats>();

        diff = (hb.transform.position - transform.position).normalized;
        StartCoroutine(AttackRandomizer(tiredValue));
    }

    // Update is called once per frame
    void Update()
    {
        diff = (hb.transform.position - transform.position).normalized;
    }

    public IEnumerator AttackRandomizer(int delay)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < nOfAttacks; i++)
        {
            int random = Random.Range(0, 4);

            if (random == 0)
            {
                StartCoroutine(Attack1());
            }
            if (random == 1)
            {
                StartCoroutine(Attack2());
            }
            if (random == 2)
            {
                StartCoroutine(Attack3());
            }
            if (random == 3)
            {
                StartCoroutine(Attack4());
            }

            yield return new WaitForSeconds(0.5f);
        }

        enemyStats.woOTimer = tiredValue;
        yield return new WaitForSeconds(tiredValue);

        StartCoroutine(AttackRandomizer(0));

    }

    public IEnumerator Attack1() //Bloom Bulleteer
    {
        Debug.Log("Bloom");
        float radius = 0.5f;
        GameObject bullet;
        int randomCount = Random.Range(8, 17);

        for (int i = 0; i < randomCount; i++) //RADIAL SEPARATION CODE FOR THE LOVE OF GOD DON'T FORGET IT AGAIN
        {
            float circleposition = i / (float)randomCount;
            float x = Mathf.Sin(circleposition * Mathf.PI * 2.0f) * radius;
            float z = Mathf.Cos(circleposition * Mathf.PI * 2.0f) * radius;
            bullet = Instantiate(bloomBulletPrefab, transform.position + new Vector3(x, z, 0), transform.rotation);
            bullet.transform.parent = gameObject.transform;
            bullet.GetComponent<BloomBulletScript>().originPoint = gameObject.transform;
            bullet.GetComponent<BloomBulletScript>().despawnTime = 30;
            bullet.GetComponent<BloomBulletScript>().bulletSpeed = 0.1f;
        }

        yield return new WaitForSeconds(1);
    }

    public IEnumerator Attack2() //GatlinGunner
    {
        Debug.Log("Gatlin");
        GameObject bullet;
        int randomCount = Random.Range(10, 16);
        
        for (var i = 0; i < randomCount; i++)
        {
            bullet = Instantiate(laserPrefab, transform.position, transform.rotation); //laser fireball
            Rigidbody2D laserRig = bullet.GetComponent<Rigidbody2D>(); //laser rigidbody

            float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            laserRig.velocity = diff * Random.Range(5f,15f);
            bullet.transform.rotation = Quaternion.Euler(Vector3.forward * distance);

            bullet.transform.Translate(new Vector3(0f, Random.Range(-1f, 1f))); //adds variation by Y axis relative to local space

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1);
    }

    public IEnumerator Attack3() //Dragonfire
    {
        Debug.Log("Dragon");
        GameObject bullet;
        int randomCount = Random.Range(5, 13);

        for (var i = 0; i < randomCount; i++)
        {
            bullet = Instantiate(dragonfirePrefab, transform.position, transform.rotation); //create fireball
            Rigidbody2D fireballRig = bullet.GetComponent<Rigidbody2D>(); //fireballs rigidbody

            float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            Vector3 shootDirection = Quaternion.AngleAxis((i / (float)randomCount * 30) - (30 / Mathf.PI), Vector3.forward) * diff;

            fireballRig.velocity = shootDirection * Random.Range(5f,10f);
            bullet.transform.rotation = Quaternion.Euler(Vector3.forward * distance);
        }

        yield return new WaitForSeconds(1);
    }

    public IEnumerator Attack4() //Robird
    {
        Debug.Log("Robird");
        GameObject bullet;
        int randomCount = Random.Range(5, 11);

        for (var i = 0; i < randomCount; i++)
        {
            float angle = i * 360 / randomCount;
            bullet = Instantiate(featherPrefab, transform.position, transform.rotation);

            Rigidbody2D featherRigidbody = bullet.GetComponent<Rigidbody2D>();
            Vector3 featherDirection = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0); //rotates bullet in angle its aimed at

            featherRigidbody.rotation = -angle;
            featherRigidbody.velocity = featherDirection * 25; //speed of feathers
            yield return new WaitForSeconds(1f / randomCount);
        }

        yield return new WaitForSeconds(1);
    }
}
