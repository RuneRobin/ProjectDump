using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSpawner : MonoBehaviour
{

    public int bulletsLeft = 100;
    public int bulletsSpawned;
    public float cooldown = 1;
    public float spawnRadius = 5;
    public GameObject enemy;

    private EnemyStats enemyStats;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats = gameObject.GetComponent<EnemyStats>();
        //enemyStats.healthSlider.maxValue = enemiesLeft;
        StartCoroutine(SpawningMonsters());
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletsSpawned >= 10)
        {
            enemyStats.attacking = true; //for shock status
            bulletsSpawned -= 10;
        }

        if(bulletsLeft == 0)
        {
            enemyStats.woOTimer = 999f;
            enemyStats.openToAttack = true;
        }
    }

    public IEnumerator SpawningMonsters()
    {
        if (bulletsLeft >= 1)
        {
            //Vector2 centrePoint = (Random.insideUnitSphere * spawnRadius) + gameObject.transform.position; //random point in circle around this gameobject

            float randomSpawnPosition = Random.Range(0f, 1f); //random position somewhere on the edge of the screen
            int coinFlip = Random.Range(0, 2); //chooses between hor and ver
            float offscreen;
            if(Random.Range(0,2) == 0) //figures out if it should spawn slightly offscreen on the pos or neg axis
            {
                offscreen = -0.1f;
            }else
            {
                offscreen = 1.1f;
            }

            Vector2 horizontalSpawn = Camera.main.ViewportToWorldPoint(new Vector2(randomSpawnPosition, offscreen));
            Vector2 verticalSpawn  = Camera.main.ViewportToWorldPoint(new Vector2(offscreen, randomSpawnPosition));
            Vector2 spawnPoint = new Vector2();

            if(coinFlip == 0)
            {
                spawnPoint = horizontalSpawn;
            }
            if(coinFlip == 1)
            {
                spawnPoint = verticalSpawn;
            }

            Instantiate(enemy, spawnPoint, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            enemy.GetComponent<EnemyBehaviour>().speed = Random.Range(6, 10);

            bulletsLeft--;
            bulletsSpawned++;
            yield return new WaitForSeconds(cooldown);
            StartCoroutine(SpawningMonsters());
        }
    }
}
