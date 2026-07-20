using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkBullet : MonoBehaviour
{
    public float despawnTime = 10;
    public float bulletDamage;
    private List<GameObject> alreadyHit = new List<GameObject>();

    public float cooldown = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Despawner()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        EnemyBehaviour eB = collision.GetComponent<EnemyBehaviour>();
        if (collision.gameObject.tag == "Enemy")
        {
            //eB.boiling = true;
            //eB.boilingDamage = bulletDamage;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        EnemyBehaviour eB = collision.GetComponent<EnemyBehaviour>();
        if (collision.gameObject.tag == "Enemy")
        {
            //eB.boiling = false;
        }
    }

}