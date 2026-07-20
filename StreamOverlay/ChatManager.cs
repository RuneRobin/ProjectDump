using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using System;

public class ChatManager : MonoBehaviour
{
    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    string username = "justinfan1234";//"Rune_Robin";
    string password = "pass";//"oauth:b2fd210hehpl1yowbdf4ok0m00lorz";
    string channelName = "Rune_Robin"; //who's chat this will work for

    public GameObject square;
    public TMP_Text bubbleText;

    public GameObject speechBubble;
    public float stupidFix = 1f;//to stop the overlap of text and shit in the dumbest way possible

    public GameObject emotePrefab;
    public Vector2 randomStartPoint;

    //change font of speech bubble

    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    private void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 * :" + username);
        writer.WriteLine("JOIN #" + channelName);
        writer.WriteLine("CAP REQ :twitch.tv/tags");
        //writer.WriteLine("CAP REQ :twitch.tv/commands");
        //writer.WriteLine("CAP REQ :twitch.tv/membership");
        writer.Flush();

        Debug.Log("Connected to Twitch IRC");
    }

    // Update is called once per frame
    void Update()
    {
        if(twitchClient == null || !twitchClient.Connected)
        {
            Connect();
        }

        ReadChat();
    }
    void ReadChat()
    {
        if(twitchClient.Available > 0)
        {
            string message = reader.ReadLine();
            

            if (message.Contains("PRIVMSG"))
            {
                string[] msgSplit = message.Split(" PRIVMSG #");
                Dictionary<string, string> msgDictionary = new Dictionary<string, string>();
                List<string> allDataInMessage = new List<string>();

                allDataInMessage = new List<string>(msgSplit[0].Split(";", StringSplitOptions.None));

                for(int i = 0; i < allDataInMessage.Count; i++)
                {
                    //Debug.Log(allDataInMessage[i]);

                    if (allDataInMessage[i].StartsWith("user-type")) //stops before the line with the message
                    {
                        break;
                    }
                    string[] s = allDataInMessage[i].Split("=", 2);
                    msgDictionary.Add(s[0],s[1]);
                }

                #region Dictionary Key
                /* 
                * allDataInMessage[] 
                * 0: badge-info
                * 1: badges
                * 2: client-nonce
                * 3: color
                * 4: display-name
                * 5: emotes
                * 6: first-msg
                * 7: flags
                * 8: id
                * 9: mod
                * 10: returning-chatter
                * 11: room-id
                * 12: subscriber
                * 13: tmi-sent-ts
                * 14: turbo
                * 15: user-id
                * 16: user-type
                */
                #endregion

                //chatters name
                string chatName = msgDictionary["display-name"];

                //chatters message
                string chatMessage = msgSplit[1].Split(" :", 2)[1];

                RunFunction(chatName, chatMessage);

                //if the emotes list is not empty, get the emote texture
                if (msgDictionary["emotes"] != string.Empty)
                {
                    string[] stringSeparators = new string[] { "emotes=" };
                    string[] result = message.Split(stringSeparators, StringSplitOptions.None);
                    //split the emote string in case of multiple emotes
                    var splitPointAllEmotes = result[1].IndexOf(";", 0);
                    var allEmotes = result[1].Substring(0, splitPointAllEmotes);
                    string[] separateEmotes = allEmotes.Split('/');
                    //grab all emote textures
                    for (int i = 0; i < separateEmotes.Length; i++)
                    {
                        var id = separateEmotes[i].IndexOf(":", 0);
                        var emoteID = separateEmotes[i].Substring(0, id);
                        //1.0 / 2.0 / 3.0 is texture sizes
                        StartCoroutine(GetTexture("https://static-cdn.jtvnw.net/emoticons/v1/" + emoteID + "/3.0"));
                    }
                }
            }

            if(message.Contains("PING :tmi.twitch.tv"))
            {
                writer.WriteLine("PONG " + "tmi.twitch.tv" + "\r\n");
                writer.Flush();
            }

            
        }      
    }

    void RunFunction(string user, string msg)
    {
        //does things
        Debug.Log($"{user} said \"{msg}\"");

        Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Random.Range(Screen.width / 6, (Screen.width / 6)*5 ), 0, 0));
         
        Vector3 offset = new Vector3(0, 0, stupidFix);

        speechBubble.GetComponent<TextMeshPro>().text = user + ": " + msg;
        Instantiate(speechBubble, transform.position + screenPos + offset, transform.rotation);
        stupidFix = stupidFix + 0.01f;

        //Tic Tac Toe
        if (Regex.IsMatch(msg, @"^TTT[1-9]+"))
        {
            TicTacToe.instance.WriteSymbol(msg,user);
            Debug.Log("picked up");
        }

        //Pong
        if(Regex.IsMatch(msg,@"^!Ping") || Regex.IsMatch(msg, @"^!Pong"))
        {
            Pong.instance.ChatPaddleMover(user,msg);
        }
        else if(Regex.IsMatch(msg,@"^!Paddle[0-5]+") && Pong.instance.isSetup == true)
        {
            Pong.instance.ChatPaddleVotingTally(msg,user);
        }

        /*if(msg == "no")
        {
            square.SetActive(false);
        }
        if(msg == "yes")
        {
            square.SetActive(true);
        }*/ //this is for interacting with the square stuff

        //text.text = msg;
    }


    IEnumerator GetTexture(string url)
    {
        Debug.Log("ran coroutine");
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        Texture2D img = DownloadHandlerTexture.GetContent(www);
        randomStartPoint = UnityEngine.Random.onUnitSphere;
        GameObject emotePart = Instantiate(emotePrefab, randomStartPoint, Quaternion.identity);
        emotePart.GetComponent<ParticleSystem>().GetComponent<Renderer>().material.mainTexture = img;
        
    }


}
