using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherScript : MonoBehaviour
{
    public bool enemyInRange = false;
    public bool isAttacking = false;

    private GameObject bullet;
    public int bulletCount = 1;
    public float accuracy = 10;

    private Vector3 diff;
    private UnitScript us;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        us = gameObject.GetComponent<UnitScript>();
        us.job = 1;
        us.personalSpace = 5f;
        us.damage = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(us.FindClosestEnemy() != null)
        {
            diff = (Vector2)(us.FindClosestEnemy().transform.position - transform.position).normalized;
        }
    }

    public IEnumerator ArcherAttack()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < bulletCount; i++)
        {
            float accOffset = Random.Range(-accuracy, accuracy);
            float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Vector3 generalDirection = diff * 5f;

            bullet = Instantiate(MasterController.instance.projPrefab, transform.position, Quaternion.Euler(Vector3.forward * distance + new Vector3(0, 0, accOffset)));
            Rigidbody2D bulletRig = bullet.GetComponent<Rigidbody2D>();

            Vector2 shoot = Quaternion.Euler(0, 0, accOffset) * generalDirection;
            bulletRig.linearVelocity = shoot;

            //float offset = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 3;
            //bullet.transform.Translate(new Vector3(0,Random.Range(-offset,offset)));
            BulletScript bs = bullet.GetComponent<BulletScript>();
            bs.enemyFaction = us.enemyArmy;
            bs.source = gameObject;

            SceneConst.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(SceneConst.instance.SFXCollection[Random.Range(16,18)]);

            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.8f);
        isAttacking = false;
        gameObject.GetComponent<UnitScript>().attacking = false;
    }
}
