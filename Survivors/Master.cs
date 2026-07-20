using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    public static Master instance;

    public int moneyCollectedDuringRun;
    public int enemiesDefeated;


    // Start is called before the first frame update
    void Start()
    {
        
        if (Master.instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
