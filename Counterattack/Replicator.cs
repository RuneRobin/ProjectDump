using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replicator : MonoBehaviour
{
    private GameObject replication;
    private GameObject replicatorPrefab;
    private GameObject parryHb;
    private Rigidbody2D rb;
    private EnemyStats eStats;

    public float speed = 20;
    public float chargeTime = 3;
    public float replicateTime = 3;
    public int previousDamage = 0; //health missing from it's progenitor

    private Vector2 diff;

    // Start is called before the first frame update
    void Start()
    {
        eStats = GetComponent<EnemyStats>();
        parryHb = GameObject.FindGameObjectWithTag("Player");
        replicatorPrefab = Resources.Load("Enemy Encounters/Replicator") as GameObject;
        rb = GetComponent<Rigidbody2D>();

        eStats.health -= previousDamage; //health missing from it's progenitor

        StartCoroutine(Charge());
        //StartCoroutine(Replicate());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(eStats.physicallyParried == true)
        {
            eStats.physicallyParried = false;
            StartCoroutine(Pushback());
        }
        diff = (parryHb.transform.position - transform.position).normalized;
    }

    public IEnumerator Charge()
    {
        //play a shaking animation here?
        yield return new WaitForSeconds(chargeTime);

        rb.velocity = diff * speed;
        //rb.AddForce(diff * speed, ForceMode2D.Force);
    }

    public IEnumerator Replicate()
    {
        yield return new WaitForSeconds(replicateTime);

        replication = Instantiate(replicatorPrefab, transform.position, transform.rotation);
        replication.GetComponent<Replicator>().previousDamage = eStats.maxHealth - eStats.health;
    }

    public IEnumerator Pushback()
    {
        rb.velocity = Vector2.zero;
        rb.velocity = diff * (speed/2) * -1;

        yield return new WaitForSeconds(1);
    }

}
