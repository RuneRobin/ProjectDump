using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarianaPaste : MonoBehaviour
{
    public int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health--;
        if(health < 1)
        {
            Destroy(gameObject);
        }
    }
}
