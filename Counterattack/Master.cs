using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    public static Master instance;

    [Header("Run Stats")]
    public int moneyCollectedDuringRun;
    public int enemiesDefeated;
    public int enemyMilestone = 5;
    public int stagesCleared;
    public int difficulty = 1;

    [Header("Enemy Count")]
    public int maxEnemyCount;
    public int currentEnemyCount;
    public int enemiesLeft;

    

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
        if (Input.GetKeyDown("q"))
        {
            enemiesDefeated++;
        }
        if(enemiesDefeated >= enemyMilestone)
        {
            difficulty++;
            enemyMilestone += Mathf.CeilToInt(enemyMilestone * 1.1f);
        }
    }
}
