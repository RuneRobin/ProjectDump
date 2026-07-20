using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volley : MonoBehaviour
{
    private EnemyStats enemyStats;

    private GameObject volleyball;
    private GameObject volleyPrefab;
    public float cooldown = 1;
    public float volleyCount = 1;
    public float volleySpeed = 10;

    public int maxIntercepts = 3;
    public int intercepts = 3;
    public float chanceofFail = 0;

    private Vector2 diff;
    private GameObject player;


    // Start is called before the first frame update
    public void Start()
    {
        volleyPrefab = Resources.Load("Enemy Assets/VolleyBullet") as GameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyStats = gameObject.GetComponent<EnemyStats>();

        //StartCoroutine(AttackCooldown());
    }

    public void FixedUpdate()
    {
        diff = (player.transform.position - transform.position).normalized;

        if(volleyball == null && enemyStats.woOTimer <= 0)
        {
            chanceofFail = 0;
            intercepts = maxIntercepts;
            StartCoroutine(AttackCooldown());
        }
    }


    public IEnumerator AttackCooldown()
    {
        for (var i = 0; i < volleyCount; i++)
        {
            volleyball = Instantiate(volleyPrefab, transform.position, transform.rotation); //laser fireball
            Rigidbody2D volleyRig = volleyball.GetComponent<Rigidbody2D>(); //laser rigidbody

            float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            volleyRig.velocity = diff * volleySpeed;
            volleyball.transform.rotation = Quaternion.Euler(Vector3.forward * distance);

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(cooldown);
    }
}