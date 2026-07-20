using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robird : MonoBehaviour
{
    private EnemyStats enemyStats;

    public float cooldown = 1;
    public float featherVolley = 5;
    public float featherSpeed = 10;
    public Vector3 featherDirection;
    private GameObject feather;
    private GameObject featherPrefab;

    private int tiredValue = 5;

    // Start is called before the first frame update
    public void Start()
    {
        featherPrefab = Resources.Load("Enemy Assets/Feather") as GameObject;
        enemyStats = gameObject.GetComponent<EnemyStats>();

        StartCoroutine(AttackCooldown(tiredValue));
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        //float vertical = Input.GetAxis("Vertical"); //input directions
        //float horizontal = Input.GetAxis("Horizontal");
    }

    public IEnumerator AttackCooldown(int energy)
    {
        for (var i = 0; i < featherVolley; i++)
        {
            float angle = i * 360 / featherVolley;
            feather = Instantiate(featherPrefab, transform.position, transform.rotation);

            Rigidbody2D featherRigidbody = feather.GetComponent<Rigidbody2D>();
            featherDirection = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0); //rotates bullet in angle its aimed at

            featherRigidbody.rotation = -angle;
            featherRigidbody.velocity = featherDirection * featherSpeed; //speed of feathers
            yield return new WaitForSeconds(1f / featherVolley);
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
