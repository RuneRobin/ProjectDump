using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RescueMenu : MonoBehaviour
{

    public Button firstRescue;
    public Text firstRescueText;
    public Image firstImage;

    public Button secondRescue;
    public Text secondRescueText;
    public Image secondImage;

    private GameObject[] allCharacters;
    public List<GameObject> rescueList;
    private List<int> randomNumberList;

    private Text rText;

    private int plus = 0;

    // Start is called before the first frame update
    void Awake()
    {
        allCharacters = Resources.LoadAll<GameObject>("Characters");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static List<int> RandomPick(int count, int minValue, int maxValue) //makes random numbers to pick for the upgrade menu
    {
        HashSet<int> uniqueNumbers = new HashSet<int>();

        while (uniqueNumbers.Count < count)
        {
            int newNumber = Random.Range(minValue, maxValue);
            uniqueNumbers.Add(newNumber); // HashSet ensures all values are unique
        }

        return new List<int>(uniqueNumbers);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;//pauses game
        plus = 0;
        
        rescueList = new List<GameObject>();

        foreach (GameObject character in allCharacters)
        {
            if (!GameObject.Find(character.name))
            {
                rescueList.Add(character);
            }
        }
        randomNumberList = RandomPick(rescueList.Count, 0, rescueList.Count); //picks numbers randomly to assign to the upgrade menu

        if (rescueList.Count >= 1)
        {
            firstImage.sprite = rescueList[randomNumberList[0]].GetComponent<SpriteRenderer>().sprite;
            firstRescue.onClick.RemoveAllListeners();
            rText = firstRescueText;
            firstRescue.onClick.AddListener(Categorizer());
            firstRescueText = rText;
        }
        else //catches if there are not enough friends. At 0 it should not give you the option to click on new friend to begin with but deal with that later
        {
            firstImage.sprite = null;
            firstRescue.onClick.RemoveAllListeners();
            firstRescueText.text = "No more friends here";
        }

        plus++;

        if (rescueList.Count >= 2)
        {
            
            secondImage.sprite = rescueList[randomNumberList[1]].GetComponent<SpriteRenderer>().sprite;
            secondRescue.onClick.RemoveAllListeners();
            rText = secondRescueText;
            secondRescue.onClick.AddListener(Categorizer());
            secondRescueText = rText;
        }
        else
        {
            secondImage.sprite = null;
            secondRescue.onClick.RemoveAllListeners();
            secondRescueText.text = "No more friends here";
        }
    }

    private UnityAction Categorizer()
    {
        if (rescueList[randomNumberList[plus]].name == "Cap")
        {
            string capText = "yeag";
            rText.text = capText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Denim")
        {
            string denimText = "*blushes* *shits* *runs away*";
            rText.text = denimText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Honey")
        {
            string honeyText = "poop fart";
            rText.text = honeyText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Jack")
        {
            string jackText = "HAHAHAHAHAHAHA (thousand yard stare)";
            rText.text = jackText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Jake")
        {
            string jakeText = "WHAT | Oh sorry | I don't know man, I guess I'd be dead?";
            rText.text = jakeText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Key")
        {
            string keyText = "If you were on your hands and knees and spat blood on the floor I would be so happy";
            rText.text = keyText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Libra")
        {
            string libraText = "You could have not said that, then I wouldn't have had to hear it";
            rText.text = libraText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Moyasi")
        {
            string moyasiText = "There's an unfuckable amount of references here";
            rText.text = moyasiText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Robin")
        {
            string robinText = "Jake! | fuck dude you don't have to shout | What would you do if I killed you?";
            rText.text = robinText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Ryan")
        {
            string ryanText = ">Certified ghostcoder for Rune R. Robin";
            rText.text = ryanText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Ryytikki")
        {
            string ryytikkiText = "smells like moron in here";
            rText.text = ryytikkiText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Shift")
        {
            string shiftText = "Moral support for when your jokes don't land";
            rText.text = shiftText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Sound")
        {
            string soundText = "See the joke is that Sound is usually quiet, but in this they got a big ass soundwave. Laugh Track.";
            rText.text = soundText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Tofu")
        {
            string tofuText = "Sigma Meal? Skibidi Slicers!!";
            rText.text = tofuText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Tri")
        {
            string triText = "I just- I- I don't know maaan ;_;";
            rText.text = triText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Tut")
        {
            string tutText = "[This Tut.exe model is deprecated, please use new 'Mag3.14' instead]";
            rText.text = tutText;
            return Rescue;
        }
        if (rescueList[randomNumberList[plus]].name == "Ushi")
        {
            string ushiText = "Do you ever just...shit? <3 <3";
            rText.text = ushiText;
            return Rescue;
        }
        return Rescue;
    }

    private void Rescue()
    {
        if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name == "Button")
        {
            plus--;
        }
        GameObject rescue = Resources.Load("Characters/" + rescueList[randomNumberList[plus]].name) as GameObject;
        rescue = Instantiate(rescue, PlayerMovement.instance.transform.position, PlayerMovement.instance.transform.rotation);
        rescue.name = rescueList[randomNumberList[plus]].name;
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
