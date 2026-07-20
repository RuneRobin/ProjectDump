using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ColourEnum;

public class AllUpgradeButtons : MonoBehaviour
{
    private Master master;

    private bool sacrificeUpgrade = false;

    public void Init()
    {
        master = Master.instance;
    }

    public void MoreScorePerClick(int timesMultiply) //Clicks substract 1*timesMultiply more per upgrade
    {
        if (master.clicksEarned >= 20 * timesMultiply)
        {
            master.clicksEarned -= 20 * timesMultiply;
            master.scoreReduceOnClick += 1 * (ulong)timesMultiply;
            master.UpdateClicksText();
        }
    }

    public void RandomColourMoney(int timesMultiply)
    {
        if (master.clicksEarned >= 100 * timesMultiply)
        {
            master.clicksEarned -= 100 * timesMultiply;
            master.UpdateClicksText();

            for (int i = 0; i < timesMultiply; i++)
            {
                int r = Random.Range(0, 5);

                master.colourMoney[r]++;
                master.colourMoneyText[r].text = master.colourMoney[r].ToString();
            }
        }
    }

    public void UpgradePercentDropChanceColourMoney(int colour)
    {
        if (master.colourMoney[colour] >= (ulong)master.piles[colour].GetComponent<Pile>().colourMoneyChance && master.piles[colour].GetComponent<Pile>().colourMoneyChance < 100)
        {
            master.colourMoney[colour] -= (ulong)master.piles[colour].GetComponent<Pile>().colourMoneyChance;
            master.piles[colour].GetComponent<Pile>().colourMoneyChance += 5;
            EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text = master.piles[colour].GetComponent<Pile>().colourMoneyChance.ToString() + "%";

            master.UpdateColourMoneyText(colour);

            if (master.piles[colour].GetComponent<Pile>().colourMoneyChance >= 100)
            {
                EventSystem.current.currentSelectedGameObject.transform.GetChild(1).transform.gameObject.SetActive(false);
                EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void SpawnGuys(int colour)
    {
        if (master.colourMoney[colour] >= (ulong)master.colourGuys[colour].Count && master.colourGuys[colour].Count < 9999)
        {
            master.CreateGuy(colour);
            master.colourMoney[colour] -= (ulong)master.colourGuys[colour].Count;
            master.UpdateColourMoneyText(colour);
            if(master.colourGuys[colour].Count >= 9998)
            {
                EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void IncreaseGlobalLittleGuysSpeed()
    {
        if (master.littleGuysGlobalSpeedMultiplier < 2)
        {
            int am = 0;
            for (int i = 0; i < 5; i++) //rudimentary way of checking if you have 5 or more of each colour money
            {
                if (master.colourMoney[i] >= 5)
                {
                    am++;
                }
            }
            if (am >= 5) //substracts 5 from each colour money and increases global speed by 0.1 per second up to a max of 1
            {
                for (int i = 0; i < 5; i++)
                {
                    master.colourMoney[i] -= 5;
                    master.UpdateColourMoneyText(i);
                }
                master.littleGuysGlobalSpeedMultiplier += 0.1f;
                EventSystem.current.currentSelectedGameObject.transform.GetChild(0).transform.GetComponent<Text>().text = "x" + master.littleGuysGlobalSpeedMultiplier + "/s";
                if (master.littleGuysGlobalSpeedMultiplier >= 2)
                {
                    EventSystem.current.currentSelectedGameObject.transform.GetChild(1).transform.gameObject.SetActive(false);
                    EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public void IncreaseOneColourLittleGuysSpeed(int colour)
    {
        if (master.colourMoney[colour] >= 5 && master.piles[colour].GetComponent<Pile>().littleGuysSpeed < 1)
        {
            master.colourMoney[colour] -= 5;
            master.UpdateColourMoneyText(colour);
            master.piles[colour].GetComponent<Pile>().littleGuysSpeed += 0.1f;
            EventSystem.current.currentSelectedGameObject.transform.GetChild(0).transform.GetComponent<Text>().text = "+" + master.piles[colour].GetComponent<Pile>().littleGuysSpeed + "/s";

            if(master.piles[colour].GetComponent<Pile>().littleGuysSpeed >= 1)
            {
                EventSystem.current.currentSelectedGameObject.transform.GetChild(1).transform.gameObject.SetActive(false);
                EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void TradeLittleGuysForPower(int colour)
    {
        if (sacrificeUpgrade == false)
        {
            if (master.colourGuys[colour].Count >= 50 && master.piles[colour].GetComponent<Pile>().isDestroyingLittleGuys == false)
            {
                master.piles[colour].GetComponent<Pile>().moneyPerDig++;
                EventSystem.current.currentSelectedGameObject.transform.GetChild(0).transform.GetComponent<Text>().text = "-" + master.piles[colour].GetComponent<Pile>().moneyPerDig;

                master.piles[colour].GetComponent<Pile>().isDestroyingLittleGuys = true;
                StartCoroutine(master.piles[colour].GetComponent<Pile>().DelayedDeath());
            }
        }
        else
        {
            if(master.colourMoney[colour] >= 50)
            {
                master.colourMoney[colour] -= 50;
                master.UpdateColourMoneyText(colour);
                master.piles[colour].GetComponent<Pile>().moneyPerDig++;
                EventSystem.current.currentSelectedGameObject.transform.GetChild(0).transform.GetComponent<Text>().text = "-" + master.piles[colour].GetComponent<Pile>().moneyPerDig;
            }
        }
    }

    public void DoubleSubstractionChance()
    {
        if(master.scoreSubstracted >= 10000 && master.chanceToDoubleSubstraction < 100)
        {
            master.chanceToDoubleSubstraction++;
            master.scoreSubstracted -= 10000;
            master.UpdateScoreText();
            EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text = "+" + master.chanceToDoubleSubstraction.ToString() + "% x2";

            if (master.chanceToDoubleSubstraction >= 100)
            {
                EventSystem.current.currentSelectedGameObject.transform.GetChild(1).transform.gameObject.SetActive(false);
                EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void DoubleClicksChance()
    {
        if (master.scoreSubstracted >= 10000 && master.chanceToDoubleClicksEarned < 100)
        {
            master.chanceToDoubleClicksEarned++;
            master.scoreSubstracted -= 10000;
            master.UpdateScoreText();
            EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text = "+" + master.chanceToDoubleClicksEarned.ToString() + "% x2";

            if (master.chanceToDoubleClicksEarned >= 100)
            {
                EventSystem.current.currentSelectedGameObject.transform.GetChild(1).transform.gameObject.SetActive(false);
                EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void AutoClickRate()
    {
        if(master.clicksEarned >= 500)
        {
            master.clicksEarned -= 500;
            master.autoClickRate++;
            if(master.autoClickRate == 1) //Starts the autoclicking
            {
                StartCoroutine(master.AutoClick());
            }
        }
    }

    public void BuyEachLittleGuy()
    {
        for(int i = 0; i < master.piles.Count; i++)
        {
            if(master.colourMoney[i] >= (ulong)master.colourGuys[i].Count && master.piles[i].GetComponent<Pile>().isDestroyingLittleGuys == false)
            {
                master.colourMoney[i] -= (ulong)master.colourGuys[i].Count;
                master.CreateGuy(i);
            }
        }
    }

    public void BonusClicksPerClick(int timesMultiply)
    {
        if (master.scoreSubstracted >= (ulong)(5000 * timesMultiply))
        {
            master.scoreSubstracted -= (ulong)(5000 * timesMultiply);
            master.clicksPerClick += 1 * timesMultiply;
            master.UpdateScoreText();
        }
    }

    public void AutoBuyUnlock()
    {
        if(master.scoreSubstracted >= 1000000 && master.autoBuyUnlocked == false)
        {
            master.scoreSubstracted -= 1000000;
            master.autoBuyUnlocked = true;
            master.UpdateScoreText();
            StartCoroutine(AutoBuyLittleGuys());
            EventSystem.current.currentSelectedGameObject.transform.GetChild(1).transform.gameObject.SetActive(false);
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
        }
    }

    public IEnumerator AutoBuyLittleGuys()
    {
        BuyEachLittleGuy();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(AutoBuyLittleGuys());
    }

    public void UpgradeToSacrificeForPower()
    {
        if(master.scoreSubstracted >= 100000000)
        {
            master.scoreSubstracted -= 100000000;
            sacrificeUpgrade = true;
            EventSystem.current.currentSelectedGameObject.transform.GetComponent<SwitchSacrificeCost>().SwitchCost();
            EventSystem.current.currentSelectedGameObject.transform.GetChild(1).transform.gameObject.SetActive(false);
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
            master.UpdateScoreText();
        }
    }
}
