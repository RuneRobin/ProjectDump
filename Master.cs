using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Master : MonoBehaviour
{
    public static Master instance;

    //settings
    public float musicLevel;
    public float soundLevel;
    public float funniLevel;
    public int totalScore;

    public int[] masterDiffs;

    public List<AudioResource> MusicTracks = new List<AudioResource>();
    public float musicTime;
    private int currTrack = 10;

    public string loseText;

    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            transform.GetChild(0).gameObject.GetComponent<AudioSource>().resource = MusicTracks[10];
            transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void Update()
    {
        musicTime = transform.GetChild(0).gameObject.GetComponent<AudioSource>().time;

        if(transform.GetChild(0).gameObject.GetComponent<AudioSource>().volume != musicLevel/100)
        {
            transform.GetChild(0).gameObject.GetComponent<AudioSource>().volume = musicLevel/100;
        }

        #region ScreenPopUp music changing
        if (ScreenPopUp.instance)
        {
            if(ScreenPopUp.instance.currRoom != currTrack && ScreenPopUp.instance.gameObject.activeInHierarchy == true)
            {
                if (ScreenPopUp.instance.currRoom != 8) //every room except 8, as 8 does it's own thing with the music
                {
                    transform.GetChild(0).gameObject.GetComponent<AudioSource>().resource = MusicTracks[ScreenPopUp.instance.currRoom];
                    transform.GetChild(0).gameObject.GetComponent<AudioSource>().time = musicTime;
                    transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                    currTrack = ScreenPopUp.instance.currRoom;
                }
                else if (ScreenPopUp.instance.currRoom == 8 && ScreenPopUp.instance.gameObject.activeInHierarchy == true) //Room 8 specifically
                {   //if Key's music taste isnt the same as the current track and the song playing isnt bad, change to bad
                    if (RoomController.instance.guy14CurrentTrack != RoomController.instance.guy14MusicTaste && transform.GetChild(0).gameObject.GetComponent<AudioSource>().resource != MusicTracks[11])
                    {
                        transform.GetChild(0).gameObject.GetComponent<AudioSource>().resource = MusicTracks[11];
                        transform.GetChild(0).gameObject.GetComponent<AudioSource>().time = musicTime;
                        transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                        currTrack = ScreenPopUp.instance.currRoom;
                    }
                    else if (RoomController.instance.guy14CurrentTrack == RoomController.instance.guy14MusicTaste)
                    {
                        transform.GetChild(0).gameObject.GetComponent<AudioSource>().resource = MusicTracks[ScreenPopUp.instance.currRoom];
                        transform.GetChild(0).gameObject.GetComponent<AudioSource>().time = musicTime;
                        transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                        currTrack = ScreenPopUp.instance.currRoom;
                    }
                }
            }
            else if(ScreenPopUp.instance.gameObject.activeInHierarchy == false && currTrack != 10)
            {
                transform.GetChild(0).gameObject.GetComponent<AudioSource>().resource = MusicTracks[10];
                transform.GetChild(0).gameObject.GetComponent<AudioSource>().time = musicTime;
                transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                currTrack = 10;
            }
        }
        #endregion

        #region Spacebar flip screen
        if (Input.GetKey(KeyCode.Space) && ScreenPopUp.instance.holdingSB == false)
        {
            ScreenPopUp.instance.ScreenUpDown();
            ScreenPopUp.instance.holdingSB = true;
        }
        if(Input.GetKeyUp(KeyCode.Space) && ScreenPopUp.instance.holdingSB == true)
        {
            ScreenPopUp.instance.holdingSB = false;
        }
        #endregion
    }
}
