using UnityEngine;

public class CustomColliderShape : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        PolygonCollider2D poly = GetComponent<PolygonCollider2D>();
        Vector2[] points = poly.points;
        EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();
        edge.points = points;
        Destroy(poly);
    }
}
