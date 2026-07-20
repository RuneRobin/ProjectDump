using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(LineController), typeof(PolygonCollider2D))]
public class BeamBullet : MonoBehaviour
{
    public float despawnTime = 10;
    public float bulletDamage;
    public bool pierces = false;
    private List<GameObject> alreadyHit = new List<GameObject>();

    private List<Vector2> colliderPoints = new List<Vector2>();
    public LineController lc;
    public PolygonCollider2D pc2D;

    // Start is called before the first frame update
    void Start()
    {
        lc = gameObject.GetComponent<LineController>();
        pc2D = gameObject.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        colliderPoints = CalculateColliderPoints();
        pc2D.SetPath(0, colliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));

    }

    private List<Vector2> CalculateColliderPoints()
    {
        Vector3[] positions = lc.GetPositions();

        float width = lc.GetWidth();

        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        List<Vector2> colliderPositions = new List<Vector2>
        {
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1]
        };
        return colliderPositions;
    }

    
    

    public IEnumerator Despawner()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //collision.gameObject.GetComponent<EnemyBehaviour>().health -= bulletDamage;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    { 

    }

}
