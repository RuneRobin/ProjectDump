using UnityEngine;

public class lilguyScript : MonoBehaviour
{
    public string army;
    public string enemyArmy;

    public float damage = 0.5f;
    public float speed = 0.1f;

    public UnitScript masterUs;
    private GameObject mastersTarget;

    private bool isFlipped = false;
    private Vector3 direction;
    public float distance;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SceneConst.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(SceneConst.instance.SFXCollection[Random.Range(11,16)],0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (masterUs != null)
        {
            mastersTarget = masterUs.FindClosestEnemy();
        }
        else
        {
            Destroy(gameObject);
        }

        if (mastersTarget != null && MasterController.instance.formationSetupTimer <= 0)
        {
            direction = (mastersTarget.transform.position - transform.position).normalized;
            distance = Vector2.Distance(transform.position, mastersTarget.transform.position);
            //rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
            if (distance > 0)//zero personal space
            {
                //rb.velocity = Vector2.MoveTowards(rb.velocity, new Vector2(direction.x * speed, direction.y * speed), 5 * Time.fixedDeltaTime);
                rb.MovePosition((Vector2)transform.position + (new Vector2(direction.x * 0.5f, direction.y * 0.5f)) * Time.deltaTime * speed);
                speed += Time.deltaTime/50f;
            }
            else
            {
                //rb.velocity = new Vector2(0,0);
                rb.MovePosition(transform.position);
            }
        }

        if (mastersTarget != null)
        {
            if (mastersTarget.transform.position.x < transform.position.x && gameObject.transform.rotation != Quaternion.Euler(0, 180, 0) && isFlipped == false)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
                isFlipped = true;
            }
            else if (mastersTarget.transform.position.x > transform.position.x && gameObject.transform.rotation != Quaternion.Euler(0, 0, 0) && isFlipped == true)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                isFlipped = false;
            }
        }

        if(MasterController.instance.winnerText.isActiveAndEnabled == true)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<UnitScript>() && collision.gameObject.GetComponent<UnitScript>().army == enemyArmy)
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<UnitScript>().health -= damage;
            MasterController.instance.ArmyHealthUpdater(enemyArmy);
        }
    }


}
