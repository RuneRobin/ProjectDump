using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : MonoBehaviour
{
    private Master master;

    public int moneyPerDig = 1;

    public float colourMoneyChance = 5f;

    public float littleGuysSpeed = 0.5f;

    public int colour;

    public bool isDestroyingLittleGuys = false;

    // Start is called before the first frame update
    public void Init()
    {
        master = Master.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoneyDig()
    {
        master.ScoreReduction((ulong)moneyPerDig);
        master.UpdateClicksText();

        if (Random.Range(0f,100f) <= colourMoneyChance)
        {
            master.colourMoney[colour]++;
            master.UpdateColourMoneyText(colour);
        }
    }

    public IEnumerator DelayedDeath()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject toKill = master.colourGuys[colour][0];
            master.colourGuys[colour].Remove(toKill);
            Destroy(toKill);
            master.UpdateAmountOfLittleGuys(colour);
            yield return new WaitForSeconds(0.05f);
        }
        isDestroyingLittleGuys = false;
    }
}
