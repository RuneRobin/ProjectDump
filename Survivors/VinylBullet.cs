using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylBullet : MonoBehaviour
{
    public float despawnTime = 10;
    public float bulletDamage;
    public bool pierces = false;
    private List<GameObject> alreadyHit = new List<GameObject>();
    public Transform originPoint;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawner());
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        transform.RotateAround(originPoint.position, Vector3.forward * Time.deltaTime, 100 * Time.deltaTime);

        transform.position += (transform.position - originPoint.position).normalized * 0.01f;
    }

    public IEnumerator Despawner()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !alreadyHit.Contains(collision.gameObject))
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().health -= bulletDamage;
            alreadyHit.Add(collision.gameObject);
            if (pierces == false)
            {
                Destroy(gameObject);
            }

        }
    }

}
