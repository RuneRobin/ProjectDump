using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlinGunner : MonoBehaviour
{
    private EnemyStats enemyStats;

    private GameObject laser;
    private GameObject laserPrefab;
    public float cooldown = 1;
    public float laserCount = 1;
    public float laserSpeed = 5;

    private Vector2 diff;
    private GameObject player;

    private int tiredValue = 50;

    // Start is called before the first frame update
    public void Start()
    {
        laserPrefab = Resources.Load("Enemy Assets/Laser") as GameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyStats = gameObject.GetComponent<EnemyStats>();

        StartCoroutine(AttackCooldown(tiredValue));
    }

    public void FixedUpdate()
    {
        diff = (player.transform.position - transform.position).normalized;
    }


    public IEnumerator AttackCooldown(int energy)
    {
        for (var i = 0; i < laserCount; i++)
        {
            laser = Instantiate(laserPrefab, transform.position, transform.rotation); //laser fireball
            Rigidbody2D laserRig = laser.GetComponent<Rigidbody2D>(); //laser rigidbody

            float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            laserRig.velocity = diff * laserSpeed;
            laser.transform.rotation = Quaternion.Euler(Vector3.forward * distance);

            laser.transform.Translate(new Vector3(0f, Random.Range(-0.5f, 0.5f))); //adds variation by Y axis relative to local space

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(cooldown);

        if (energy > 1)
        {
            StartCoroutine(AttackCooldown(energy--));
        }
        else
        {
            enemyStats.woOTimer = tiredValue / 2;
            yield return new WaitForSeconds(tiredValue / 2);
            StartCoroutine(AttackCooldown(tiredValue));
        }

    }
}
