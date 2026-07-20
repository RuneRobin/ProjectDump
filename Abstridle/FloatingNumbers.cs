using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour
{
    public float despawnTimer = 1;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(Master.instance.canvas.transform);
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0, 1 * Time.deltaTime, 0);

        despawnTimer -= Time.deltaTime;
        GetComponent<Text>().color = new Color(0,0,0,despawnTimer);
        if(despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
