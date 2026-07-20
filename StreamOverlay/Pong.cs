using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pong : MonoBehaviour
{
    public static Pong instance;

    public List<GameObject> streamerPaddles = new List<GameObject>();
    public List<GameObject> chatPaddles = new List<GameObject>();
    public int streamerPaddleChoice;
    public int chatPaddleChoice = 0;
    public GameObject pongBall;
    public int[] chatPaddleVoting;
    public List<TextMeshProUGUI> votingTexts = new List<TextMeshProUGUI>();
    public GameObject StreamerButtons;
    public GameObject ChatButtons;

    private float aspect;
    private float worldHeight;
    private float worldWidth;

    public float lSpeed = 0.1f;
    public float rSpeed = 0.1f;

    public bool isSetup = false;
    public float setupTime = 90f;
    public TextMeshProUGUI timerText;

    public int[] score;

    private List<string> activePlayers = new List<string>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;

        aspect = (float)Screen.width / Screen.height;
        worldHeight = Camera.main.orthographicSize * 2;
        worldWidth = worldHeight * aspect;

        //GetComponent<BoxCollider2D>().size = new Vector2(Screen.width, Screen.height);
    }

    private void OnEnable()
    {
        isSetup = true;
        setupTime = 90f;
        score = new int[] { 0, 0 };
        chatPaddleVoting = new int[] { 0, 0, 0, 0, 0, 0 };
        activePlayers.Clear();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Movement for the streamer
        if (Input.GetKey(KeyCode.W))
        {
            streamerPaddles[streamerPaddleChoice].transform.position = new Vector3(streamerPaddles[streamerPaddleChoice].transform.position.x, Mathf.Clamp(streamerPaddles[streamerPaddleChoice].transform.position.y + lSpeed, worldHeight / 2 * -0.6f, worldHeight / 2 * 0.6f),0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            streamerPaddles[streamerPaddleChoice].transform.position = new Vector3(streamerPaddles[streamerPaddleChoice].transform.position.x, Mathf.Clamp(streamerPaddles[streamerPaddleChoice].transform.position.y - lSpeed, worldHeight / 2 * -0.6f, worldHeight / 2 * 0.6f),0);
        }

        //Counting down while chatters vote on paddle.
        if(isSetup == true && setupTime > 0)
        {
            timerText.text = setupTime.ToString("f1") + "s left";
            setupTime -= Time.deltaTime;
        }
        else if(isSetup == true && setupTime <= 0)
        {
            isSetup = false;
            activePlayers.Clear();
            StreamerButtons.SetActive(false);
            ChatButtons.SetActive(false);
            pongBall.SetActive(true);
            timerText.gameObject.SetActive(false);
            int currHighest = 0;
            List<int> highestPaddlesID = new List<int>();
            for(int i = 0; i < chatPaddles.Count;i++)
            {
                if(chatPaddleVoting[i] > currHighest || (chatPaddleVoting[i] >= currHighest && chatPaddleVoting[i] > 0))
                {
                    currHighest = chatPaddleVoting[i];
                    highestPaddlesID.Add(i);
                }
            }

            for(int i = 0; i < highestPaddlesID.Count;i++)
            {
                Debug.Log(highestPaddlesID[i]);
            }

            int r = Random.Range(0, highestPaddlesID.Count);

            chatPaddles[highestPaddlesID[r]].SetActive(true);
            chatPaddleChoice = highestPaddlesID[r];
            Debug.Log(r);
        }
    }

    //Movement for the chat
    public void ChatPaddleMover(string player, string direction)
    {
        if(!activePlayers.Contains(player))
        {
            activePlayers.Add(player);
        }

        if(direction == "!Ping")
        {
            chatPaddles[chatPaddleChoice].transform.position = new Vector3(chatPaddles[chatPaddleChoice].transform.position.x, Mathf.Clamp(chatPaddles[chatPaddleChoice].transform.position.y + (rSpeed / activePlayers.Count), worldHeight / 2 * -0.6f, worldHeight / 2 * 0.6f), 0);
            Debug.Log("UP");
        }
        else
        {
            chatPaddles[chatPaddleChoice].transform.position = new Vector3(chatPaddles[chatPaddleChoice].transform.position.x, Mathf.Clamp(chatPaddles[chatPaddleChoice].transform.position.y - (rSpeed / activePlayers.Count), worldHeight / 2 * -0.6f, worldHeight / 2 * 0.6f), 0);
            Debug.Log("Down");
        }
    }

    public void PaddleChooser(int paddle) //simple choose for the streamer
    {
        for(int i = 0; i < streamerPaddles.Count; i++)
        {
            if(streamerPaddles[i].activeInHierarchy == true && i != paddle)
            {
                streamerPaddles[i].SetActive(false);
            }
        }
        streamerPaddles[paddle].SetActive(true);
        streamerPaddleChoice = paddle;
    }

    public void ChatPaddleVotingTally(string vote, string chatter)
    {
        if (!activePlayers.Contains(chatter))
        {
            for (int i = 0; i < chatPaddles.Count; i++)
            {
                if (vote.Contains(i.ToString()))
                {
                    chatPaddleVoting[i]++;
                    votingTexts[i].text = "Votes: " + chatPaddleVoting[i].ToString();
                    break;
                }
            }
            activePlayers.Add(chatter); //only used for the voting, is cleared after voting done
        }
    }
}