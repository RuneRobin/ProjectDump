using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomBulleteer : MonoBehaviour
{
    public float duration = 5;
    public float cooldown = 5;
    public float bloomCount = 5;

    private GameObject bullet;
    private GameObject bulletPrefab;

    private GameObject parryHitbox;
    private Vector2 startPosition;
    private Vector2 randomPos;
    private Vector2 lastPos = new Vector2(0,0);
    private float timerUntilChange = 3;
    private float t;

    private int tiredValue = 10;
    private EnemyStats enemyStats;

    public void Start()
    {
        parryHitbox = GameObject.FindGameObjectWithTag("Player");
        bulletPrefab = Resources.Load("Enemy Assets/BloomBulletPrefab") as GameObject;
        enemyStats = gameObject.GetComponent<EnemyStats>();

        StartCoroutine(AttackCooldown(tiredValue));

        startPosition = transform.position;
        randomPos = RandomPlace() + (Vector2)parryHitbox.transform.position;
    }

    public void FixedUpdate()
    {
        t += Time.unscaledDeltaTime / timerUntilChange;
        transform.position = Vector3.Lerp(startPosition, randomPos, t);

        if(timerUntilChange > 0)
        {
            timerUntilChange -= Time.unscaledDeltaTime;
        }
        if(timerUntilChange <= 0)
        {
            startPosition = transform.position;
            t = 0;
            randomPos = RandomPlace() + (Vector2)parryHitbox.transform.position;
            timerUntilChange = 3;
        }    
    }

    public Vector2 RandomPlace()
    {
        float x = Random.Range(3f, 5f);
        float y = Random.Range(3f, 5f);

        if ((lastPos.x < 0 && lastPos.y > 0) || (lastPos.x > 0 && lastPos.y < 0)) //A D
        {
            if (Random.Range(0, 2) == 0) // 50/50 it goes full neg or full pos
            {
                x *= -1;
                y *= -1;
            }
        }

        if ((lastPos.x < 0 && lastPos.y < 0) || (lastPos.x > 0 && lastPos.y > 0)) //C B
        {
            if (Random.Range(0, 2) == 0) // 50/50 it negs one and keeps the other pos
            {
                x *= -1;
            }
            else
            {
                y *= -1;
            }
        }

        if(lastPos.x == 0) //first time
        {
            //hi :)
        }

        lastPos = new Vector2(x, y);
        return lastPos;
    }


    public IEnumerator AttackCooldown(int energy) //How long before character attacks
    {
        float radius = 0.5f;

        for (int i = 0; i < bloomCount; i++) //RADIAL SEPARATION CODE FOR THE LOVE OF GOD DON'T FORGET IT AGAIN
        {
            float circleposition = i / bloomCount;
            float x = Mathf.Sin(circleposition * Mathf.PI * 2.0f) * radius;
            float z = Mathf.Cos(circleposition * Mathf.PI * 2.0f) * radius;
            bullet = Instantiate(bulletPrefab, transform.position + new Vector3(x, z, 0), transform.rotation);
            bullet.transform.parent = gameObject.transform;
            bullet.GetComponent<BloomBulletScript>().originPoint = gameObject.transform;
            bullet.GetComponent<BloomBulletScript>().despawnTime = duration;
        }

        yield return new WaitForSeconds(cooldown);

        if(energy > 1)
        {
            StartCoroutine(AttackCooldown(energy--));
        }
        else
        {
            enemyStats.woOTimer = tiredValue / 2;
            yield return new WaitForSeconds(tiredValue/2);
            StartCoroutine(AttackCooldown(tiredValue));
        }
       
        
    }
}