using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBoss : MonoBehaviour
{
    private Transform player;
    private EnemyStats eStats;

    [SerializeField] private LayerMask mask;
    private bool los = false;

    private float attackInterval;
    public GameObject bulletprefab;
    private GameObject bullet;

    private Vector2 diff;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerScript.instance.transform;
        eStats = gameObject.GetComponent<EnemyStats>();

        attackInterval = Random.Range(1.5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.back, Time.deltaTime * 100);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        diff = (player.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        RaycastHit2D[] ray = Physics2D.LinecastAll(transform.position, player.position - transform.position,mask);

        if (attackInterval > 0)
        {
            attackInterval -= Time.deltaTime;
        }

        foreach (RaycastHit2D hit in ray)
        {
            if (hit.collider.CompareTag("Enemy") && hit.collider.transform != transform)
            {
                Debug.Log(hit.collider.name);
                los = false;
                break;
            }
            else
            {
                los = true;
            }
        }


        if (los == true && attackInterval <= 0)
        {
            attackInterval = 5;
            StartCoroutine(Shoot());
        }

        if(los == true) //testing purposes, delete later
        {
            Debug.DrawRay(transform.position, player.position - transform.position, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, player.position - transform.position, Color.red);
            //do nothing i think?
        }
    }

    public IEnumerator Shoot()
    {
        for (var i = 0; i < 3; i++)
        {
            bullet = Instantiate(bulletprefab, transform.position, transform.rotation); //laser fireball
            Rigidbody2D bulletRig = bullet.GetComponent<Rigidbody2D>(); //laser rigidbody

            float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            bulletRig.velocity = diff * eStats.speed;
            bullet.GetComponent<EnemyBehaviour>().damage = eStats.damage;
            bullet.transform.rotation = Quaternion.Euler(Vector3.forward * distance);

            yield return new WaitForSeconds(1f);
        }
    }
}
