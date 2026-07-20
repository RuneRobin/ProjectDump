using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    public static MouseFollower instance;

    private Vector2 mousePosTarget;
    public Vector2 dis;
    private Rigidbody2D rb;
    public GameObject screen;
    public int turnSpeed = 3;
    public float turnTreshold = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (screen.activeInHierarchy == false)
        {
            Mathf.Clamp(transform.position.x, -5, 5);

            mousePosTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dis = (mousePosTarget - (Vector2)transform.position);

            if ((dis.x < -turnTreshold && transform.position.x > -5) || (dis.x > turnTreshold && transform.position.x < 5))
            {
                rb.linearVelocity = dis.normalized * turnSpeed;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (rb.linearVelocity != Vector2.zero)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
