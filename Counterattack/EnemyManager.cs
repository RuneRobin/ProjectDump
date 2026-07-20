using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public CharacterLoader cl;
    public GameObject arena;

    public GameObject[] enemyEncountersArea1;
    public GameObject[] enemyEncountersArea2;
    public GameObject[] enemyEncountersArea3;

    public GameObject[] eliteEncounters;

    public GameObject[] bossEncounters;

    private void Start()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        if(cl.currEncounter == "EnemyEncounter")
        {
            Master.instance.enemiesLeft = Master.instance.difficulty * 3;
            Master.instance.maxEnemyCount = Mathf.Clamp(Master.instance.stagesCleared + 1, 1, 15); //clamps maximum possible enemycount to 15 at any given time

            for (Master.instance.currentEnemyCount = 0; Master.instance.currentEnemyCount < Master.instance.maxEnemyCount && Master.instance.enemiesLeft > 0; Master.instance.currentEnemyCount++)
            {
                Master.instance.enemiesLeft--;
                Vector2 point = Random.insideUnitCircle.normalized * Random.Range(arena.GetComponent<CircleCollider2D>().radius * 0.1f, arena.GetComponent<CircleCollider2D>().radius * 0.95f) * arena.GetComponent<Transform>().localScale;

                if (cl.currAreaAndNode.Contains("Area 1"))
                {
                    GameObject newEnemy = Instantiate(enemyEncountersArea1[Random.Range(0, enemyEncountersArea1.Length)], point, transform.rotation);
                    newEnemy.GetComponent<EnemyStats>().enemyType = "Square";
                }
                else if (cl.currAreaAndNode.Contains("Area 2"))
                {
                    GameObject newEnemy = Instantiate(enemyEncountersArea2[Random.Range(0, enemyEncountersArea2.Length)], point, transform.rotation);
                    newEnemy.GetComponent<EnemyStats>().enemyType = "Circle";
                }
                else if (cl.currAreaAndNode.Contains("Area 3"))
                {
                    GameObject newEnemy = Instantiate(enemyEncountersArea3[Random.Range(0, enemyEncountersArea3.Length)], point, transform.rotation);
                    newEnemy.GetComponent<EnemyStats>().enemyType = "Triangle";
                }
            }
        }
        else if(cl.currEncounter == "BossEncounter")
        {
            if(cl.currAreaAndNode.Contains("Area 1"))
            {

            }
            else if (cl.currAreaAndNode.Contains("Area 2"))
            {

            }
            else if (cl.currAreaAndNode.Contains("Area 3"))
            {
                for (int i = 0; i < 100; i++)
                {
                    Vector2 point = Random.insideUnitCircle.normalized * Random.Range(arena.GetComponent<CircleCollider2D>().radius * 0.5f, arena.GetComponent<CircleCollider2D>().radius * 0.95f) * arena.GetComponent<Transform>().localScale;

                    GameObject newEnemy = Instantiate(bossEncounters[0], point, transform.rotation);
                    newEnemy.GetComponent<EnemyStats>().enemyType = "Triangle";
                }
                Master.instance.enemiesLeft = 0;
                Master.instance.currentEnemyCount = 100;
                Master.instance.maxEnemyCount = 100;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(cl.currEncounter == "EnemyEncounter" && Master.instance.currentEnemyCount < Master.instance.maxEnemyCount && Master.instance.enemiesLeft > 0)
        {
            Master.instance.currentEnemyCount++;
            Master.instance.enemiesLeft--;
            Vector2 point = Random.insideUnitCircle.normalized * Random.Range(arena.GetComponent<CircleCollider2D>().radius * 0.1f, arena.GetComponent<CircleCollider2D>().radius * 0.95f) * arena.GetComponent<Transform>().localScale;

            if (cl.currAreaAndNode.Contains("Area 1"))
            {
                GameObject newEnemy = Instantiate(enemyEncountersArea1[Random.Range(0, enemyEncountersArea1.Length)], point, transform.rotation);
            }
            if (cl.currAreaAndNode.Contains("Area 2"))
            {
                GameObject newEnemy = Instantiate(enemyEncountersArea2[Random.Range(0, enemyEncountersArea2.Length)], point, transform.rotation);
            }
            if (cl.currAreaAndNode.Contains("Area 3"))
            {
                GameObject newEnemy = Instantiate(enemyEncountersArea3[Random.Range(0, enemyEncountersArea3.Length)], point, transform.rotation);
            }
        }
    }
}
