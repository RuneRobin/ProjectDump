using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtPrefabScript : MonoBehaviour
{
    private float rotationSpeed;
    private Color flashValues;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(-75f, 75f);
        flashValues = new Color(1f, 1f, 1f, 1f);
        transform.SetSiblingIndex(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        flashValues.a -= 0.02f;
        GetComponent<Image>().color = new Color(1f, 1f, 1f, flashValues.a);
        if(flashValues.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
