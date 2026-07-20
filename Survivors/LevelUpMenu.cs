using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelUpMenu : MonoBehaviour
{
    //public GameObject[] friendList = new GameObject[17];
    private List<GameObject> friendList;
    private List<int> randomNumberList;

    public Button firstOption;
    public Text firstDescription;
    public Image firstFriendImage;

    public Button secondOption;
    public Text secondDescription;
    public Image secondFriendImage;

    public Button thirdOption;
    public Text thirdDescription;
    public Image thirdFriendImage;

    public Button fourthOption;
    public Text fourthDescription;
    public Image fourthFriendImage;

    public Button fifthOption;
    public Text fifthDescription;
    public Image fifthFriendImage;

    public Text uText;

    public Canvas canvas;

    #region Upgrade Text
    public string andechUpgradeText;
    public string capUpgradeText;
    public string caddyUpgradeText;
    public string denimUpgradeText;
    public string honeyUpgradeText;
    public string jackUpgradeText;
    public string jacobUpgradeText;
    public string jakeUpgradeText;
    public string kestrylUpgradeText;
    public string keyUpgradeText;
    public string libraUpgradeText;
    public string moyasiUpgradeText;
    public string robinUpgradeText;
    public string ryanUpgradeText;
    public string ryytikkiUpgradeText;
    public string shiftUpgradeText;
    public string soundUpgradeText;
    public string tofuUpgradeText;
    public string triUpgradeText;
    public string tutUpgradeText;
    public string ushiUpgradeText;
    #endregion

    void Awake()
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

    // Start is called before the first frame update
    void OnEnable()
    {
        Time.timeScale = 0;//pauses game
        friendList = new List<GameObject>();

        foreach (GameObject friend in PlayerMovement.instance.friendArray)
        {
            if(friend != null && friend.GetComponent<Friend>().level <= 7)
            {
                friendList.Add(friend);
            }
            
        }        

        randomNumberList = RandomPick(friendList.Count, 0, friendList.Count); //picks numbers randomly to assign to the upgrade menu

        if (friendList.Count >= 1)
        {
            firstFriendImage.sprite = friendList[randomNumberList[0]].GetComponent<SpriteRenderer>().sprite;
            firstOption.onClick.RemoveAllListeners();
            uText = firstDescription;
            firstOption.onClick.AddListener(Categorizer());
            firstDescription = uText;
            randomNumberList.RemoveAt(0); //might no longer be necessary if List is dynamic can just use 1-5?
        }
        else //catches if there are not enough friends. At 0 it should not give you the option to click on new friend to begin with but deal with that later
        {
            firstFriendImage.sprite = null;
            firstOption.onClick.RemoveAllListeners();
            firstDescription.text = "No more upgrades here";
        }

        if (friendList.Count >= 2)
        {
            secondFriendImage.sprite = friendList[randomNumberList[0]].GetComponent<SpriteRenderer>().sprite;
            secondOption.onClick.RemoveAllListeners();
            uText = secondDescription;
            secondOption.onClick.AddListener(Categorizer());
            secondDescription = uText;
            randomNumberList.RemoveAt(0);
        }
        else
        {
            secondFriendImage.sprite = null;
            secondOption.onClick.RemoveAllListeners();
            secondDescription.text = "No more upgrades here";
        }

        if (friendList.Count >= 3)
        {
            thirdFriendImage.sprite = friendList[randomNumberList[0]].GetComponent<SpriteRenderer>().sprite;
            thirdOption.onClick.RemoveAllListeners();
            uText = thirdDescription;
            thirdOption.onClick.AddListener(Categorizer());
            thirdDescription = uText;
            randomNumberList.RemoveAt(0);
        }
        else
        {
            thirdFriendImage.sprite = null;
            thirdOption.onClick.RemoveAllListeners();
            thirdDescription.text = "No more upgrades here";
        }

        if (friendList.Count >= 4)
        {
            fourthFriendImage.sprite = friendList[randomNumberList[0]].GetComponent<SpriteRenderer>().sprite;
            fourthOption.onClick.RemoveAllListeners();
            uText = fourthDescription;
            fourthOption.onClick.AddListener(Categorizer());
            fourthDescription = uText;
            randomNumberList.RemoveAt(0);
        }
        else
        {
            fourthFriendImage.sprite = null;
            fourthOption.onClick.RemoveAllListeners();
            fourthDescription.text = "No more upgrades here";
        }

        if (friendList.Count >= 5)
        {
            fifthFriendImage.sprite = friendList[randomNumberList[0]].GetComponent<SpriteRenderer>().sprite;
            fifthOption.onClick.RemoveAllListeners();
            uText = fifthDescription;
            fifthOption.onClick.AddListener(Categorizer());
            fifthDescription = uText;
            randomNumberList.RemoveAt(0);
        }
        else
        {
            fifthFriendImage.sprite = null;
            fifthOption.onClick.RemoveAllListeners();
            fifthDescription.text = "No more upgrades here";
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1; //unpauses game
    }

    // Update is called once per frame
    void Update()
    {

    }

    private UnityAction Categorizer()
    {

        if (friendList[randomNumberList[0]].name == "Andech")
        {
            andechUpgradeText = "*sniffs* Ohhh kitten, you're so ripe for me";
            uText.text = andechUpgradeText;
            return Andech;
        }
        if (friendList[randomNumberList[0]].name == "Cap")
        {
            capUpgradeText = "yeag";
            uText.text = capUpgradeText;
            return Cap;
        }
        if (friendList[randomNumberList[0]].name == "Denim")
        {
            denimUpgradeText = "*blushes* *shits* *runs away*";
            uText.text = denimUpgradeText;
            return Denim;
        }
        if (friendList[randomNumberList[0]].name == "Honey")
        {
            honeyUpgradeText = "poop fart";
            uText.text = honeyUpgradeText;
            return Honey;
        }
        if (friendList[randomNumberList[0]].name == "Jack")
        {
            jackUpgradeText = "HAHAHAHAHAHAHA (thousand yard stare)";
            uText.text = jackUpgradeText;
            return Jack;
        }
        if (friendList[randomNumberList[0]].name == "Jacob")
        {
            jacobUpgradeText = "If this was my upgrade text, like...I would add something, like, really cool B-)";
            uText.text = jacobUpgradeText;
            return Jacob;
        }
        if (friendList[randomNumberList[0]].name == "Jake")
        {
            jakeUpgradeText = "WHAT | Oh sorry | I don't know man, I guess I'd be dead?";
            uText.text = jakeUpgradeText;
            return Jake;
        }
        if (friendList[randomNumberList[0]].name == "Kestryl")
        {
            kestrylUpgradeText = "I don't think you understand exactly how fucked up that situation would be";
            uText.text = kestrylUpgradeText;
            return Kestryl;
        }
        if (friendList[randomNumberList[0]].name == "Key")
        {
            keyUpgradeText = "If you were on your hands and knees and spat blood on the floor I would be so happy";
            uText.text = keyUpgradeText;
            return Key;
        }
        if (friendList[randomNumberList[0]].name == "Libra")
        {
            libraUpgradeText = "You could have not said that, then I wouldn't have had to hear it";
            uText.text = libraUpgradeText;
            return Libra;
        }
        if (friendList[randomNumberList[0]].name == "Moyasi")
        {
            moyasiUpgradeText = "There's an unfuckable amount of references here";
            uText.text = moyasiUpgradeText;
            return Moyasi;
        }
        if (friendList[randomNumberList[0]].name == "Robin")
        {
            robinUpgradeText = "Jake! | fuck dude you don't have to shout | What would you do if I killed you?";
            uText.text = robinUpgradeText;
            return Robin;
        }
        if (friendList[randomNumberList[0]].name == "Ryan")
        {
            ryanUpgradeText = ">Certified ghostcoder for Rune R. Robin";
            uText.text = ryanUpgradeText;
            return Ryan;
        }
        if (friendList[randomNumberList[0]].name == "Ryytikki")
        {
            ryytikkiUpgradeText = "smells like faggot in here";
            uText.text = ryytikkiUpgradeText;
            return Ryytikki;
        }
        if (friendList[randomNumberList[0]].name == "Shift")
        {
            shiftUpgradeText = "Moral support for when your jokes don't land";
            uText.text = shiftUpgradeText;
            return Shift;
        }
        if (friendList[randomNumberList[0]].name == "Sound")
        {
            soundUpgradeText = "See the joke is that Sound is usually quiet, but in this they got a big ass soundwave. Laugh Track.";
            uText.text = soundUpgradeText;
            return Sound;
        }
        if (friendList[randomNumberList[0]].name == "Tofu")
        {
            tofuUpgradeText = "Sigma Meal? Skibidi Slicers!!";
            uText.text = tofuUpgradeText;
            return Tofu;
        }
        if (friendList[randomNumberList[0]].name == "Tri")
        {
            triUpgradeText = "I just- I- I don't know maaan ;_;";
            uText.text = triUpgradeText;
            return Tri;
        }
        if (friendList[randomNumberList[0]].name == "Tut")
        {
            tutUpgradeText = "[This Tut.exe model is deprecated, please use new 'Mag3.14' instead]";
            uText.text = tutUpgradeText;
            return Tut;
        }
        if (friendList[randomNumberList[0]].name == "Ushi")
        {
            ushiUpgradeText = "Do you ever just...shit? <3 <3";
            uText.text = ushiUpgradeText;
            return Ushi;
        }
        return Categorizer();
    }

    #region Andech Upgrades
    private void Andech()
    {
        Debug.Log("Andech!");

        if (GameObject.Find("Andech").GetComponent<Friend>().level == 1)
        {
            GameObject.Find("Andech").GetComponent<Friend>().level++;
            gameObject.SetActive(false);
        }
        else if (GameObject.Find("Andech").GetComponent<Friend>().level == 2)
        {
            GameObject.Find("Andech").GetComponent<Friend>().level++;
            gameObject.SetActive(false);
        }
        else if (GameObject.Find("Andech").GetComponent<Friend>().level == 3)
        {
            GameObject.Find("Andech").GetComponent<Friend>().level++;
            gameObject.SetActive(false);
        }
        else if (GameObject.Find("Andech").GetComponent<Friend>().level == 4)
        {
            GameObject.Find("Andech").GetComponent<Friend>().level++;
            gameObject.SetActive(false);
        }
        else if (GameObject.Find("Andech").GetComponent<Friend>().level == 5)
        {
            GameObject.Find("Andech").GetComponent<Friend>().level++;
            gameObject.SetActive(false);
        }
        else if (GameObject.Find("Andech").GetComponent<Friend>().level == 6)
        {
            GameObject.Find("Andech").GetComponent<Friend>().level++;
            gameObject.SetActive(false);
        }
        else if (GameObject.Find("Andech").GetComponent<Friend>().level == 7)
        {
            GameObject.Find("Andech").GetComponent<Friend>().level++;
            gameObject.SetActive(false);
        }
    }
    #endregion
    //Guardian

    #region Cap Upgrades
    private void Cap()
    {
        Debug.Log("Cap!");
        GameObject cap = GameObject.Find("Cap");
        cap.GetComponent<Friend>().level++;
        cap.GetComponent<Cap>().OnLevelUp();

        gameObject.SetActive(false);

    }
    #endregion

    #region Denim Upgrades
    private void Denim()
    {
        Debug.Log("Denim!");

        GameObject denim = GameObject.Find("Denim");
        denim.GetComponent<Friend>().level++;
        denim.GetComponent<Denim>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Honey Upgrades
    private void Honey()
    {
        Debug.Log("Honey!");

        GameObject honey = GameObject.Find("Honey");
        honey.GetComponent<Friend>().level++;
        honey.GetComponent<Honey>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Jack Upgrades
    private void Jack()
    {
        Debug.Log("JAck!");

        GameObject jack = GameObject.Find("Jack");
        jack.GetComponent<Friend>().level++;
        jack.GetComponent<Jack>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Jacob Upgrades
    private void Jacob()
    {
        Debug.Log("JAcob!");

    }
    #endregion
    //Guardian

    #region Jake Upgrades
    private void Jake()
    {
        Debug.Log("JAke!");

        GameObject jake = GameObject.Find("Jake");
        jake.GetComponent<Friend>().level++;
        jake.GetComponent<Jake>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Kestryl Upgrades
    private void Kestryl()
    {
        Debug.Log("Kestryl!");

    }
    #endregion
    //Guardian

    #region Key Upgrades
    private void Key()
    {
        Debug.Log("Key!");

        GameObject key = GameObject.Find("Key");
        key.GetComponent<Friend>().level++;
        key.GetComponent<KeyRat>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Libra Upgrades
    private void Libra()
    {
        Debug.Log("Libra!");

        GameObject libra = GameObject.Find("Libra");
        libra.GetComponent<Friend>().level++;
        libra.GetComponent<Libra>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Moyasi Upgrades
    private void Moyasi()
    {
        Debug.Log("Moyasi!");

        GameObject moyasi = GameObject.Find("Moyasi");
        moyasi.GetComponent<Friend>().level++;
        moyasi.GetComponent<Moyasi>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Robin Upgrades
    private void Robin()
    {
        Debug.Log("Me :)");

        GameObject robin = GameObject.Find("Robin");
        robin.GetComponent<Friend>().level++;
        robin.GetComponent<Robin>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Ryan Upgrades
    private void Ryan()
    {
        Debug.Log("Ryan!");

        GameObject ryan = GameObject.Find("Ryan");
        ryan.GetComponent<Friend>().level++;
        ryan.GetComponent<Ryan>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Ryytikki Upgrades
    private void Ryytikki()
    {
        Debug.Log("Ryytikki!");

        GameObject ryy = GameObject.Find("Ryytikki");
        ryy.GetComponent<Friend>().level++;
        ryy.GetComponent<Ryytikki>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Shift Upgrades
    private void Shift()
    {
        Debug.Log("Shift!");

        GameObject shift = GameObject.Find("Shift");
        shift.GetComponent<Friend>().level++;
        shift.GetComponent<Shift>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Sound Upgrades
    private void Sound()
    {
        Debug.Log("Sound!");

        GameObject sound = GameObject.Find("Sound");
        sound.GetComponent<Friend>().level++;
        sound.GetComponent<Sound>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Tofu Upgrades
    private void Tofu()
    {
        Debug.Log("Tofu!");

        GameObject tofu = GameObject.Find("Tofu");
        tofu.GetComponent<Friend>().level++;
        tofu.GetComponent<Tofu>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Tri Upgrades
    private void Tri()
    {
        Debug.Log("Tri!");

        GameObject tri = GameObject.Find("Tri");
        tri.GetComponent<Friend>().level++;
        tri.GetComponent<Tri>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Tut Upgrades
    private void Tut()
    {
        Debug.Log("Tut!");

        GameObject tut = GameObject.Find("Tut");
        tut.GetComponent<Friend>().level++;
        tut.GetComponent<Tut>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion

    #region Ushi Upgrades
    private void Ushi()
    {
        Debug.Log("Ushi!");

        GameObject ushi = GameObject.Find("Ushi");
        ushi.GetComponent<Friend>().level++;
        ushi.GetComponent<Jemma>().OnLevelUp();

        gameObject.SetActive(false);
    }
    #endregion
}
