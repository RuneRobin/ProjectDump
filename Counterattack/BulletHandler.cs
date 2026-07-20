using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public float despawnTime = 5;
    public int bulletDamage;
    public bool pierces = false;
    private List<GameObject> alreadyHit = new List<GameObject>();

    public int bleedDamage;
    public int burnDamage;
    public int virusInflict;

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
        if (collision.gameObject.tag == "Enemy" && !alreadyHit.Contains(collision.gameObject) && enemy.openToAttack == true)
        {
            enemy.health -= bulletDamage;

            if(bleedDamage > 0)
            {
                enemy.health -= bleedDamage;
            }

            if(burnDamage > 0)
            {
                enemy.extinguishTimer = 0;
                enemy.burnDamage += burnDamage;
            }

            if(virusInflict > 0)
            {
                enemy.virusStacks += virusInflict;
            }

            alreadyHit.Add(collision.gameObject);
            if(pierces == false)
            {
                Destroy(gameObject);
            }
            
        }
    }

}
