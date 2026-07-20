using System.Collections.Generic;
using UnityEngine;

public class CircusClown : MonoBehaviour
{
    private UnitScript us;
    private GameObject friendgameObject;

    public float comeback = 0;

    public int lilFriends = 1;
    public float friendSummonTimer = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        us = gameObject.GetComponent<UnitScript>();
        us.faction = "Circus";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        friendSummonTimer -= Time.deltaTime;
        if(friendSummonTimer <= 0 && MasterController.instance.winnerText.isActiveAndEnabled == false)
        {
            for(int i = 0; i < lilFriends; i++)
            {
                Vector3 b = gameObject.GetComponent<SpriteRenderer>().bounds.size;

                friendgameObject = Instantiate(MasterController.instance.lilguyPrefab, transform.localPosition + new Vector3(Random.Range(-b.x/2,b.x/2),Random.Range(-b.y/2,b.y/2)), transform.rotation);
                lilguyScript lgs = friendgameObject.GetComponent<lilguyScript>();
                lgs.masterUs = us;
                lgs.army = us.army;
                lgs.enemyArmy = us.enemyArmy;
            }
            lilFriends++;
            friendSummonTimer = 20f;
        }
    }
}
