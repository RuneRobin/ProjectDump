using UnityEngine;

public class PongBall : MonoBehaviour
{
    private Pong p;

    private Canvas canvas;
    private RectTransform canvasTransform;

    Vector3 dir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        p = Pong.instance;
        canvas = FindAnyObjectByType<Canvas>();
        canvasTransform = canvas.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        dir = (transform.up * CoinFlip()) + -transform.right;

        GetComponent<Rigidbody2D>().AddForce(dir * 100);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 canvasSize = canvasTransform.sizeDelta * canvasTransform.localScale;

        //bounces on the floor and ceiling
        if(transform.position.y < -canvasSize.y/2)
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocityY = Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().linearVelocityY);
        }
        else if(transform.position.y > canvasSize.y / 2)
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocityY = -Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().linearVelocityY);
        }

        //Streamer scores
        if(gameObject.transform.position.x < -canvasSize.x/2)
        {
            p.score[1]++;
            transform.position = Vector3.zero;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            dir = (transform.up * CoinFlip()) + -transform.right;

            GetComponent<Rigidbody2D>().AddForce(dir * 100);
        }
        //Chat scores
        else if(gameObject.transform.position.x > canvasSize.x / 2)
        {
            p.score[0]++;
            transform.position = Vector3.zero;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            dir = (transform.up * CoinFlip()) + -transform.right;

            GetComponent<Rigidbody2D>().AddForce(dir * 100);
        }
    }

    private int CoinFlip()
    {
        if (Random.Range(0, 2) == 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }



    /*private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("hit");
        //gameObject.GetComponent<Rigidbody2D>().linearVelocityX *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject == p.chatPaddle.gameObject || collision.transform.gameObject == p.streamerPaddle.gameObject)
        {
            Debug.Log("hittered");
            gameObject.GetComponent<Rigidbody2D>().linearVelocityX *= -1;
        }
        else if(collision.name.Contains("Boundary"))
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocityX *= -1;
            gameObject.GetComponent<Rigidbody2D>().linearVelocityY *= -1;
        }
    }*/
}
