using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumb : MonoBehaviour
{
    private GameObject noPermitZone;

    // Start is called before the first frame update
    void Start()
    {
        noPermitZone = Resources.Load("NoPermit") as GameObject;
        noPermitZone = Instantiate(noPermitZone, transform.position, transform.rotation);
        noPermitZone.transform.parent = gameObject.transform;
        noPermitZone.layer = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
