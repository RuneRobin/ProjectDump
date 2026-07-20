using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public float despawnTime = 10;
    public float bulletDamage;
    public bool pierces = false;
    private List<GameObject> alreadyHit = new List<GameObject>();

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
        if(collision.gameObject.tag == "Enemy" && !alreadyHit.Contains(collision.gameObject))
        {
            Debug.Log("hit enemy");
            collision.gameObject.GetComponent<EnemyBehaviour>().health -= bulletDamage;
            alreadyHit.Add(collision.gameObject);
            if(pierces == false)
            {
                Destroy(gameObject);
            }
            
        }
    }

}
