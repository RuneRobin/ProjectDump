using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{
    [Header("Guy1: Jacob")]
    //Jacob. Moves around fast room per room, catch him.
    public bool guy1Escaped = true;
    public float guy1Interval = 80f;
    public float guy1Timer;
    public float guy1BonusDrain = 0;
    private bool[] guy1ScreenLocations = { false, false, false };
    public List<GameObject> guy1ByteGameObjects = new List<GameObject>();
    public List<Sprite> guy1FaceSprites = new List<Sprite>();

    [Header("Guy2: Shift")]
    //Shift. Help him take pictures.
    public int guy2Location = 7;
    public float guy2Interval = 10f;
    public float guy2Timer;
    private bool guy2Arrive = false;
    public float guy2Satisfaction = 0;
    public int guy2Disatisfaction = 0;
    public GameObject flash;
    private Color flashValues;
    [HideInInspector] public List<int> roomsNotPhotographedYet = new List<int>();

    [Header("Guy 3: Denim")]
    //Denim. Snooze her alarm or game over
    public int guy3NoOfAlarms;
    private List<int> guy3AllHours = new List<int>();
    private List<int> guy3Alarms = new List<int>();
    public float guy3GracePeriod;
    public Text guy3HourDisplay;
    private bool guy3AlarmPlaying = false;
    public GameObject guy3AlarmGameObject;
    public GameObject guy3GameObject;

    [Header("Guy 4: Jack")]
    public List<GameObject> guy4Apparel = new List<GameObject>();
    public List<GameObject> guy4ScreenFeatures = new List<GameObject>();
    private bool guy4ShutUp = false;
    private GameObject guy4CurrentMouth;
    public List<AudioClip> guy4Voicemails = new List<AudioClip>();
    private bool guy4VoicemailIsPlaying = false;
    private float guy4VoicemailDuration;

    [Header("Guy 5: Sound")]
    public int guy5Location = 8;
    public float guy5Interval = 30;
    public float guy5Timer;
    public int guy5PlugsSiphon = 0;
    public GameObject guy5DoorPeak;
    private float guy5AmbientZippers;
    public List<GameObject> guy5Plugs = new List<GameObject>();

    [Header("Guy 6: Captain")]
    public float guy6Interval = 30;
    public float guy6Timer;
    public GameObject guy6GameObject;
    public GameObject guy6Boat;
    private float guy6StartPoint;
    private bool guy6Danger = false;

    [Header("Guy 7: Kestryl")]
    public float guy7Interval;
    public float guy7Timer;
    public bool guy7BehindYou = false;
    public float guy7HugTime = 10;
    public GameObject guy7GameObject;
    public GameObject guy7Start;
    public GameObject guy7End;

    [Header("Guy 8: Honey")]
    public float guy8Interval = 66;
    public float guy8Timer;
    public int guy8HowManyMissing = 3;
    public List<int> guy8ShoppingList = new List<int>();
    public GameObject guy8CurrFoodDisplayer;
    public List<Sprite> guy8FoodButtons;
    private int guy8currButton = 0;
    public float guy8OrderingCooldown = 0;
    public GameObject guy8OfficePCButtons;

    [Header("Guy 9: Andech")]
    public float guy9Interval = 60;
    public float guy9Timer;
    public int guy9CurrRoom = 5;
    public GameObject guy9DoorPeak;
    public bool guy9InsideOffice = false;
    public GameObject guy9InsideOfficeGameObject;
    private float guy9InsideOfficeEndPos;

    [Header("Guy 10: RyanV")]
    public int guy10Patience;
    public int guy10TimesPissedOff = 0;
    public GameObject guy10GameObject;
    public GameObject guy10Disclaimer;

    [Header("Guy 11: Ryytikki")]
    public GameObject guy11GameObject;
    public List<GameObject> guy11PlantGameObjects = new List<GameObject>();
    private List<float> guy11Moisture = new List<float>(); //90
    public List<bool> guy11Hovering = new List<bool>(); //false
    private List<float> guy11WilterRate = new List<float>(); //0.001f
    private List<int> guy11WilterLevel = new List<int>(); //0
    private List<float> guy11WilterMultiTimer = new List<float>(); //10f
    private List<float> guy11WilterBonusWilter = new List<float>(); //0;
    private List<GameObject> guy11WilterLevelGameObject = new List<GameObject>();

    [Header("Guy 12: Tri")]
    public float guy12Interval = 120;
    public float guy12Timer;
    public bool guy12ScrunglyEscaped = false;
    public int guy12ScrunglyEscapeAttempts;
    public int guy12ScrunglyLocation;
    public List<GameObject> guy12GameObjects = new List<GameObject>();

    [Header("Guy 13: Tofu")]
    public float guy13Interval = 60;
    public float guy13Timer;
    public int guy13Needs;
    public int guy13CooldownMethod;
    public int guy13Burns;
    public bool guy13InNeed = false;
    public List<GameObject> guy13Expressions = new List<GameObject>();

    [Header("Guy 14: Key")]
    public float guy14Interval = 60;
    public float guy14Timer;
    public float guy14Patience;
    public int guy14MusicTaste;
    public int guy14CurrentTrack;
    public int guy14CamerasToFuckUp = 2;

    private bool[] guy14MalfCameras = { false, false, false, false, false, false, false, false, false, false };
    private float[] guy14CameraMalfTimers = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    private bool guy14allCamerasFucked = false;

    public GameObject screenStatic;
    private Color screenStaticValues;

    private float guy14AmbientSqueaks;

    [Header("Guy 15: Moyasi")]
    public float guy15Mood;
    public float guy15HeadpatOverload = 0;
    public GameObject guy15GameObject;
    public GameObject guy15MoodBar;
    private int guy15CurrentMood; //0 = low. 1 = normal. 2 = high.
    
    [Header("Guy 16: Jake")]
    public float guy16Timer;
    public GameObject guy16AllSprites;
    private float guy16AmbientPops;

    [Header("Guy 17: Tut")]
    public float guy17Interval = 60;
    public float guy17Timer;
    public int guy17FeathersLeft;
    public float guy17ChanceOfFeathers;
    private bool guy17IsHere = false;
    public GameObject guy17FixItButton;

    [Header("Guy 18: Libra")]
    public float guy18FortuneCooldown = 0;
    private bool guy18FortuneReady = false;
    public int guy18FortuneRequirement;
    public GameObject guy18GameObject;
    public GameObject guy18MessagePrefab;
    private GameObject guy18Message;

    [Header("Guy 19: Rune")]
    public float guy19Timer;
    public bool guy19GloveEscaped = false;
    public float guy19TooCloseTimer = 5;
    public float guy19TooCloseDistance = 10;
    public int guy19GlovesCaughtRequirement;

    public List<GameObject> guy19AllGloves = new List<GameObject>();
    public List<Sprite> guy19GloveSprites = new List<Sprite>();

    [Header("Rooms/Extras")]
    public float shiftTime = 540;
    public float shiftEnd = 1020;
    public Text timeText;

    public float power = 100f;
    public Text powerText;
    public int powerEfficiency = 10;

    //public int currentRoomInCamera = 7;

    public GameObject ScreenGameObject; //to find the guys inside the screens | ScreenGameObject.GetComponent<ScreenPopUp>().gameObject.transform
    public GameObject popUpButton;

    public static RoomController instance;

    public int[] allDiffs = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //guys difficulty
    public bool[] allGuysActive = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false}; //guys active

    //0 -> 1
    //1 -> 3,5 (to 3 is one-way, slide)
    //2 -> 3,4,5,7
    //3 -> 2,4
    //4 -> 2,3,8
    //5 -> 1,2,6
    //6 -> 5,[9],7
    //7 -> 2,6,8
    //8 -> 4,[9],7

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        allDiffs = Master.instance.masterDiffs;

        for(int i = 0; i < allDiffs.Length; i++)
        {
            if(allDiffs[i] > 0)
            {
                allGuysActive[i] = true;
            }
        }

        //guy1
        if (allGuysActive[0] == true)
        {
            guy1Timer = Random.Range(guy1Interval, guy1Interval * 2);
        }

        //guy2
        if (allGuysActive[1] == true)
        {
            guy2Timer = Random.Range(60f, 360f); //10am-3pm
            guy2Satisfaction = (70f / 19f) * (20f - allDiffs[1]); //diff 1 = 70%. diff 20 = 0%
        }

        //guy3
        if (allGuysActive[2] == true)
        {
            guy3GracePeriod = 1f + (9f / 19f) * (20f - allDiffs[2]); //diff1 = 10 seconds. diff20 = 1 second
            guy3NoOfAlarms = Mathf.FloorToInt(8f / 20f * allDiffs[2]) + 1; //diff 1 = 1 alarm. diff 20 = 9 alarms
            for (int time = 570; time <= 960; time += 30)
            {
                guy3AllHours.Add(time);
            }
            for (int i = 0; i < guy3NoOfAlarms; i++)
            {
                guy3Alarms.Add(guy3AllHours[Random.Range(0, guy3AllHours.Count - 1)]);
                guy3AllHours.Remove(guy3Alarms[guy3Alarms.Count - 1]);
            }
            guy3Alarms.Sort();

            int min = Mathf.FloorToInt(guy3Alarms[0] / 60);
            int sec = Mathf.FloorToInt(guy3Alarms[0] % 60);
            guy3HourDisplay.text = min.ToString("00") + ":" + sec.ToString("00");
        }

        //guy4
        if (allGuysActive[3] == true)
        {
            //start recording?
            
            for (int i = 0; i < guy4Apparel.Count; i++)
            {
                guy4Apparel[i].SetActive(true);
            }

            SoundManager.instance.PlaySound(SoundType.GUY4, 16, 1);
            guy4CurrentMouth = guy4ScreenFeatures[0];
            StartCoroutine(MouthChanger());

            guy4VoicemailDuration = 480 / 20 * allDiffs[3]; //24 seconds at diff 1. 8 minutes at diff 20

        }

        //guy5
        if (allGuysActive[4] == true)
        {
            guy5Timer = Random.Range(guy5Interval / 2, guy5Interval * 2);
            guy5AmbientZippers = Random.Range(1f, 5f);
        }

        //guy6
        if (allGuysActive[5] == true)
        {
            guy6Timer = guy6Interval;
            guy6StartPoint = guy6GameObject.transform.localPosition.y;
        }

        //guy7
        if (allGuysActive[6] == true)
        {
            guy7Interval = Random.Range(50, 80) - allDiffs[6];
            guy7Timer = 0;
            guy7HugTime = 15f - (10f / 19f) * (20f - allDiffs[6]); //diff1 = 5 seconds. diff20 = 15 seconds
        }

        //guy8
        if (allGuysActive[7] == true)
        {
            guy8Interval = 66f;
            guy8Timer = Random.Range(guy8Interval, guy8Interval * 2);
            foreach (GameObject g in Resources.LoadAll("Guy8Mustard/FoodButtons/Sprites"))
            {
                guy8FoodButtons.Add(g.GetComponent<SpriteRenderer>().sprite);
            }
            guy8HowManyMissing = 9 - Mathf.RoundToInt((6f / 19f) * (20f - allDiffs[7])); //diff1 = 3 items. diff20 = 9 items
            guy8OfficePCButtons.SetActive(true);
        }

        //guy9
        if (allGuysActive[8] == true)
        {
            guy9Timer = guy9Interval;
        }

        //guy10
        if (allGuysActive[9] == true)
        {
            guy10Patience = Mathf.CeilToInt(1f + (4f / 19f) * (20f - allDiffs[9])); //diff1 = 5 patience. diff20 = 1 patience
        }

        //guy11
        if (allGuysActive[10] == true)
        {
            for (int i = 0; i < guy11PlantGameObjects.Count; i++)
            {
                guy11Moisture.Add(90 - Random.Range(0f, 15f));
                guy11WilterRate.Add(0.001f);
                guy11WilterLevel.Add(0);
                guy11WilterMultiTimer.Add(10f);
                guy11WilterBonusWilter.Add(0);
                guy11WilterLevelGameObject.Add(guy11PlantGameObjects[i].transform.GetChild(0).transform.gameObject);
            }

            guy11GameObject.GetComponent<AudioSource>().Stop();//so it doesn't clip the first time for whatever reason
        }

        //guy12
        if (allGuysActive[11] == true)
        {
            guy12Timer = Random.Range(guy12Interval / 2, guy12Interval * 2);
        }

        //guy13
        if (allGuysActive[12] == true)
        {
            guy13Timer = Random.Range(guy13Interval / 2, guy13Interval);
            guy13Burns = 2;
            if (allDiffs[12] >= 10)
            {
                guy13Burns--;

                if (allDiffs[12] >= 20)
                {
                    guy13Burns--;
                }
            }
        }

        //guy14
        if (allGuysActive[13] == true)
        {
            guy14Timer = Random.Range(guy14Interval / 2, guy14Interval * 2);
            guy14Patience = Mathf.Clamp(guy14Interval / 2 - allDiffs[13], 12, 999);

            for (int i = 0; i < guy14CameraMalfTimers.Length; i++)
            {
                guy14CameraMalfTimers[i] = Random.Range(5f, 20f);
            }

            guy14AmbientSqueaks = Random.Range(1f, 3f);
        }

        //guy15
        if (allGuysActive[14] == true)
        {
            guy15Mood = 60 + (90f / 19f * (20f - allDiffs[14])); //diff1 = 150 starting mood. diff20 = 60 starting mood.
            guy15MoodBar.SetActive(true);
        }

        //guy16
        if (allGuysActive[15] == true)
        {
            guy16Timer = Random.Range(5f, 10f + (19f / 20f * (20f - allDiffs[15])));
            guy16AmbientPops = Random.Range(0f, 3f);
        }

        //guy17
        if (allGuysActive[16] == true)
        {
            guy17ChanceOfFeathers = 10 + (10f / 19f * (20f - allDiffs[16])); //diff1 = 20%. diff20 = 10%
            guy17FeathersLeft = 5;
        }

        //guy18
        if (allGuysActive[17] == true)
        {
            guy18FortuneRequirement = 1 + Mathf.CeilToInt(allDiffs[17] / 10);
        }

        //guy19
        if (allGuysActive[18] == true)
        {
            guy19Timer = Random.Range(870f, 990f); //somewhere between 2:30PM and 4:30PM
            guy19TooCloseTimer = 1.5f + (3.5f / 19f * (20f - allDiffs[18])); //diff1 = 5 seconds. diff20 = 1.5 seconds.
            guy19TooCloseDistance = 5 + (0.5f * allDiffs[18]); //diff1 = 5 distance. diff20 = 15 distance
            guy19GlovesCaughtRequirement = 2 + Mathf.FloorToInt(6f / 19f * allDiffs[18]); //diff1 = 2 catches. diff20 = 8 catches.
        }
}

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
            Master.instance.totalScore = 0;
            SceneManager.LoadScene("Main Menu");
            Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
        }
        
        #region Timer
        shiftTime += Time.deltaTime;
        int min = Mathf.FloorToInt(shiftTime / 60);
        int sec = Mathf.FloorToInt(shiftTime % 60);
        timeText.text = min.ToString("00") + ":" + sec.ToString("00");

        if(shiftTime >= shiftEnd)
        {
            if((allGuysActive[17] == true && guy18FortuneRequirement <= 0) || allGuysActive[17] == false)
            {
                Debug.Log("Win");
                Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("Success");
            }
            else
            {
                Master.instance.loseText = "You did not appease Libra and have been erased, Game Over";
                Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("Game Over");
            }
        }
        #endregion

        #region Power
        power -= Time.deltaTime / (Mathf.Clamp(powerEfficiency,1,999) - guy1BonusDrain);
        powerText.text = "Power: " + power.ToString("F1");

        if(power <= 0)
        {
            Master.instance.loseText = "Dude what? You're not supposed to run out of power I added it as a joke, are you good?";
            Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
            SceneManager.LoadScene("Game Over");
        }
        #endregion

        #region guy1
        if (allGuysActive[0] == true)
        {
            if(guy1Escaped == false)
            {
                guy1Timer -= Time.deltaTime;
            }

            if (guy1Timer <= 0 && guy1Escaped == false)
            {
                guy1Escaped = true;
                SoundManager.instance.PlaySound(SoundType.GUY1, 3, 1 * Master.instance.soundLevel/100); //plays goblin laugh
                for (int i = 0; i < guy1ScreenLocations.Length; i++)
                {
                    guy1ScreenLocations[i] = true;
                    guy1ByteGameObjects[i].SetActive(true);
                    guy1ByteGameObjects[i].transform.GetChild(0).transform.GetChild(0).transform.gameObject.GetComponent<Image>().sprite = guy1FaceSprites[Random.Range(0, guy1FaceSprites.Count)];
                }
            }

            if (guy1Escaped == true && guy1BonusDrain == 0)
            {
                guy1BonusDrain = 0.2f * allDiffs[0];
            }
        }
        #endregion

        #region guy2
        if (allGuysActive[1] == true)
        {
            guy2Timer -= Time.deltaTime;

            //What makes the sprite show up in the cameras
            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == guy2Location && ScreenGameObject.transform.Find("Room" + guy2Location + "Sprites").Find("guy2").transform.gameObject.activeInHierarchy == false && guy2Arrive == true)
            {
                ScreenGameObject.transform.Find("Room" + guy2Location + "Sprites").Find("guy2").transform.gameObject.SetActive(true);
            }
            else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != guy2Location && ScreenGameObject.transform.Find("Room" + guy2Location + "Sprites").Find("guy2").transform.gameObject.activeInHierarchy == true && guy2Arrive == true)
            {
                ScreenGameObject.transform.Find("Room" + guy2Location + "Sprites").Find("guy2").transform.gameObject.SetActive(false);
            }

            if (guy2Timer <= 0 && guy2Arrive == false)
            {
                guy2Arrive = true;
                guy2Timer = guy2Interval;
                SoundManager.instance.PlaySound(SoundType.GUY2, 0, 1 * 1); //plays bell jingle
            }
            else if (guy2Timer <= 0 && guy2Arrive == true)
            {
                guy2Disatisfaction++;
                SoundManager.instance.PlaySound(SoundType.GUY2, 2, 1 * 1); //plays dissatisfaction noise
                ScreenGameObject.transform.Find("Room" + guy2Location + "Sprites").Find("guy2").transform.gameObject.SetActive(false);
                guy2Location = roomsNotPhotographedYet[Random.Range(0, roomsNotPhotographedYet.Count - 1)];
                guy2Timer = guy2Interval;
            }
            if (guy2Disatisfaction >= 3 - Mathf.FloorToInt(allDiffs[1]/10))
            {
                Master.instance.loseText = "Shift is kinda disappointed in you and no longer wants to be friends, Game Over";
                Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("Game Over");
            }
        }
        #endregion

        #region guy3
        if (allGuysActive[2] == true)
        {
            if (guy3Alarms.Count > 0 && shiftTime >= guy3Alarms[0] + guy3GracePeriod) //reacted too late
            {
                Master.instance.loseText = "You let the alarms denim set for herself to wake up to wake her up, how could you, Game Over";
                Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("Game Over");
            }

            #region audio sources
            if(shiftTime >= guy3Alarms[0] && shiftTime <= guy3Alarms[0] + guy3GracePeriod && guy3AlarmPlaying == false)
            {
                guy3AlarmPlaying = true;
                guy3AlarmGameObject.GetComponent<AudioSource>().Play();
            }
            #endregion

            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 2 && ScreenGameObject.transform.Find("Room" + 2 + "Sprites").Find("guy3").transform.gameObject.activeInHierarchy == false)
            {
                ScreenGameObject.transform.Find("Room" + 2 + "Sprites").Find("guy3").transform.gameObject.SetActive(true);
            }
            else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != 2 && ScreenGameObject.transform.Find("Room" + 2 + "Sprites").Find("guy3").transform.gameObject.activeInHierarchy == true)
            {
                ScreenGameObject.transform.Find("Room" + 2 + "Sprites").Find("guy3").transform.gameObject.SetActive(false);
            }
        }
        #endregion

        #region guy4
        if(allGuysActive[3] == true)
        {
            if(guy4VoicemailDuration > 0)
            {
                guy4VoicemailDuration -= Time.deltaTime;
            }
            else
            {
                if (guy4ShutUp == false)
                {
                    SomethingCameUp();
                    guy4ShutUp = true;
                }
            }
            
            if(guy4ShutUp == true && guy4Apparel[1].transform.position.x < -0.5f) //move mute button right
            {
                guy4Apparel[1].transform.position += new Vector3(Time.deltaTime,0);
            }
            if(guy4ShutUp == true && guy4Apparel[1].transform.position.y < 2 && guy4Apparel[1].transform.position.x >= -0.5f) //move mute button up after it has moved right
            {
                guy4Apparel[1].transform.position += new Vector3(0,Time.deltaTime);
            }
        }
        #endregion

        #region guy5
        if (allGuysActive[4] == true)
        {
            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == guy5Location && ScreenGameObject.transform.Find("Room" + guy5Location + "Sprites").Find("guy5").transform.gameObject.activeInHierarchy == false)
            {
                ScreenGameObject.transform.Find("Room" + guy5Location + "Sprites").Find("guy5").transform.gameObject.SetActive(true);
            }
            else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != guy5Location && ScreenGameObject.transform.Find("Room" + guy5Location + "Sprites").Find("guy5").transform.gameObject.activeInHierarchy == true)
            {
                ScreenGameObject.transform.Find("Room" + guy5Location + "Sprites").Find("guy5").transform.gameObject.SetActive(false);
            }

            if(guy5Location == 8 && ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 8 && ScreenGameObject.activeInHierarchy == true)
            {
                guy5AmbientZippers -= Time.deltaTime;
                if(guy5AmbientZippers <= 0)
                {
                    SoundManager.instance.PlaySound(SoundType.GUY5, Random.Range(3, 6), 1 * 1);
                    guy5AmbientZippers = Random.Range(1f, 5f);
                }
            }

            if (guy5PlugsSiphon < 3)
            {
                guy5Timer -= Time.deltaTime;

                if (guy5Timer <= 0)
                {
                    if (guy5Location == 8)
                    {
                        guy5Location = 9;
                        guy5Timer = 3 + ((9f / 19f) * (20f - allDiffs[4]));//diff 1 = 12 seconds. diff 20 = 3 seconds
                        ScreenGameObject.transform.Find("Room8Sprites").Find("guy5").transform.gameObject.SetActive(false);
                        guy5DoorPeak.SetActive(true);
                        SoundManager.instance.PlaySound(SoundType.GUY5, 2, 1 * 1); //plays hurry zipper
                    }
                    else if (guy5Location == 9)
                    {
                        guy5Plugs[guy5PlugsSiphon].SetActive(true);
                        guy5PlugsSiphon++;
                        guy5Location = 8;
                        guy5Timer = Random.Range(guy5Interval / 2, guy5Interval * 2);
                        ScreenGameObject.transform.Find("Room9Sprites").Find("guy5").transform.gameObject.SetActive(false);
                        guy5DoorPeak.SetActive(false);
                        powerEfficiency--;
                        SoundManager.instance.PlaySound(SoundType.GUY5, 0, 1 * 1); //plays breaker switch.
                    }
                }
            }
        }


        #endregion

        #region guy6
        if (allGuysActive[5] == true)
        {
            guy6Timer -= Time.deltaTime * ((float)allDiffs[5]/20f);

            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 4 && ScreenGameObject.transform.Find("Room4Sprites").Find("guy6").transform.gameObject.activeInHierarchy == false)
            {
                ScreenGameObject.transform.Find("Room4Sprites").Find("guy6").transform.gameObject.SetActive(true);
            }
            else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != 4 && ScreenGameObject.transform.Find("Room4Sprites").Find("guy6").transform.gameObject.activeInHierarchy == true)
            {
                ScreenGameObject.transform.Find("Room4Sprites").Find("guy6").transform.gameObject.SetActive(false);
            }

            if(guy6Timer <= guy6Interval/4 && guy6Danger == false) //plays elephant sound if too low
            {
                SoundManager.instance.PlaySound(SoundType.GUY6, 5, 1 * 1);
                guy6Danger = true;
            }

            if(guy6Timer > guy6Interval/2 && guy6Danger == true) //resets elephant sound if high enough
            {
                guy6Danger = false;
            }

            if (guy6Timer > 0)
            {
                guy6GameObject.transform.localPosition = new Vector2(guy6GameObject.transform.localPosition.x, guy6StartPoint + (guy6Timer * 5));
            }
            else
            {
                Master.instance.loseText = "Captain drowned, why would you let this happen, Game Over";
                Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("Game Over");
            }
        }
        #endregion

        #region guy7
        if (allGuysActive[6] == true)
        {
            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != 9 || ScreenGameObject.activeInHierarchy == false)
            {
                guy7Timer += Time.deltaTime / guy7Interval;
            }
            guy7GameObject.transform.position = Vector3.Lerp(guy7Start.transform.position, guy7End.transform.position, guy7Timer);


            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 9 && guy7GameObject.transform.gameObject.activeInHierarchy == false)
            {
                guy7GameObject.transform.gameObject.SetActive(true);
            }
            else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != 9 && guy7GameObject.transform.gameObject.activeInHierarchy == true)
            {
                guy7GameObject.transform.gameObject.SetActive(false);
            }

            if (guy7Timer >= 1 && guy7BehindYou == false)
            {
                guy7BehindYou = true;
                ScreenGameObject.gameObject.SetActive(false);
                SoundManager.instance.PlaySound(SoundType.GUY7, 2, 1); //plays hug noise
            }
            if (guy7BehindYou == true)
            {
                guy7HugTime -= Time.deltaTime;
            }
            if (guy7HugTime <= 0)
            {
                guy7BehindYou = false;
                guy7Timer = 0;
                guy7Interval = Random.Range(50, 80) - allDiffs[6];
                guy7HugTime = 15f - (10f / 19f) * (20f - allDiffs[6]); //diff1 = 5 seconds. diff20 = 15 seconds
            }
        }
        #endregion

        #region guy8
        if (allGuysActive[7] == true)
        {
            guy8Timer -= Time.deltaTime;

            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 6 && ScreenGameObject.transform.Find("Room6Sprites").Find("guy8").transform.gameObject.activeInHierarchy == false)
            {
                ScreenGameObject.transform.Find("Room6Sprites").Find("guy8").transform.gameObject.SetActive(true);
            }
            else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != 6 && ScreenGameObject.transform.Find("Room6Sprites").Find("guy8").transform.gameObject.activeInHierarchy == true)
            {
                ScreenGameObject.transform.Find("Room6Sprites").Find("guy8").transform.gameObject.SetActive(false);
            }
            

            if (guy8Timer <= 0)
            {
                SoundManager.instance.PlaySound(SoundType.GUY8, 0, 1); //plays stomach grumbling

                if (guy8ShoppingList.Count == 0)
                {
                    CreateShoppingList();
                    guy8Timer = guy8Interval;
                }
                else
                {
                    Master.instance.loseText = "Robin was reaaaaaally looking forward to that sandwich, now he'll have to wait like, 15 minutes for delivery thanks to you, Game Over";
                    Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                    SceneManager.LoadScene("Game Over");
                }
            }

            if (guy8OrderingCooldown > 0)
            {
                guy8OrderingCooldown -= Time.deltaTime;
            }
        }
        #endregion

        #region guy9
        if (allGuysActive[8] == true)
        {
            guy9Timer -= Time.deltaTime;

            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == guy9CurrRoom && ScreenGameObject.transform.Find("Room" + guy9CurrRoom + "Sprites").Find("guy9").transform.gameObject.activeInHierarchy == false)
            {
                ScreenGameObject.transform.Find("Room" + guy9CurrRoom + "Sprites").Find("guy9").transform.gameObject.SetActive(true);
            }
            else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != guy9CurrRoom && ScreenGameObject.transform.Find("Room" + guy9CurrRoom + "Sprites").Find("guy9").transform.gameObject.activeInHierarchy == true)
            {
                ScreenGameObject.transform.Find("Room" + guy9CurrRoom + "Sprites").Find("guy9").transform.gameObject.SetActive(false);
            }

            if (guy9Timer <= 0 && guy9InsideOffice == false)
            {
                if (guy9CurrRoom == 5)
                {
                    ScreenGameObject.transform.Find("Room" + guy9CurrRoom + "Sprites").Find("guy9").transform.gameObject.SetActive(false);
                    guy9CurrRoom = 6;
                    guy9Timer = 1.5f;
                }
                else if (guy9CurrRoom == 6)
                {
                    ScreenGameObject.transform.Find("Room" + guy9CurrRoom + "Sprites").Find("guy9").transform.gameObject.SetActive(false);
                    guy9CurrRoom = 9;
                    guy9Timer = 3f + ((7f / 19f) * (20f - allDiffs[8])); //diff1 = 10 seconds. diff20 = 1 second
                    guy9DoorPeak.SetActive(true);
                    SoundManager.instance.PlaySound(SoundType.GUY9, 2, 1);
                }
                else
                {
                    if (Random.Range(0, 100) <= 1) //2% chance nothing happens lmao fellas gottem
                    {
                        SendAndechBack();
                    }
                    else
                    {
                        guy9InsideOfficeGameObject.SetActive(true);
                        guy9DoorPeak.SetActive(false);
                        guy9InsideOffice = true;
                        guy9InsideOfficeEndPos = guy9InsideOfficeGameObject.transform.position.y;
                        guy9InsideOfficeGameObject.transform.position = new Vector3(0, -25, 0);

                    }
                }

            }

            if (guy9InsideOffice == true)
            {
                if (popUpButton.activeInHierarchy == true) //constantly creeping up
                {
                    guy9InsideOfficeGameObject.transform.position += new Vector3(0, Time.deltaTime * (1+(allDiffs[8]/10)), 0);
                }

                if (guy9InsideOfficeGameObject.transform.position.y >= guy9InsideOfficeEndPos) //steals ipad
                {
                    ScreenGameObject.transform.Find("Room" + guy9CurrRoom + "Sprites").Find("guy9").transform.gameObject.SetActive(false);
                    ScreenGameObject.SetActive(false);
                    popUpButton.SetActive(false);
                }

                if (guy9InsideOfficeGameObject.transform.position.y > -25 && popUpButton.activeInHierarchy == false) //slowly creeping down until out of sight, then sets self to inactive
                {
                    guy9InsideOfficeGameObject.transform.position -= new Vector3(0, Time.deltaTime, 0);
                }
                else if (guy9InsideOfficeGameObject.transform.position.y <= -25 && popUpButton.activeInHierarchy == false)
                {
                    guy9InsideOffice = false;
                    allGuysActive[8] = false;
                    guy9InsideOfficeGameObject.SetActive(false);
                }
            }
        }
        #endregion

        #region guy10
        if (allGuysActive[9] == true && guy10GameObject.transform.gameObject.activeInHierarchy == false)
        {
            guy10GameObject.SetActive(true);
            guy10Disclaimer.SetActive(true);

        }
        #endregion

        #region guy11
        if (allGuysActive[10] == true)
        {
            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 1 && guy11GameObject.activeInHierarchy == false)
            {
                guy11GameObject.SetActive(true);

                for(int i = 0; i < guy11PlantGameObjects.Count;i++)
                {
                    guy11PlantGameObjects[i].SetActive(true);
                }

            }
            else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != 1 && guy11GameObject.activeInHierarchy == true)
            {
                guy11GameObject.SetActive(false);

                for (int i = 0; i < guy11PlantGameObjects.Count; i++)
                {
                    guy11PlantGameObjects[i].SetActive(false);
                }
            }

            #region Plant Updating Details
            for (int i = 0; i < guy11PlantGameObjects.Count; i++)
            {
                guy11WilterMultiTimer[i] -= Time.deltaTime;

                guy11Moisture[i] -= allDiffs[10] * guy11WilterRate[i];

                if (guy11WilterMultiTimer[i] <= 0)
                {
                    guy11Moisture[i] -= guy11WilterBonusWilter[i];
                    guy11WilterBonusWilter[i] += 0.00005f;
                }

                if (guy11Hovering[i] == true && Input.GetMouseButton(0))
                {
                    guy11Moisture[i] += 0.5f;
                    guy11WilterMultiTimer[i] = 10f;
                    guy11WilterBonusWilter[i] = 0;
                    if(guy11GameObject.GetComponent<AudioSource>().isPlaying == false)
                    {
                        
                        guy11GameObject.GetComponent<AudioSource>().time = Random.Range(0, guy11GameObject.GetComponent<AudioSource>().clip.length - 15);
                        guy11GameObject.GetComponent<AudioSource>().volume = 1;
                        guy11GameObject.GetComponent<AudioSource>().Play(); //plays watering noise
                    }
                }
                else if(!Input.GetMouseButton(0) && guy11GameObject.GetComponent<AudioSource>().isPlaying == true)
                {
                    guy11GameObject.GetComponent<AudioSource>().volume -= Master.instance.soundLevel/100*Time.deltaTime; //fades out in one second, add '/ n' at the end to make it fade out faster. 
                    if (guy11GameObject.GetComponent<AudioSource>().volume <= 0)
                    {
                        guy11GameObject.GetComponent<AudioSource>().Stop(); //stops playing watering noise
                    }
                }

                #region wilter level based on moisture
                if (guy11Moisture[i] >= 66 && guy11WilterLevel[i] != 0)
                {
                    if (ScreenPopUp.instance.currRoom == 1)
                    {
                        SoundManager.instance.PlaySound(SoundType.GUY11, 0, 1); //plays whistle going up
                    }

                    guy11WilterLevel[i] = 0;
                    guy11WilterLevelGameObject[i].SetActive(false);
                    guy11PlantGameObjects[i].transform.GetChild(guy11WilterLevel[i]).gameObject.SetActive(true);
                    guy11WilterLevelGameObject[i] = guy11PlantGameObjects[i].transform.GetChild(guy11WilterLevel[i]).transform.gameObject;
                }
                else if (guy11Moisture[i] < 66 && guy11Moisture[i] >= 33 && guy11WilterLevel[i] != 1)
                {
                    if (ScreenPopUp.instance.currRoom == 1)
                    {
                        if (guy11WilterLevel[i] == 2) //the level went up
                        {
                            SoundManager.instance.PlaySound(SoundType.GUY11, 0, 1); //plays whistle going up
                        }
                        else if (guy11WilterLevel[i] == 0) //the level went down
                        {
                            SoundManager.instance.PlaySound(SoundType.GUY11, 1, 1); //plays whistle going down
                        }
                    }

                    guy11WilterLevel[i] = 1;
                    guy11WilterLevelGameObject[i].SetActive(false);
                    guy11PlantGameObjects[i].transform.GetChild(guy11WilterLevel[i]).gameObject.SetActive(true);
                    guy11WilterLevelGameObject[i] = guy11PlantGameObjects[i].transform.GetChild(guy11WilterLevel[i]).transform.gameObject;
                }
                else if (guy11Moisture[i] < 33 && guy11WilterLevel[i] != 2)
                {
                    if (ScreenPopUp.instance.currRoom == 1)
                    {
                        SoundManager.instance.PlaySound(SoundType.GUY11, 1, 1); //plays whistle going down
                    }

                    guy11WilterLevel[i] = 2;
                    guy11WilterLevelGameObject[i].SetActive(false);
                    guy11PlantGameObjects[i].transform.GetChild(guy11WilterLevel[i]).gameObject.SetActive(true);
                    guy11WilterLevelGameObject[i] = guy11PlantGameObjects[i].transform.GetChild(guy11WilterLevel[i]).transform.gameObject;
                }
                #endregion

                if (guy11Moisture[i] > 100 || guy11Moisture[i] < 0)
                {
                    Master.instance.loseText = "One of the plants has died, why weren't you helping Ryy? Game Over";
                    Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                    SceneManager.LoadScene("Game Over");
                }
            }
            #endregion
        }
        #endregion

        #region guy12
        if (allGuysActive[11] == true)
        {
            guy12Timer -= Time.deltaTime;

            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 3 && guy12GameObjects[0].activeInHierarchy == false)
            {
                guy12GameObjects[0].SetActive(true);
            }
            else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != 3 && guy12GameObjects[0].activeInHierarchy == true)
            {
                guy12GameObjects[0].SetActive(false);
            }

            if (guy12ScrunglyEscaped == true)
            {
                if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == guy12ScrunglyLocation && ScreenGameObject.transform.Find("Room" + guy12ScrunglyLocation + "Sprites").Find("guy12Scrungly").transform.gameObject.activeInHierarchy == false)
                {
                    ScreenGameObject.transform.Find("Room" + guy12ScrunglyLocation + "Sprites").Find("guy12Scrungly").transform.gameObject.SetActive(true);
                }
                else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != guy12ScrunglyLocation && ScreenGameObject.transform.Find("Room" + guy12ScrunglyLocation + "Sprites").Find("guy12Scrungly").transform.gameObject.activeInHierarchy == true)
                {
                    ScreenGameObject.transform.Find("Room" + guy12ScrunglyLocation + "Sprites").Find("guy12Scrungly").transform.gameObject.SetActive(false);
                }
            }

            if (guy12ScrunglyEscaped == true && guy12Timer <= 0)
            {
                Master.instance.loseText = "Scrungly pissed everywhere WTF! Game Over";
                Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("Game Over");
            }

            if (guy12Timer <= 0 && guy12ScrunglyEscaped == false)
            {
                SoundManager.instance.PlaySound(SoundType.GUY12, 0, 1); //plays gasp
                guy12ScrunglyEscaped = true;
                guy12ScrunglyEscapeAttempts = 3 + Mathf.FloorToInt(7f/20f * allDiffs[11]); //diff1 = 3 escape attempts. diff20 = 10 escape attempts.
                guy12Timer = 8 + (12f / 19f * (20f - allDiffs[11])); //diff1 = 20 seconds to find. diff20 = 8 seconds to find
                guy12ScrunglyLocation = Random.Range(0, 10);

                guy12GameObjects[1].SetActive(false); //head sleeping peacefully
                guy12GameObjects[2].SetActive(true); //head standing up worried
                guy12GameObjects[3].SetActive(false); //frog resting on Tri
            }

        }
        #endregion

        #region guy13
        if(allGuysActive[12] == true)
        {
            guy13Timer -= Time.deltaTime;

            //guy13 themselves
            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 4 && ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("guy13").transform.gameObject.activeInHierarchy == false)
            {
                ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("guy13").transform.gameObject.SetActive(true);
            }
            else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != 4 && ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("guy13").transform.gameObject.activeInHierarchy == true)
            {
                ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("guy13").transform.gameObject.SetActive(false);
            }

            //the interactive buttons
            if (guy13InNeed == true && ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 4 && ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("guy13Lotion").transform.gameObject.activeInHierarchy == false)
            {
                ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("guy13Lotion").transform.gameObject.SetActive(true);
                ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("guy13Drink").transform.gameObject.SetActive(true);
                ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("guy13Refresh").transform.gameObject.SetActive(true);
                ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("Tofu").transform.gameObject.SetActive(true);
            }
            else if((ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != 4 || guy13InNeed == false) && ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("guy13Lotion").transform.gameObject.activeInHierarchy == true)
            {
                ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("guy13Lotion").transform.gameObject.SetActive(false);
                ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("guy13Drink").transform.gameObject.SetActive(false);
                ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("guy13Refresh").transform.gameObject.SetActive(false);
                ScreenGameObject.transform.Find("Room" + 4 + "Sprites").Find("Tofu").transform.gameObject.SetActive(false);
            }

            if (guy13Timer <= 0 && guy13InNeed == false)
            {
                guy13InNeed = true;
                guy13Needs = Random.Range(0, 3);
                guy13Expressions[guy13Needs].SetActive(true);
                guy13Timer = 7 + (13f/19f * (20f - allDiffs[12])); //diff1 = 20 seconds. diff20 = 7 seconds.
            }
            if(guy13Timer <= 0 && guy13InNeed == true)
            {
                if (guy13Burns > 0)
                {
                    guy13Burns--;
                    guy13Timer = Random.Range(guy13Interval / 2, guy13Interval);
                    guy13Expressions[guy13Needs].SetActive(false);
                    guy13InNeed = false;
                    guy13CooldownMethod = -1;
                }
                else
                {
                    Master.instance.loseText = "Tofu has fried under the sun and is delicious, Game Over";
                    Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                    SceneManager.LoadScene("Game Over");
                }
            }
        }
        #endregion

        #region guy14
        if(allGuysActive[13] == true && guy14allCamerasFucked == false)
        {
            guy14Timer -= Time.deltaTime;

            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 8 && ScreenGameObject.transform.Find("Room" + 8 + "Sprites").Find("guy14").transform.gameObject.activeInHierarchy == false)
            {
                ScreenGameObject.transform.Find("Room" + 8 + "Sprites").Find("guy14").transform.gameObject.SetActive(true);
                ScreenGameObject.transform.Find("Room" + 8 + "Sprites").Find("Next Song Button").transform.gameObject.SetActive(true);
            }
            else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != 8 && ScreenGameObject.transform.Find("Room" + 8 + "Sprites").Find("guy14").transform.gameObject.activeInHierarchy == true)
            {
                ScreenGameObject.transform.Find("Room" + 8 + "Sprites").Find("guy14").transform.gameObject.SetActive(false);
                ScreenGameObject.transform.Find("Room" + 8 + "Sprites").Find("Next Song Button").transform.gameObject.SetActive(false);
            }

            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 8 && ScreenGameObject.activeInHierarchy == true) //ambient squeaking when in room 8
            {
                guy14AmbientSqueaks -= Time.deltaTime;
                if (guy14AmbientSqueaks <= 0)
                {
                    SoundManager.instance.PlaySound(SoundType.GUY14, Random.Range(1, 5), 1);
                    guy14AmbientSqueaks = Random.Range(1f, 3f);
                }
            }

            if (guy14MusicTaste != guy14CurrentTrack)
            {
                guy14Patience -= Time.deltaTime;

                if(ScreenPopUp.instance.currRoom == 8 && Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().resource != Master.instance.MusicTracks[11])
                {
                    Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().resource = Master.instance.MusicTracks[11];
                    Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().time = Master.instance.musicTime;
                    Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                }
            }
            else if(guy14Patience < Mathf.Clamp(guy14Interval/2 - allDiffs[13], 12, 999))
            {
                guy14Patience += Time.deltaTime;
            }

            if(guy14Timer <= 0 && guy14MusicTaste == guy14CurrentTrack)
            {
                guy14MusicTaste = Random.Range(0, 6);
                guy14Timer = Random.Range(guy14Interval / 2, guy14Interval * 2);
            }
            if(guy14Patience <= 0 && guy14MusicTaste != guy14CurrentTrack)
            {
                guy14CamerasToFuckUp = 2 + Mathf.FloorToInt(allDiffs[13] / 10); //diff1 = 2 cameras. diff10 = 3 cameras. diff20 = 4 cameras. Reapplies incase of Ryan increasing difficulty.
                GnawTheWires();//break some shit

                guy14MusicTaste = Random.Range(0, 6);
                guy14CurrentTrack = guy14MusicTaste;
                guy14Timer = Random.Range(guy14Interval / 2, guy14Interval);
                guy14Patience = Mathf.Clamp(guy14Interval / 2 - allDiffs[13], 12, 999);
            }
        }

        //Malfunctioning cameras counting down to malfunction
        for (int i = 0; i < guy14MalfCameras.Length; i++)
        {
            if (guy14MalfCameras[i] == true)
            {
                guy14CameraMalfTimers[i] -= Time.deltaTime;
                if (guy14CameraMalfTimers[i] <= 0)
                {
                    if(ScreenPopUp.instance.currRoom == i)
                    {
                        CameraMalfunction(i);
                    }
                    guy14CameraMalfTimers[i] = Random.Range(5f, 10f);
                }
            }
        }
        #endregion

        #region guy15
        if (allGuysActive[14] == true)
        {
            guy15Mood -= Time.deltaTime;

            if(guy15HeadpatOverload > 0)
            {
                guy15HeadpatOverload -= Time.deltaTime * (guy15HeadpatOverload/10);
            }

            if (guy15MoodBar.activeInHierarchy == true)
            {
                guy15MoodBar.transform.GetChild(1).transform.GetComponent<Image>().fillAmount = guy15Mood / 200;
            }

            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 2 && guy15GameObject.activeInHierarchy == false)
            {
                guy15GameObject.SetActive(true);
            }
            else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != 2 && guy15GameObject.activeInHierarchy == true)
            {
                guy15GameObject.SetActive(false);
            }

            if(guy15Mood <= 0 || guy15Mood >= 200)
            {
                Master.instance.loseText = "Moyasi is overwhelmed and is now crying, happy now? Game Over";
                Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("Game Over");
            }

            if ((guy15Mood >= 0 && guy15Mood <= 50 && guy15CurrentMood != 0) || (guy15Mood > 50 && guy15Mood <= 150 && guy15CurrentMood != 1) || (guy15Mood > 150 && guy15Mood <= 200 && guy15CurrentMood != 2))
            {
                for (int i = 0; i < guy15GameObject.transform.childCount; i++) //resets the sprites to something suitable below
                {
                    if (guy15GameObject.transform.GetChild(i).transform.gameObject.activeInHierarchy == true)
                    {
                        guy15GameObject.transform.GetChild(i).transform.gameObject.SetActive(false);
                    }
                }

                if (guy15Mood >= 0 && guy15Mood <= 50) //mood is critically low
                {
                    guy15GameObject.transform.GetChild(3).transform.gameObject.SetActive(true);
                    guy15CurrentMood = 0;
                }
                else if (guy15Mood > 50 && guy15Mood <= 150) //mood is normal levels
                {
                    guy15CurrentMood = 1;
                    guy15GameObject.transform.GetChild(Random.Range(0, 3)).transform.gameObject.SetActive(true);
                }
                else if (guy15Mood > 150 && guy15Mood <= 200) //mood is critically high
                {
                    guy15GameObject.transform.GetChild(4).transform.gameObject.SetActive(true);
                    guy15CurrentMood = 2;
                }
            }
        }
        #endregion

        #region guy16
        if(allGuysActive[15] == true)
        {
            guy16Timer -= Time.deltaTime;
            
            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 0 && ScreenGameObject.transform.Find("Room" + 0 + "Sprites").Find("guy16").transform.gameObject.activeInHierarchy == false)
            {
                ScreenGameObject.transform.Find("Room" + 0 + "Sprites").Find("guy16").transform.gameObject.SetActive(true);
            }
            else if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != 0 && ScreenGameObject.transform.Find("Room" + 0 + "Sprites").Find("guy16").transform.gameObject.activeInHierarchy == true)
            {
                ScreenGameObject.transform.Find("Room" + 0 + "Sprites").Find("guy16").transform.gameObject.SetActive(false);
            }

            if(guy16Timer <= 0 && ScreenGameObject.GetComponent<ScreenPopUp>().currRoom != 0)
            {
                MoveAroundSpookyLike();
            }

            if (ScreenGameObject.GetComponent<ScreenPopUp>().currRoom == 0 && ScreenGameObject.activeInHierarchy == true) //ambient popping when in room 0
            {
                guy16AmbientPops -= Time.deltaTime;
                if (guy16AmbientPops <= 0)
                {
                    SoundManager.instance.PlaySound(SoundType.GUY16, Random.Range(0, 8), 1);
                    guy16AmbientPops = Random.Range(5f, 10f);
                }
            }
        }
        #endregion

        #region guy17
        //if(allGuysActive[16] == true)
        //{
             //if(guy17IsFixingIt == true && )
        //}
        #endregion

        #region guy18
        if (allGuysActive[17] == true)
        {
            if(guy18FortuneReady == false)
            {
                guy18FortuneCooldown -= Time.deltaTime;
            }
            if(guy18FortuneCooldown <= 0 && guy18FortuneReady == false)
            {
                guy18FortuneReady = true;
                SoundManager.instance.PlaySound(SoundType.GUY18, 0, 1);
            }

            if (guy18GameObject.transform.gameObject.activeInHierarchy == false)
            {
                guy18GameObject.SetActive(true);
            }

            if(guy18Message != null)
            {
                guy18Message.transform.position = guy18Message.transform.position + new Vector3(0, 0.01f);
            }
        }
        #endregion

        #region guy19
        if (allGuysActive[18] == true)
        {
            if (guy19Timer > 0)
            {
                guy19Timer -= Time.deltaTime;
            }

            if(guy19Timer <= 0 && guy19GloveEscaped == false)
            {
                guy19GloveEscaped = true;
                guy19AllGloves[Random.Range(0, guy19AllGloves.Count - 1)].SetActive(true);
                SoundManager.instance.PlaySound(SoundType.GUY19, 2, 1);
            }
        }
        #endregion

        #region Camera Flash
        //camera flash fading out
        if (flashValues.a > 0)
        {
            flashValues.a -= 0.1f;
            flash.GetComponent<Image>().color = new Color(1f, 1f, 1f, flashValues.a);
        }
        #endregion

        #region Screen Static
        if (screenStaticValues.a > 0)
        {
            screenStaticValues.a -= 0.01f;
            screenStatic.GetComponent<Image>().color = new Color(1f, 1f, 1f, screenStaticValues.a);
            if(screenStatic.GetComponent<Image>().raycastTarget == false)
            {
                screenStatic.GetComponent<Image>().raycastTarget = true;
            }
        }
        else if(screenStaticValues.a <= 0 && screenStatic.GetComponent<Image>().raycastTarget == true)
        {
            screenStatic.GetComponent<Image>().raycastTarget = false;
        }
        #endregion
    }

    public void CatchTheByte(int location) //Jacob
    {
        guy1ScreenLocations[location] = false;
        guy1ByteGameObjects[location].SetActive(false);
        SoundManager.instance.PlaySound(SoundType.GUY1, 3, 1 * 1); //plays goblin laugh

        int guy1CaughtTimes = 0;
        for(int i = 0; i < guy1ScreenLocations.Length; i++) //checks if all 3 are off
        {
            if(guy1ScreenLocations[i] == false)
            {
                guy1CaughtTimes++;
            }
        }

        if(guy1CaughtTimes == guy1ScreenLocations.Length) //if all 3 are off, reset guy1
        {
            guy1Escaped = false;
            guy1Timer = Random.Range(guy1Interval, guy1Interval * 2);
            guy1BonusDrain = 0;
        }
    }

    public void TakeAPicture()
    {
        SoundManager.instance.PlaySound(SoundType.GUY2, 1, 1 * 1); //plays camera flash
        flash.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        flashValues = new Color(1f, 1f, 1f, 1f);

        if (allGuysActive[1] == true && guy2Location == ScreenGameObject.GetComponent<ScreenPopUp>().currRoom && guy2Arrive == true)
        {
            guy2Satisfaction += 10;
            if (Random.Range(0, 101) <= guy2Satisfaction)
            {
                SoundManager.instance.PlaySound(SoundType.GUY2, 0, 1 * 1); //plays bell jingle
                allGuysActive[1] = false; //satisfied, leaves
                guy2Arrive = false;
                ScreenGameObject.transform.Find("Room" + guy2Location + "Sprites").Find("guy2").transform.gameObject.SetActive(false);
                roomsNotPhotographedYet = new List<int>(); //resets list
                for (int i = 0; i < 10; i++)
                {
                    roomsNotPhotographedYet.Add(i);
                }
                //guy2Satisfaction = (70f/19f)*(20f-allDiffs[1]); //diff 1 = 70%. diff 20 = 0%
                guy2Timer = guy2Interval;
                guy2Location = 7;
            }
            else
            {
                roomsNotPhotographedYet.Remove(guy2Location);
                ScreenGameObject.transform.Find("Room" + guy2Location + "Sprites").Find("guy2").transform.gameObject.SetActive(false);
                guy2Location = roomsNotPhotographedYet[Random.Range(0, roomsNotPhotographedYet.Count - 1)];
                guy2Timer = guy2Interval;
            }
        }
    } //Shift

    public void SnoozeTheAlarm()
    {
        if (guy3Alarms.Count > 0) //to avoid errors if player presses it while the clock is finished or inactive
        {
            if (allGuysActive[2] == true && shiftTime >= guy3Alarms[0] && shiftTime < guy3Alarms[0] + guy3GracePeriod && guy3Alarms.Count > 0)
            {
                guy3Alarms.RemoveAt(0);
                guy3AlarmGameObject.GetComponent<AudioSource>().Stop();
            }
            else if (guy3Alarms.Count == 0 || shiftTime < guy3Alarms[0])
            {
                Master.instance.loseText = "You pressed the alarm too soon, which inexplicably sets it off, your fault obviously, Game Over";
                Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("Game Over");
            }

            if (guy3Alarms.Count == 0)
            {
                guy3HourDisplay.text = "Sleep Time :)";
            }
            else
            {
                int min = Mathf.FloorToInt(guy3Alarms[0] / 60);
                int sec = Mathf.FloorToInt(guy3Alarms[0] % 60);
                guy3HourDisplay.text = min.ToString("00") + ":" + sec.ToString("00");
            }
        }
    } //Denim

    public void SendSoundBack()
    {
        guy5Location = 8;
        ScreenGameObject.transform.Find("Room9Sprites").Find("guy5").transform.gameObject.SetActive(false); //pointless if the button game object IS the sprite?
        guy5DoorPeak.SetActive(false);
        guy5Timer = Random.Range((guy5Interval / 2) + 20, guy5Interval * 2);
        SoundManager.instance.PlaySound(SoundType.GUY5, 1, 1 * 1); //plays angry zipper

    } //Sound

    public void UnSinkTheShip()
    {
        guy6Timer += 10;
        guy6Timer = Mathf.Clamp(guy6Timer, 0, guy6Interval);
        SoundManager.instance.PlaySound(SoundType.GUY6, Random.Range(0, 5), 1 * 1);
    } //Capt

    public void ScareTheHugger()
    {
        if (guy7Timer >= 0.5f)
        {
            guy7BehindYou = false;
            guy7Interval = Random.Range(50, 80) - allDiffs[6];
            guy7Timer = 0;
            guy7HugTime = 15 - (10f / 19f) * (20 - allDiffs[6]); //diff1 = 5 seconds. diff20 = 15 seconds
            guy7GameObject.transform.position = guy7Start.transform.position;
            SoundManager.instance.PlaySound(SoundType.GUY7, 1, 1); //plays dash sound
        }else
        {
            guy7BehindYou = true;
            ScreenGameObject.gameObject.SetActive(false);
            SoundManager.instance.PlaySound(SoundType.GUY7, 2, 1); //plays hug noise
        }
    } //Kestryl

    public int CreateShoppingList()
    {
        int a = 0;

        while (guy8ShoppingList.Count < guy8HowManyMissing)
        {
            a = Random.Range(0, 9); //9 max
            if (!guy8ShoppingList.Contains(a))
            {
                guy8ShoppingList.Add(a);
            }
        }
        return a;
    } //Robin

    public void NextFood()
    {
        SoundManager.instance.PlaySound(SoundType.GUI, 0, 1);
        guy8currButton++;
        if (guy8currButton > 8)
        {
            guy8currButton = 0;
        }
        guy8CurrFoodDisplayer.GetComponent<Image>().sprite = guy8FoodButtons[guy8currButton];
    } //Robin

    public void OrderFood()
    {
        if (allGuysActive[7] == true && guy8OrderingCooldown <= 0 && guy8ShoppingList.Count > 0)
        {
            if (guy8ShoppingList.Contains(guy8currButton))
            {
                guy8ShoppingList.Remove(guy8currButton);
                SoundManager.instance.PlaySound(SoundType.GUY8, 4, 1);
            }
            else
            {
                SoundManager.instance.PlaySound(SoundType.GUY14, 4, 1); //plays wrong buzzer (from tofu's sfx)
            }
            guy8OrderingCooldown = 5f;

            if (guy8ShoppingList.Count == 0) //nothing more to order
            {
                float guy8IntervalHalf = guy8Interval / 2;
                guy8Timer += Random.Range(guy8IntervalHalf + (guy8IntervalHalf / 19f * (20f - allDiffs[7])), guy8IntervalHalf + (guy8IntervalHalf / 19 * (20f - allDiffs[7])*2)); //diff1 = 60-120 second interval. diff20 = 30-60 second interval
                guy8HowManyMissing = 9 - Mathf.RoundToInt((6f / 19f) * (20f - allDiffs[7])); //diff1 = 3 items. diff20 = 9 items //re-done here in case of ryan updiff
                SoundManager.instance.PlaySound(SoundType.GUY8, 1, 1);
            }
        }
    } //Robin

    public void SendAndechBack()
    {
        guy9CurrRoom = 5;
        guy9Timer = Random.Range(guy9Interval * 0.75f, guy9Interval);
        guy9DoorPeak.SetActive(false);
        SoundManager.instance.PlaySound(SoundType.GUY9, 3, 1);
    } //Andech

    public void AndechPopDown()
    {
        if (popUpButton.activeInHierarchy == true)
        {
            guy9InsideOfficeGameObject.transform.position -= new Vector3(0, 6, 0);
        }
        SoundManager.instance.PlaySound(SoundType.GUY9, Random.Range(4, 8), 1);
    } //Andech

    public void NowImGettingRealPissedOff()
    {
        for (int i = 0; i < allDiffs.Length; i++)
        {
            if (allDiffs[i] < 20 && allDiffs[i] != 0)
            {
                allDiffs[i]++;
            }
        }
        SoundManager.instance.PlaySound(SoundType.GUY10, 2, 1);
        guy10TimesPissedOff++;
    } //Ryan

    public void FindTheScrungly()
    {
        SoundManager.instance.PlaySound(SoundType.GUY12, Random.Range(1, 3), 1); //plays frog croaks
        guy12ScrunglyEscapeAttempts--;
        guy12Timer = 8 + (12f/19f * (20f - allDiffs[11])); //diff1 = 20 seconds to find. diff20 = 8 seconds to find

        if (guy12ScrunglyEscapeAttempts <= 0)
        {
            SoundManager.instance.PlaySound(SoundType.GUY12, 3, 1); //plays fanfare
            ScreenGameObject.transform.Find("Room" + guy12ScrunglyLocation + "Sprites").Find("guy12Scrungly").transform.gameObject.SetActive(false);
            guy12ScrunglyEscaped = false;
            guy12Timer = Random.Range(guy12Interval, guy12Interval*2);

            guy12GameObjects[1].SetActive(true); //head sleeping peacefully
            guy12GameObjects[2].SetActive(false); //head standing up worried
            guy12GameObjects[3].SetActive(true); //frog resting on Tri
        }
        else
        {
            int noDupe = guy12ScrunglyLocation;

            ScreenGameObject.transform.Find("Room" + guy12ScrunglyLocation + "Sprites").Find("guy12Scrungly").transform.gameObject.SetActive(false);

            guy12ScrunglyLocation = Random.Range(0, 10);
            if(guy12ScrunglyLocation == noDupe)
            {
                guy12ScrunglyLocation++;
                if(guy12ScrunglyLocation > 9)
                {
                    guy12ScrunglyLocation = 0;
                }
            }

            do { guy12ScrunglyLocation = Random.Range(0, 10); }
            while (guy12ScrunglyLocation == noDupe);
        }
    } //Tri

    public void CooldownTheTofu()
    {
        if (guy13InNeed == true && guy13CooldownMethod > -1)
        {
            if (guy13CooldownMethod == guy13Needs)
            {
                guy13Expressions[guy13Needs].SetActive(false);
                guy13InNeed = false;
                guy13CooldownMethod = -1;
                guy13Timer = Random.Range(guy13Interval, guy13Interval * 1.5f);
                SoundManager.instance.PlaySound(SoundType.GUY13, 3, 1); //plays correct jingle
            }
            else
            {            
                if (guy13Burns > 0)
                {
                    guy13Burns--;
                    guy13Timer = Random.Range(guy13Interval / 2, guy13Interval);
                    guy13Expressions[guy13Needs].SetActive(false);
                    guy13InNeed = false;
                    guy13CooldownMethod = -1;
                    SoundManager.instance.PlaySound(SoundType.GUY13, 4, 1); //plays wrong jingle
                }
                else
                {
                    Master.instance.loseText = "Tofu has fried under the sun and is delicious, Game Over";
                    Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                    SceneManager.LoadScene("Game Over");
                }
            }
        }
    } //Tofu

    public void CooldownMethod(int i)
    {
        guy13CooldownMethod = i;
    } //Tofu

    public void SwitchTracks()
    {
        SoundManager.instance.PlaySound(SoundType.GUY14, Random.Range(5,9), 1);

        if (allGuysActive[13] == true)
        {
            guy14CurrentTrack++;
            if (guy14CurrentTrack > 5)
            {
                guy14CurrentTrack = 0;
            }

            if(guy14CurrentTrack == guy14MusicTaste) //track becomes normal again
            {
                Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().resource = Master.instance.MusicTracks[ScreenPopUp.instance.currRoom];
                Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().time = Master.instance.musicTime;
                Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
            }
        }
    } //Key

    public void GnawTheWires()
    {
        List<int> goodCameras = new List<int>(); // ideally a class member to avoid unnecessary garbage - if you do that, make sure to Clear the list as well

        for (int i = 0; i < guy14MalfCameras.Length;i++)
        {
            if (guy14MalfCameras[i] == false)
            {
                goodCameras.Add(i);
            }
        }

        for(int i = 0; i < guy14CamerasToFuckUp; i++)
        {
            int r = Random.Range(0, goodCameras.Count-1);
            guy14MalfCameras[goodCameras[r]] = true;
            goodCameras.RemoveAt(r);
        }

        SoundManager.instance.PlaySound(SoundType.GUY14, 0, 1);

        if(goodCameras.Count == 0)
        {
            guy14allCamerasFucked = true;
            ScreenGameObject.transform.Find("Room" + 8 + "Sprites").Find("guy14").transform.gameObject.SetActive(false);
            ScreenGameObject.transform.Find("Room" + 8 + "Sprites").Find("Next Song Button").transform.gameObject.SetActive(false);
        }
    } //Key

    public void CameraMalfunction(int i)
    {
        if(ScreenPopUp.instance.currRoom == i)
        {
            screenStatic.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            screenStaticValues = new Color(1f, 1f, 1f, 1f);
        }
    } //Key

    public void PetTheBean()
    {
        SoundManager.instance.PlaySound(SoundType.GUY15, Random.Range(0, 9), 1);
        guy15Mood += Random.Range(0, allDiffs[14] + guy15HeadpatOverload); //between 0 to diff + overload
        guy15HeadpatOverload += 10 + allDiffs[14]; //diff1 = 11 overload. diff20 = 30 overload.
    } //Moyasi

    public void MoveAroundSpookyLike()
    {
        int lastGuy = 0;
        List<GameObject> otherguys = new List<GameObject>();

        for (int i = 0; i < guy16AllSprites.transform.childCount; i++)
        {
            if(guy16AllSprites.transform.GetChild(i).transform.gameObject.activeSelf == false)
            {
                otherguys.Add(guy16AllSprites.transform.GetChild(i).transform.gameObject);
            }
            else
            {
                lastGuy = i;
            }
        }
        guy16AllSprites.transform.GetChild(lastGuy).transform.gameObject.SetActive(false);
        otherguys[Random.Range(0, otherguys.Count)].transform.gameObject.SetActive(true);

        guy16Timer = Random.Range(5f, 10f + (19f/20f * (20f-allDiffs[15])));
    } //Jake

    public void ChanceToFeather()
    {
        if (guy17IsHere == false && Random.Range(0f, 100f) < guy17ChanceOfFeathers + 5)
        {
            ScreenGameObject.transform.Find("Room" + ScreenPopUp.instance.currRoom + "Sprites").Find("Feather").transform.gameObject.SetActive(true);
        }
    } //Anfor

    public void GrabTheFeather()
    {
        guy17FeathersLeft--;
        ScreenGameObject.transform.Find("Room" + ScreenPopUp.instance.currRoom + "Sprites").Find("Feather").transform.gameObject.SetActive(false);
        if (guy17FeathersLeft <= 0)
        {
            guy17IsHere = true;
            guy17FixItButton.SetActive(true);
        }

        SoundManager.instance.PlaySound(SoundType.GUY17, Random.Range(0, 3), 1); //plays feather sounds
    } //Anfor

    public void FixEverythingHere()
    {
        int cr = ScreenPopUp.instance.currRoom;
        SoundManager.instance.PlaySound(SoundType.GUY17, 3, 1);

        if (cr == 0)
        {
            #region guy2
            if(allGuysActive[1] == true && guy2Location == cr)
            {
                guy2Satisfaction += 100;
                TakeAPicture();
            }
            #endregion
            #region guy16
            if (allGuysActive[15] == true)
            {
                Debug.Log("Oh yeah you've reset Jake, totally");
            }
            #endregion
        }
        else if (cr == 1)
        {
            #region guy2
            if (allGuysActive[1] == true && guy2Location == cr)
            {
                guy2Satisfaction += 100;
                TakeAPicture();
            }
            #endregion
            #region guy11
            if(allGuysActive[10] == true)
            {
                for(int i = 0; i < guy11PlantGameObjects.Count;i++)
                {
                    guy11Moisture[i] = 100;
                }
            }
            #endregion
        }
        else if(cr == 2)
        {
            #region guy2
            if (allGuysActive[1] == true && guy2Location == cr)
            {
                guy2Satisfaction += 100;
                TakeAPicture();
            }
            #endregion
            #region guy3
            if (allGuysActive[2] == true)
            {
                guy3Alarms.RemoveAt(0);
                if (guy3Alarms.Count == 0)
                {
                    guy3HourDisplay.text = "Sleep Time :)";
                }
                else
                {
                    int min = Mathf.FloorToInt(guy3Alarms[0] / 60);
                    int sec = Mathf.FloorToInt(guy3Alarms[0] % 60);
                    guy3HourDisplay.text = min.ToString("00") + ":" + sec.ToString("00");
                }
            }
            #endregion
            #region guy15
            if(allGuysActive[14] == true)
            {
                guy15Mood = Random.Range(150f, 199f);
                guy15HeadpatOverload = 0;
            }
            #endregion

        }
        else if (cr == 3)
        {
            #region guy2
            if (allGuysActive[1] == true && guy2Location == cr)
            {
                guy2Satisfaction += 100;
                TakeAPicture();
            }
            #endregion
            #region guy12
            if(allGuysActive[11] == true)
            {
                guy12ScrunglyEscapeAttempts = 0;
                FindTheScrungly();
            }
            #endregion //add this to all other rooms if scrungly is in them
        }
        else if (cr == 4)
        {
            #region guy2
            if (allGuysActive[1] == true && guy2Location == cr)
            {
                guy2Satisfaction += 100;
                TakeAPicture();
            }
            #endregion
            #region guy6
            if(allGuysActive[5] == true)
            {
                guy6Timer = guy6Interval;
            }
            #endregion
            #region guy13
            if(allGuysActive[12] == true)
            {
                guy13Burns = 0;
                guy13Needs = 0;
                guy13CooldownMethod = 0;
                guy13Timer = Random.Range(guy13Interval * 1.2f, guy13Interval * 1.5f);
            }
            #endregion
        }
        else if (cr == 5)
        {
            #region guy2
            if (allGuysActive[1] == true && guy2Location == cr)
            {
                guy2Satisfaction += 100;
                TakeAPicture();
            }
            #endregion
            #region guy9
            if(allGuysActive[8] == true)
            {
                guy9Timer = guy9Interval;
            }
            #endregion
        }
        else if (cr == 6)
        {
            #region guy2
            if (allGuysActive[1] == true && guy2Location == cr)
            {
                guy2Satisfaction += 100;
                TakeAPicture();
            }
            #endregion
            #region guy8
            if(allGuysActive[7] == true)
            {
                guy8ShoppingList.Clear();
                guy8Timer = guy8Interval;
            }
            #endregion
            //guy18?
        }
        else if (cr == 7)
        {
            #region guy2
            if (allGuysActive[1] == true && guy2Location == cr)
            {
                guy2Satisfaction += 100;
                TakeAPicture();
            }
            #endregion
        }
        else if (cr == 8)
        {
            #region guy2
            if (allGuysActive[1] == true && guy2Location == cr)
            {
                guy2Satisfaction += 100;
                TakeAPicture();
            }
            #endregion
            #region guy5
            if(allGuysActive[4] == true)
            {
                SendSoundBack();
                powerEfficiency += guy5PlugsSiphon;
                guy5PlugsSiphon = 0;
            }
            #endregion
            #region guy14
            if(allGuysActive[13] == true)
            {
                guy14MusicTaste = Random.Range(0, 6);
                guy14CurrentTrack = guy14MusicTaste;
                guy14allCamerasFucked = false;
                guy14Timer = Random.Range(guy14Interval, guy14Interval * 1.1f);
                guy14Patience = Mathf.Clamp(guy14Interval / 4, 12, 999);

                //Ungnaw the wires
                for (int i = 0; i < guy14MalfCameras.Length; i++)
                {
                    if (guy14MalfCameras[i] == true)
                    {
                        guy14MalfCameras[i] = false;
                    }
                }
            }
            #endregion
        }
        else if (cr == 9)
        {
            #region guy2
            if (allGuysActive[1] == true && guy2Location == cr)
            {
                guy2Satisfaction += 100;
                TakeAPicture();
            }
            #endregion
            #region guy7
            if(allGuysActive[6] == true)
            {
                ScareTheHugger();
            }
            #endregion
            #region guy10
            if(allGuysActive[9] == true)
            {
                for (int i = 0; i < guy10TimesPissedOff; i++)
                {
                    for (int i2 = 0; i2 < allDiffs.Length; i2++)
                    {
                        allDiffs[i2]--;
                    }
                }
                guy10TimesPissedOff = 0;
                guy10Patience = 5; //make dynamic later
            }
            #endregion
        }

        guy17FixItButton.SetActive(false); //you only get one
    } //Anfor

    public void DrawANewFortune()
    {
        Guy18TarotCycler.instance.bonusVelocity = 100;
        if (guy18FortuneReady == true)
        {
            int f = Random.Range(0, 12);
            SoundManager.instance.PlaySound(SoundType.GUY18, 0, 1);
            if (f == 0)
            {
                powerEfficiency++;
                MessageSpawner("Your very touch will increase the longetivity of your functions");
                SoundManager.instance.PlaySound(SoundType.GUY18, 1, 1);
            }
            else if (f == 1)
            {
                powerEfficiency--;
                MessageSpawner("It may feel as though your interactions drain those around you");
                SoundManager.instance.PlaySound(SoundType.GUY18, 2, 1);
            }
            else if (f == 2)
            {
                power *= 1.1f;
                MessageSpawner("You shall experience great gains today!");
                SoundManager.instance.PlaySound(SoundType.GUY18, 1, 1);
            }
            else if (f == 3)
            {
                power *= 0.9f;
                MessageSpawner("You will experience loss today");
                SoundManager.instance.PlaySound(SoundType.GUY18, 2, 1);
            }
            else if (f == 4)
            {
                shiftTime += 15f;
                MessageSpawner("This day will go by faster than you think");
                SoundManager.instance.PlaySound(SoundType.GUY18, 1, 1);
            }
            else if (f == 5)
            {
                shiftTime -= 30f;
                MessageSpawner("Long day ahead, steel yourself");
                SoundManager.instance.PlaySound(SoundType.GUY18, 2, 1);
            }
            else if (f == 6)
            {
                MouseFollower.instance.turnSpeed++;
                MessageSpawner("Today will be a great day, tackle it head on");
                SoundManager.instance.PlaySound(SoundType.GUY18, 1, 1);
            }
            else if (f == 7)
            {
                //Do nothing
                MessageSpawner("Today will be a day like any other");
                SoundManager.instance.PlaySound(SoundType.GUY18, 1, 1);
            }
            else if (f == 8)
            {
                guy18FortuneCooldown = 999999f;
                MessageSpawner("No more Fortunes for you >:(");
                SoundManager.instance.PlaySound(SoundType.GUY18, 3, 1);
            }
            else if (f == 9)
            {
                //Unlock a...thing
                MessageSpawner("Your gallery will feel unlockier than usual (once I add a gallery lmao)");
                SoundManager.instance.PlaySound(SoundType.GUY18, 4, 1);
            }
            else if (f == 10)
            {
                //Game Over
                if (Random.Range(0, 22 - allDiffs[17]) == 0)
                {
                    Master.instance.loseText = "Look I don't know what you did, maybe you drew a fortune wrong somehow, but it has to be your fault because I know Libra and he's a good guy, Game Over";
                    Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                    SceneManager.LoadScene("Game Over");
                }
                else
                {
                    MessageSpawner("Today will be a day like any other");
                    SoundManager.instance.PlaySound(SoundType.GUY18, 4, 1);
                }
            }
            else if (f == 11) //Libra
            {
                if (Random.Range(0, 2) == 0) //50%
                {
                    powerEfficiency -= 2;
                    shiftTime += 25f;
                    MouseFollower.instance.turnSpeed += 3;
                    MessageSpawner("Everything is going to go your way today, also hotness +2");
                    SoundManager.instance.PlaySound(SoundType.GUY18, 1, 1);
                }
                else
                {
                    MessageSpawner("Today will be a day like any other");
                    SoundManager.instance.PlaySound(SoundType.GUY18, 4, 1);
                }
            }

            guy18FortuneRequirement--;
            guy18FortuneCooldown += 30 + (30f/20f * allDiffs[17]);
            guy18FortuneReady = false;
        }

    } //Libra

    public void MessageSpawner(string messageText) //Libra
    {
        guy18Message = Instantiate(guy18MessagePrefab, guy18GameObject.transform.GetChild(0).transform.position, transform.rotation); //Instantiates on the head
        guy18Message.transform.SetParent(guy18GameObject.transform);
        guy18Message.GetComponent<TextMeshProUGUI>().text = messageText;
    }

    public void CaughtTheGlove(GameObject glove)
    {
        guy19GlovesCaughtRequirement--;

        if(guy19GlovesCaughtRequirement <= 0)
        {
            glove.SetActive(false);
            guy19GloveEscaped = false;
            SoundManager.instance.PlaySound(SoundType.GUY19, 1, 1); //plays startle
        }
        else
        {
            AnotherGloveToCatch(glove);
            SoundManager.instance.PlaySound(SoundType.GUY19, 0, 1); //plays hiss
        }
    } //Rune

    public void AnotherGloveToCatch(GameObject glove)
    {
        List<GameObject> allButOneGlove = guy19AllGloves;
        allButOneGlove.Remove(glove);

        allButOneGlove[Random.Range(0, allButOneGlove.Count)].SetActive(true);
        glove.SetActive(false);
    } //Rune

    public void GloveHasSlipped(GameObject glove)
    {
        List<GameObject> allButOneGlove = guy19AllGloves;
        allButOneGlove.Remove(glove);

        allButOneGlove[Random.Range(0, allButOneGlove.Count - 1)].SetActive(true);
        glove.SetActive(false);

        SoundManager.instance.PlaySound(SoundType.GUY19, 2, 1); //plays laugh
    } //Rune

    public IEnumerator MouthChanger()
    {
        if(guy4ShutUp == false)
        {
            guy4CurrentMouth.SetActive(true);
            yield return new WaitForSeconds(3f);
            guy4CurrentMouth.SetActive(false);
            guy4CurrentMouth = guy4ScreenFeatures[Random.Range(5, guy4ScreenFeatures.Count)];

            if (guy4VoicemailIsPlaying == false) //plays one of the voicemails, making sure this only happens once
            {
                guy4VoicemailIsPlaying = true;
                gameObject.GetComponent<AudioSource>().clip = guy4Voicemails[Random.Range(0, guy4Voicemails.Count)];
                gameObject.GetComponent<AudioSource>().volume = 1;
                gameObject.GetComponent<AudioSource>().Play();
            }

            StartCoroutine(MouthChanger());  
        }
    } //Jack

    public void ShutJackUp()
    {
        if (guy4ShutUp == false)
        {
            GameObject randomPout = guy4ScreenFeatures[Random.Range(1, 4)];
            shiftEnd += 6 * allDiffs[3];
            guy4ShutUp = true;
            guy4CurrentMouth.SetActive(false);
            randomPout.SetActive(true);
            guy4VoicemailIsPlaying = true; //in case they mute it before it plays.

            gameObject.GetComponent<AudioSource>().Stop();
            SoundManager.instance.PlaySound(SoundType.GUY4, 17, 1); //plays mute button sound
            SoundManager.instance.PlaySound(SoundType.GUY4, 18, 1); //plays crane noise
            SoundManager.instance.PlaySound(SoundType.GUY4, Random.Range(6, 12), 1); //plays mute lines

            StartCoroutine(AntennaRetractor(guy4Apparel[0], 3f, 0.1f, randomPout));
        }
    } //Jack

    public IEnumerator AntennaRetractor(GameObject antenna, float duration, float scale, GameObject pout)
    {
        Vector2 startScale = antenna.transform.localScale;
        Vector2 endScale = Vector3.one * scale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            var t = elapsed / duration;
            antenna.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        antenna.transform.localScale = endScale;
        pout.SetActive(false);
    } //Jack

    public void SomethingCameUp()
    {
        SoundManager.instance.PlaySound(SoundType.GUY4, Random.Range(12, 16), 1); //plays crash noise
        gameObject.GetComponent<AudioSource>().Stop();
        SoundManager.instance.PlaySound(SoundType.GUY4, Random.Range(0, 6), 1); //plays cut off lines

        guy4CurrentMouth.SetActive(false);
        guy4ScreenFeatures[1].SetActive(true);
        StartCoroutine(AntennaRetractor(guy4Apparel[0], 3f, 0.1f, guy4ScreenFeatures[1]));
    } //Jack
}
