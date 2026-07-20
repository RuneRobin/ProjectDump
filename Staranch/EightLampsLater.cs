using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightLampsLater : MonoBehaviour
{
    private GameObject helper;
    private GameObject helperPrefab;

    public int moreMes = 8;
    public float buildUp = 0;
    public float TimeBeforeMore = 88;

    // Start is called before the first frame update
    void Start()
    {
        helperPrefab = Resources.Load("Little Instigator") as GameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        buildUp += Time.deltaTime;
        if(buildUp >= TimeBeforeMore)
        {
            StartCoroutine(CallInTheLilGuys());
        }
    }

    public IEnumerator CallInTheLilGuys()
    {
        buildUp = 0;
        for (int i=0;i<moreMes;i++)
        {
            helper = Instantiate(helperPrefab, transform.position, transform.rotation);
            helper.layer = gameObject.layer;
        }
        TimeBeforeMore /= 2;
        if(TimeBeforeMore < 8)
        {
            TimeBeforeMore = 8;
        }
        yield return null;
    }
}
