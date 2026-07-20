using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ColourEnum;

public class Master : MonoBehaviour
{
    public static Master instance;
    public Canvas canvas;

    public ulong ScoreLeft = 0;
    public Text scoreText;
    public ulong scoreReduceOnClick = 1;

    public int clicksEarned = 0;
    public int clicksPerClick = 1;
    public Text clicksText;
    public ulong scoreSubstracted = 0;
    public Text scoreSubText;

    public int chanceToDoubleSubstraction = 0;
    public int chanceToDoubleClicksEarned = 0;
    public int autoClickRate = 0;

    public List<ulong> colourMoney = new List<ulong>(5); //Contains all 5 colours of money
    public List<Text> colourMoneyText = new List<Text>(5); //Contains all 5 colours of the text signifying colour money
    public List<List<GameObject>> colourGuys = new List<List<GameObject>>(); //A list of all 5 colours of currently active little guys
    public List<Text> colourGuysPileAmountText = new List<Text>(); //A list of the texts on each pile that show how many little guys there are

    public List<GameObject> piles = new List<GameObject>(); //All 5 colour piles

    public GameObject littleGuyPrefab;
    public GameObject floatingNumberPrefab;
    public GameObject mainUpgradeMenuPopUp;
    public GameObject pileUpgradeMenuPopup;

    public float littleGuysGlobalSpeedMultiplier = 1f;
    public bool autoBuyUnlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        FindObjectOfType<AllUpgradeButtons>().Init();
        foreach(GameObject g in piles)
        {
            g.GetComponent<Pile>().Init();
        }

        for(int i = 0; i < 5; i++) //makes 5 lists for each colour of little guys
        {
            colourGuys.Add(new List<GameObject>());
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainOnClick()
    {
        ScoreReduction(scoreReduceOnClick);
        clicksEarned += 1 * clicksPerClick;
        if(chanceToDoubleClicksEarned >= Random.Range(0,100))
        {
            clicksEarned += 1 * clicksPerClick;
        }

        GameObject floatNum = Instantiate(floatingNumberPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);

        floatNum.GetComponent<Text>().text = "-" + scoreReduceOnClick.ToString();
        //floatNum.transform.position = new Vector3(floatNum.transform.position.x, floatNum.transform.position.y, 0);
        UpdateClicksText();
    }

    public void CreateGuy(int colour) //Used for creating little guys from upgrade shop
    {
        GameObject littleGuy = Instantiate(littleGuyPrefab, piles[colour].transform.position, transform.rotation);

        littleGuy.GetComponent<LittleGuy>().colour = colour;
        littleGuy.GetComponent<LittleGuy>().mainPile = transform.position;
    }

    public void UpdateScoreText()
    {
        scoreText.text = ScoreLeft.ToString();
        scoreSubText.text = scoreSubstracted.ToString();
    }

    public void UpdateClicksText()
    {
        clicksText.text = clicksEarned.ToString();
    }

    public void UpdateColourMoneyText(int colour)
    {
        colourMoneyText[colour].text = colourMoney[colour].ToString();
    }

    public void UpdateAmountOfLittleGuys(int colour)
    {
        colourGuysPileAmountText[colour].text = colourGuys[colour].Count.ToString();
    }

    public void MoveMenuSideToSide(string menuType)
    {
        if(menuType == "Main")
        {
            GameObject menu = EventSystem.current.currentSelectedGameObject.transform.parent.transform.gameObject;
            if(menu.transform.localPosition.x == -960)
            {
                menu.transform.localPosition = new Vector3(-1590, menu.transform.localPosition.y,0);
            }
            else
            {
                menu.transform.localPosition = new Vector3(-960, menu.transform.localPosition.y,0);
            }
        }
        else if(menuType == "Piles")
        {
            GameObject menu = EventSystem.current.currentSelectedGameObject.transform.parent.transform.gameObject;
            if (menu.transform.localPosition.x == 960)
            {
                menu.transform.localPosition = new Vector3(1590, menu.transform.localPosition.y, 0);
            }
            else
            {
                menu.transform.localPosition = new Vector3(960, menu.transform.localPosition.y, 0);
            }
        }
    }

    public void ScoreReduction(ulong amount)
    {
        ScoreLeft -= amount;
        scoreSubstracted += amount;
        if(chanceToDoubleSubstraction >= Random.Range(0,101))
        {
            ScoreLeft -= amount;
            scoreSubstracted += amount;
        }
        UpdateScoreText();
    }

    public IEnumerator AutoClick()
    {
        yield return new WaitForSeconds(1f / autoClickRate);
        MainOnClick();
        StartCoroutine(AutoClick());
    }
}
