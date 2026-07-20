using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public Text vt;
    public GameObject button;
    public int score;
    public float scoreSpeed;
    public GameObject scoreTitles;
    private int milestone = 4750;
    private int milestoneNumber = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreSpeed = Mathf.Clamp(Master.instance.totalScore / 400,1,99999); //50 calls per second on FixedUpdate, 400 would be 8 seconds which is roughly the timing of the fanfare jingle
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (score < Master.instance.totalScore)
        {
            score += Mathf.CeilToInt(scoreSpeed);
            if(score > Master.instance.totalScore) //for when the rounding up takes it over the actual score
            {
                score = Master.instance.totalScore;
            }
            vt.text = score.ToString();
        }

        if(score >= milestone)
        {
            if (milestoneNumber > -1)
            {
                scoreTitles.transform.GetChild(milestoneNumber).transform.gameObject.SetActive(false);
            }
            milestoneNumber++;
            scoreTitles.transform.GetChild(milestoneNumber).transform.gameObject.SetActive(true);

            milestone += 4750;
        }

        if(score == Master.instance.totalScore)
        {
            button.SetActive(true);
        }

    }

    public void BackToMenu()
    {
        SoundManager.instance.PlaySound(SoundType.GUI, 0, 1);
        if (Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().isPlaying == false)
        {
            Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().resource = Master.instance.MusicTracks[10];
            Master.instance.musicTime = 0;
            Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
            Debug.Log("Started song again");
        }
        SceneManager.LoadScene("Main Menu");
    }
}
