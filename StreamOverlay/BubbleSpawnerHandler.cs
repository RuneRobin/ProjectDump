using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawnerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BubbleDespawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator BubbleDespawner()
    {
        
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
