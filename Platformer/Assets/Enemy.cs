using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject player;

    float step;
    public float moveSpeed = 3;
    public float health = 10.0f;
    public Camera theCamera;

    float burning;

    public bool onFire = false;

    float vicinity;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        vicinity = Vector3.Distance(player.transform.position, gameObject.transform.position);
        
        if(onFire == true)
        {
            burning += Time.deltaTime * 1.0f;
            health -= Time.deltaTime * 1.0f;
            if (burning >= 3.0f)
            {
                onFire = false;
                burning = 0;
            }
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        step = moveSpeed * Time.deltaTime;

        if (vicinity <= 3.0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), step);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InvokeRepeating("damaging", 0.0f, 1.0f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InvokeRepeating("damaging", 0.0f, 1.0f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CancelInvoke("damaging");
    }

    void damaging()
    {
        theCamera.GetComponent<PlatformChanger>().health -= 0.1f;
    }


}