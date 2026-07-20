using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    //public static List<GameObject> friends = new List<GameObject>();

    public GameObject[] friendArray = new GameObject[17];

    public GameObject[] spots = new GameObject[16]; //contains all 16 spots for non PCs

    Rigidbody2D body;

    float horizontal;
    float vertical;
    public int friendCount = 0;

    ////////////////CHARACTER BOOSTIES FOR YOUR BOOTIES////////////////

    public float tofuSupp = 1; //COUNT


    public float moyaSupp = 1.0f; //cooldown

    ////////////////SOYBEAN BOOSTIES FOR YOUR BOOTIES////////////////

    //////////////// PERMANENT UPGRADES ////////////////

    public int healthBoost;
    public int regenBoost;
    public int armourBoost;
    public int countBoost;
    public int bSpeedBoost;
    public int mSpeedBoost;
    public int sizeBoost;
    public int moneyBoost;
    public int expBoost;
    public int capacityBoost;
    public int luckBoost;
    public int damageBoost;

    public string characterToBecome;

    public int money;
    public int enemiesDefeated;

    //////////////// PERMANENT UPGRADES ////////////////

    #region Character Levels

    public static int andechLv = 1;
    public static int capLv = 1;
    public static int caddyLv = 1;
    public static int denimLv = 1;
    public static int jackLv = 1;
    public static int jacobLv = 1;
    public static int jakeLv = 1;
    public static int kestrylLv = 1;
    public static int keyLv = 1;
    public static int libraLv = 1;
    public static int moyasiLv = 1;
    public static int robinLv = 1;
    public static int ryanLv = 1;
    public static int soundLv = 1;
    public static int tofuLv = 1;
    public static int triLv = 1;
    public static int tutLv = 1;
    public static int ushiLv = 1;

    #endregion


    public float runSpeed = 1.0f;

    public float exp = 0;
    public float levelUp = 20;
    public Canvas canvas;

    public float pickUpRange = 1;
    public GameObject[] pickUps;

    private bool onOff = true;

    public PlayerMovement()
    {
        instance = this;
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadUpgrades();

        healthBoost = data.health;
        regenBoost = data.regen;
        armourBoost = data.armour;
        countBoost = data.count;
        bSpeedBoost = data.bSpeed;
        mSpeedBoost = data.mSpeed;
        sizeBoost = data.size;
        moneyBoost = data.money;
        expBoost = data.exp;
        capacityBoost = data.capacity;
        luckBoost = data.luck;
        damageBoost = data.damage;

        characterToBecome = data.selectedCharacterName;
        //money = data.moneyOnHand;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        LoadPlayer(); //load progress
    }

    void Update()
    {
        if(exp >= levelUp)
        {
            exp -= levelUp;
            levelUp *= 1.2f;
            canvas.gameObject.transform.Find("Choose Menu").gameObject.SetActive(true);
            //add code to make upgrade menu pop up
        }

        if(Input.GetKeyDown("q")) //temporary to see what the fuck is up my boy
        {
            canvas.gameObject.transform.Find("Choose Menu").gameObject.SetActive(onOff);
            onOff = !onOff;

        }

        if(Input.GetKeyDown("p"))
        {
            canvas.gameObject.transform.Find("Pause Menu").gameObject.SetActive(true);
        }

        if (Input.GetKeyDown("g"))
        {
            canvas.gameObject.transform.Find("Friend Grid Menu").gameObject.SetActive(true);
        }

        if(Input.GetKeyDown("l"))
        {
            SceneManager.LoadScene("Stage Over Scene");
        }

        int num = friendArray.Length;

        for (int i = 0; i < num; i++)
        {
            if (friendArray[i] != null && !friendArray[i].GetComponent<PlayerMovement>())
            {
                friendArray[i].GetComponent<Friend>().positionInCircle = Vector3.MoveTowards(friendArray[i].GetComponent<Friend>().transform.position, spots[i-1].gameObject.transform.position, friendArray[i].gameObject.GetComponent<Friend>().speed * Time.deltaTime);
            }//-1 on spots[] pos as player character will always be the first but should not be on the formation.
            
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        pickUps = GameObject.FindGameObjectsWithTag("PickUp");
        if (pickUps != null) //sets up pick ups to come towards the player
        {
            foreach(GameObject pickUp in pickUps)
            {
                if (Vector3.Distance(pickUp.transform.position, transform.position) <= pickUpRange && pickUp.GetComponent<Pickupable>().inRange == false)
                {
                    pickUp.GetComponent<Pickupable>().inRange = true;
                }
            }
        }

       GameObject.Find("Friend Placement Grid").transform.rotation = Quaternion.LookRotation(Vector3.forward, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position));

    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}