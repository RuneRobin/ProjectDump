using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreativeButtons : MonoBehaviour
{
    public float sizeThreshold = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = sizeThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
