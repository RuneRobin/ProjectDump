using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherBullet : MonoBehaviour
{
    public float despawnTime = 10;
    public float bulletDamage;
    public float bulletSpeed = 200;
    public float bulletRotationSpeed = 200;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawner());
        FindClosestEnemy();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        BulletFlight();
        if(target == null)
        {
            FindClosestEnemy();
        }
    }

    public IEnumerator Despawner()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().health -= bulletDamage;
            Destroy(gameObject);

        }
    }

    private void BulletFlight()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed * Time.deltaTime;
        if(target != null)
        {
            Vector2 direction = (Vector2)target.position - GetComponent<Rigidbody2D>().position;
            direction.Normalize();
            float rotationValue = Vector3.Cross(direction, transform.up).z;
            GetComponent<Rigidbody2D>().angularVelocity = -rotationValue * bulletRotationSpeed;
            GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed * Time.deltaTime;
        }
    }


    private void FindClosestEnemy()
    {
        GameObject[] allies;
        allies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject ally in allies)
        {
            Vector3 diff = ally.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = ally;
                distance = curDistance;
                target = closest.transform;
            }
        }
    }



}
