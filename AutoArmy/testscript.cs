using System.Collections;
using UnityEngine;

public class testscript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bullet;
    public int bulletCount = 1;
    public float accuracy = 10;
    public int speed = 10;

    private Vector3 diff;
    private Vector2 mouse;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ArcherAttack());
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        diff = (mouse - (Vector2)transform.position).normalized;
    }

    public IEnumerator ArcherAttack()
    {
        yield return new WaitForSeconds(2f);
        
        for (int i = 0; i < bulletCount; i++)
        {
            float accOffset = Random.Range(-accuracy, accuracy);
            float distance = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Vector3 generalDirection = diff * speed;

            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(Vector3.forward * distance + new Vector3(0, 0, accOffset)));
            Rigidbody2D bulletRig = bullet.GetComponent<Rigidbody2D>();

            Vector2 shoot = Quaternion.Euler(0, 0, accOffset) * generalDirection;
            bulletRig.linearVelocity = shoot;

            //float offset = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 3;
            //bullet.transform.Translate(new Vector3(0,Random.Range(-offset,offset)));
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(ArcherAttack());
    }
}
