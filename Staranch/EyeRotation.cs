using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRotation : MonoBehaviour
{
    private RunnerScript runScript;
    private float angle;
    Vector2 v;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        v = gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity;
        angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
