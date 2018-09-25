using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickScript : MonoBehaviour {

    //current money
    public float Revenue = 0.0f;

    //text displaying income
    public Text money;
    public Text tShirtTextStat;
    public Text roadiesTextStat;
    public Text CDTextStat;

    //store buttons (rock out is the primary clicker button)
    public Button rockOut;
    public Button tShirt;
    public Button roadies;
    public Button CD;

    //current amount of moneymakers bought
    public float tShirtCount = 0.0f;
    public float roadiesCount = 0.0f;
    public float CDCount = 0.0f;

    //cost of moneymakers
    public float tShirtCost = 15.0f;
    public float roadiesCost = 100.0f;
    public float CDCost = 500.0f;

    //income made per second by moneymakers
    public float rockOutIncome = 1.0f;
    public float tShirtIncome = 1.0f;
    public float roadieIncome = 5.0f;
    public float CDIncome = 20.0f;

    public bool WalkAdvBought = false;
    public float WalkAdvBonus = 0.0f;

    
    // Use this for initialization
    void Start ()
    {
        Button btn = rockOut.GetComponent<Button>();
        btn.onClick.AddListener(RockOut);

        Button shirtBtn = tShirt.GetComponent<Button>();
        shirtBtn.onClick.AddListener(TShirts);

        Button roadBtn = roadies.GetComponent<Button>();
        roadBtn.onClick.AddListener(Roadies);

        Button CDBtn = CD.GetComponent<Button>();
        CDBtn.onClick.AddListener(CDProd);

        InvokeRepeating("RevenueStream", 0.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void RockOut()
    {
        Revenue = Revenue + rockOutIncome;
    }

    void TShirts()
    {
        if(Revenue >= tShirtCost)
        {
            Revenue = Revenue - tShirtCost;
            tShirtCost = tShirtCost * 1.1f;
            tShirtCount++;
            tShirt.GetComponentInChildren<Text>().text = ("T-Shirt Production\nCost: $" + tShirtCost.ToString("F2"));
            
        }
    }

    void Roadies()
    {
        if (Revenue >= roadiesCost)
        {
            Revenue = Revenue - roadiesCost;
            roadiesCost = roadiesCost * 1.1f;
            roadiesCount++;
            roadies.GetComponentInChildren<Text>().text = ("Hire a Roadie\nCost: $" + roadiesCost.ToString("F2"));
            
        }
    }

    void CDProd()
    {
        if (Revenue >= CDCost)
        {
            Revenue = Revenue - CDCost;
            CDCost = CDCost * 1.1f;
            CDCount++;
            CD.GetComponentInChildren<Text>().text = ("CD Production\nCost: $" + CDCost.ToString("F2"));
            
        }
    }

    void RevenueStream()
    {
        if(WalkAdvBought == true)
        {
            WalkAdvBonus = (tShirtCount * 0.2f);
        }

        Revenue = Revenue + (tShirtCount * (tShirtIncome)) + (roadiesCount * (roadieIncome + WalkAdvBonus)) + (CDCount * CDIncome);

        money.text = "Money: $" + Revenue.ToString("F2");

        tShirtTextStat.text = "T-Shirts: " + tShirtCount.ToString() + "\nIncome per T-Shirt: $" + (tShirtIncome).ToString() + "\nTotal Income: $" + (tShirtCount * tShirtIncome).ToString();

        roadiesTextStat.text = "Roadies: " + roadiesCount.ToString() + "\nIncome per Roadie: $" + (roadieIncome + WalkAdvBonus).ToString() + "\nTotal Income: $" + (roadiesCount * (roadieIncome + WalkAdvBonus)).ToString();

        CDTextStat.text = "CDs: " + CDCount.ToString() + "\nIncome per CD: $" + (CDIncome).ToString() + "\nTotal Income: $" + (CDCount * CDIncome).ToString();
    }

}
