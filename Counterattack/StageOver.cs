using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageOver : MonoBehaviour
{
    public Text gold;
    public Text enemy;

    // Start is called before the first frame update
    void Start()
    {
        gold = GameObject.Find("Gold Collected").GetComponent<Text>();
        enemy = GameObject.Find("Enemies Defeated").GetComponent<Text>();

        gold.text = "Gold Collected: " + Master.instance.moneyCollectedDuringRun;
        enemy.text = "Enemies Defeated: " + Master.instance.enemiesDefeated;

        Master.instance.moneyCollectedDuringRun = 0;
        Master.instance.enemiesDefeated = 0;
        Master.instance.stagesCleared = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
