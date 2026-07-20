using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    public float enemiesLeft = 10;
    public float cooldown = 1;
    public float spawnRadius = 5;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawningMonsters());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawningMonsters()
    {
        if (enemiesLeft >= 1)
        {
            Vector2 centrePoint = (Random.insideUnitSphere * spawnRadius) + gameObject.transform.position; //random point in circle around this gameobject

            float randomSpawnPosition = Random.Range(0f, 1f); //random position somehwere on the edge of the screen
            float coinFlip = Random.Range(0f, 1f); //chooses between hor and ver

            Vector2 horizontalSpawn = Camera.main.ViewportToWorldPoint(new Vector2(randomSpawnPosition, -0.1f)); //-0.1 will make it just slightly off screen
            Vector2 verticalSpawn  = Camera.main.ViewportToWorldPoint(new Vector2(-0.1f, randomSpawnPosition));
            Vector2 spawnPoint = new Vector2();


            if(coinFlip <= 0.5f)
            {
                spawnPoint = horizontalSpawn;
            }
            if(coinFlip > 0.5f)
            {
                spawnPoint = verticalSpawn;
            }

            if(Random.Range(0f,1f) <= 0.5f)
            {
                spawnPoint *= -1;
            }

            Instantiate(enemy, spawnPoint, Quaternion.Euler(new Vector3(0f, 0f, 0f)));

            enemiesLeft--;
            yield return new WaitForSeconds(cooldown);
            StartCoroutine(SpawningMonsters());
        }
    }
}
