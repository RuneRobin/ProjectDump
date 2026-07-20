using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBullet : MonoBehaviour
{
    public float despawnTime = 10;
    public float bulletDamage;
    public bool triggered = false;

    private GameObject explosion;
    private GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        explosionPrefab = Resources.Load("Sprites/Bullets/MineExplosion") as GameObject;
        StartCoroutine(Despawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Despawner()
    {
        yield return new WaitForSeconds(despawnTime);
        explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && triggered == false)
        {
            triggered = true;
            collision.gameObject.GetComponent<EnemyBehaviour>().health -= bulletDamage;
            explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }

}
