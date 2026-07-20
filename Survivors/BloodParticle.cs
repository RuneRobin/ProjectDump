using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour
{
    private float catchUp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
            gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position + (GameObject.FindGameObjectWithTag("Guardian").gameObject.transform.position - transform.position) * catchUp * Time.deltaTime);
            catchUp += 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Guardian")
        {
            Destroy(gameObject);
        }
    }
}
