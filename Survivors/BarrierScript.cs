using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    private new Camera camera;
    Vector2 dir;
    private float barrierRange; //how big the circle of the barrier is
    private float barrierWidth = 10f;
    private ParticleSystem particle;
    private Denim denim;
    private bool isDamaged = false;

    // Start is called before the first frame update
    void Start()
    {
        barrierRange = gameObject.GetComponent<CircleCollider2D>().radius;
        particle = gameObject.GetComponentInChildren<ParticleSystem>();
        camera = Camera.main;
        denim = GameObject.Find("Denim").GetComponent<Denim>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir = camera.ScreenToWorldPoint(Input.mousePosition) - PlayerMovement.instance.transform.position;

        ParticleSystem.ShapeModule shape = particle.shape;
        shape.arc = denim.barrierSize * 2;
        shape.radius = barrierRange;
        particle.transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        particle.transform.localEulerAngles += new Vector3(0, 0, 90 - denim.barrierSize);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (isDamaged == false) //damages barrier and gives it an invul window
            {
                denim.barrierHealth -= collision.GetComponent<EnemyBehaviour>().damage;
                isDamaged = true;
                StartCoroutine(Damaged());
            }
            if(collision.GetComponent<EnemyBehaviour>().isShocked == false) //inflicts status ailment 'Shock'
            {
                collision.GetComponent<EnemyBehaviour>().isShocked = true;
                StartCoroutine(collision.GetComponent<EnemyBehaviour>().Shocked()); 
            }
            

            if(Vector3.Angle(dir, collision.transform.position - PlayerMovement.instance.transform.position) < denim.barrierSize) //effective push range arc
            {
                if(Vector3.Distance(collision.transform.position, PlayerMovement.instance.transform.position) > barrierRange - barrierWidth) //thicker width means more push when running at them
                {
                    collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - PlayerMovement.instance.transform.position).normalized * denim.barrierPush); // change number to force push variable
                }
            }
            
        }
    }

    public IEnumerator Damaged()
    {
        yield return new WaitForSeconds(1);
        isDamaged = false;
    }
}
