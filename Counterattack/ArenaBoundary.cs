using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBoundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PolygonCollider2D poly = GetComponent<PolygonCollider2D>();
        Vector2[] points = poly.points;
        EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();
        edge.points = points;
        Destroy(poly);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Arm" && collision.GetComponent<ArmStats>().armType != "Sword") || collision.tag == "AllyProjectile")
        {
            if (GameObject.Find("Tantrum Arm"))
            {
                GameObject.Find("Tantrum Arm").GetComponent<TantrumArm>().tantrumStacks++;
            }
        }
    }

}
