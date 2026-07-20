using UnityEngine;

public class DespawnerScript : MonoBehaviour
{
    public string objectType;
    private SpriteRenderer spr;

    private float despawnTimer = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        if (objectType == "HealSparkle")
        {
            despawnTimer = 1f; //useful for when more get added to this script
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        despawnTimer -= Time.deltaTime;
        if(despawnTimer <= 0)
        {
            Destroy(gameObject);
        }

        if (objectType == "HealSparkle")
        {
            spr.color = new Color(spr.color.r,spr.color.g,spr.color.b,despawnTimer); //convenient line up
        }

    }
}
