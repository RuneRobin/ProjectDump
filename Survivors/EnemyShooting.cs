using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private EnemyBehaviour enemyScript;
    private GameObject bullet;
    private GameObject bulletPrefab;

    private Vector3 diff;

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = gameObject.GetComponent<EnemyBehaviour>();
        bulletPrefab = Resources.Load("Sprites/Bullets/EnemyBullet") as GameObject;
        StartCoroutine(AttackCooldown());
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        diff = enemyScript.direction;
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitUntil(() => enemyScript.direction != null); //waits until there's an enemy around

        bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);  //chat hold on, chat chat, chat, is he goated?
        Rigidbody2D bulletRig = bullet.GetComponent<Rigidbody2D>(); 

        float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        bulletRig.velocity = diff * 3;
        bullet.transform.rotation = Quaternion.Euler(Vector3.forward * distance);

        yield return new WaitForSeconds(3);

        StartCoroutine(AttackCooldown());

    }
}
