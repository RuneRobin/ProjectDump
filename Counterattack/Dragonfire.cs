using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragonfire : MonoBehaviour
{
    private EnemyStats enemyStats;

    private GameObject fireball;
    private GameObject fireballPrefab;
    public float cooldown = 3;
    public int fireCount = 3;
    public float fireSpeed = 4;
    public float coneSize = 30;

    private Vector2 diff;
    private GameObject player;

    private int tiredValue = 10;

    public void Start()
    {
        fireballPrefab = Resources.Load("Enemy Assets/Fireball") as GameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyStats = gameObject.GetComponent<EnemyStats>();

        StartCoroutine(AttackCooldown(tiredValue));
    }

    public void FixedUpdate()
    {
        diff = (player.transform.position - transform.position).normalized;
    }


    public IEnumerator AttackCooldown(int energy) //How long before character attacks
    {
        for (var i = 0; i < fireCount; i++)
        {
            fireball = Instantiate(fireballPrefab, transform.position, transform.rotation); //create fireball
            Rigidbody2D fireballRig = fireball.GetComponent<Rigidbody2D>(); //fireballs rigidbody

            float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            Vector3 shootDirection = Quaternion.AngleAxis((i / (float)fireCount * coneSize) - (coneSize / Mathf.PI), Vector3.forward) * diff;

            fireballRig.velocity = shootDirection * fireSpeed;
            fireball.transform.rotation = Quaternion.Euler(Vector3.forward * distance);
        }

        yield return new WaitForSeconds(cooldown); //cooldown between each attack

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
