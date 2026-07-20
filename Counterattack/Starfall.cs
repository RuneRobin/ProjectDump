using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfall : MonoBehaviour
{
    private EnemyStats enemyStats;

    private GameObject star;
    private GameObject starPrefab;
    public float cooldown = 1;
    public int starCount = 1;
    //public float starSpeed = 2;
    public Vector3 sky;

    private int tiredValue = 50;

    public void Start()
    {
        starPrefab = Resources.Load("Enemy Assets/SporeBullet") as GameObject;
        enemyStats = gameObject.GetComponent<EnemyStats>();

        StartCoroutine(AttackCooldown(tiredValue));
    }

    public IEnumerator AttackCooldown(int energy)
    {
        for (var i = 0; i < starCount; i++)
        {
            GameObject hitbox = GameObject.FindGameObjectWithTag("Player");
            Vector3 roof = new Vector3(hitbox.transform.position.x + Random.Range(-10f,10f), Camera.main.ViewportToWorldPoint(new Vector3(1, 1.1f, 0)).y, 0);

            star = Instantiate(starPrefab, roof, transform.rotation); //create bullet
            yield return new WaitForSeconds(0.05f);
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
