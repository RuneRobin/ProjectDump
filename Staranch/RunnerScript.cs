using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerScript : MonoBehaviour
{ 
    private Rigidbody2D rb;

    private Vector2 direction;

    public Vector2 objVel;

    public float speed = 5;
    private float trueSpeed;
    public float slow;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        direction = Random.insideUnitCircle.normalized;
        rb.velocity = direction * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        trueSpeed = speed - slow;

        objVel = rb.velocity;
        if(rb.velocity.magnitude < trueSpeed)
        {
            rb.velocity = rb.velocity.normalized * trueSpeed;
        }
        if(objVel == new Vector2(0,0))
        {
            direction = Random.insideUnitCircle.normalized;
            rb.velocity = direction.normalized * trueSpeed;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
         ContactPoint2D hit = collision.GetContact(0);
         Vector2 reflected = Vector2.Reflect(objVel, hit.normal);
         rb.velocity = reflected.normalized * trueSpeed;
    }
}
