using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporePlant : MonoBehaviour
{
    private EnemyStats enemyStats;

    public GameObject spore;
    private GameObject sporePrefab;

    public float cooldown = 3;
    public int sporeCount = 6;
    public float sporeSpeed = 3;
    public float coneSize = 30;

    private Vector3 diff;

    private int tiredValue = 10;

    // Start is called before the first frame update
    public void Start()
    {
        sporePrefab = Resources.Load("Enemy Assets/SporeBullet") as GameObject;
        enemyStats = gameObject.GetComponent<EnemyStats>();

        StartCoroutine(AttackCooldown(tiredValue));
    }

    public void FixedUpdate()
    {
        
    }

    public IEnumerator AttackCooldown(int energy) //How long before character attacks
    {
        for (var i = 0; i < sporeCount; i++)
        {
            spore = Instantiate(sporePrefab, transform.position, transform.rotation);

            Rigidbody2D sporeRig = spore.GetComponent<Rigidbody2D>(); //fireballs rigidbody
            //float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Vector3 shootDirection = Quaternion.AngleAxis((Random.Range(0f, 1f) * coneSize) - (coneSize / Mathf.PI), Vector3.forward) * transform.up;

            sporeRig.velocity = shootDirection * sporeSpeed;
            //sporeRig.velocity = 
            //spore.transform.localRotation = Quaternion.Euler(Vector3.forward * Vector2.up);


            yield return new WaitForSeconds(0.05f);
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
