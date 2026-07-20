using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    private EnemyStats enemyStats;
    
    public float summonCooldown = 5f;
    private GameObject summon;
    private GameObject summonPrefab;
    public int maxSummonCount = 3;
    public int summonsActive = 0;

    private Vector2 areaToMoveIn;
    private GameObject armSpawnArea;
    private float timerToMove = 1f;

    // Start is called before the first frame update
    void Start()
    {
        armSpawnArea = GameObject.Find("ArmSpawnArea");
        areaToMoveIn = (Random.insideUnitCircle * (armSpawnArea.GetComponent<CircleCollider2D>().radius * armSpawnArea.GetComponent<Transform>().localScale.x)) - (Vector2)armSpawnArea.transform.position;

        summonPrefab = Resources.Load("Enemy Assets/Minion") as GameObject;
        enemyStats = gameObject.GetComponent<EnemyStats>();
        StartCoroutine(AttackCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, areaToMoveIn, Time.deltaTime*2);

        if (timerToMove > 0)
        {
            timerToMove -= Time.unscaledDeltaTime;
        }
        if(timerToMove <= 0)
        {
            areaToMoveIn = (Random.insideUnitCircle * (armSpawnArea.GetComponent<CircleCollider2D>().radius * armSpawnArea.GetComponent<Transform>().localScale.x)) - (Vector2)armSpawnArea.transform.position;
            timerToMove = 1f;
        }



        if (summonsActive == 0 && enemyStats.woOTimer < 1)
        {
            enemyStats.woOTimer = 10;
        }
        if(summonsActive > 0 && enemyStats.woOTimer > 0)
        {
            enemyStats.woOTimer = 0;
        }
    }

    public IEnumerator AttackCooldown()
    {
        for (int i = 0; i < maxSummonCount; i++)
        {
            if (summonsActive < maxSummonCount)
            {
                summon = Instantiate(summonPrefab, transform.position, transform.rotation);
                summon.GetComponent<SummonScript>().summoner = gameObject;
                summonsActive++;

                yield return new WaitForSeconds(0.2f);
            }
        }


        yield return new WaitForSeconds(summonCooldown);
        StartCoroutine(AttackCooldown());

    }
}
