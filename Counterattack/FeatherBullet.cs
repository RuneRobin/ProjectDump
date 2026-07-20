using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherBullet : MonoBehaviour
{
    public float despawnTime = 10;
    public float bulletDamage;
    public float bulletSpeed = 200;
    public float bulletRotationSpeed = 200;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Despawner());
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        BulletFlight();
    }

    //public IEnumerator Despawner()
    //{
        //yield return new WaitForSeconds(despawnTime);
       // Destroy(gameObject);
    //}

    private void BulletFlight()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed * Time.deltaTime;
        Vector2 direction = ((Vector2)GameObject.Find("ParryPlayer").transform.position - GetComponent<Rigidbody2D>().position).normalized;
        float rotationValue = Vector3.Cross(direction, transform.up).z;
        GetComponent<Rigidbody2D>().angularVelocity = -rotationValue * bulletRotationSpeed;
        GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed * Time.deltaTime;
    }
}
