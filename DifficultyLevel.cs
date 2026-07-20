using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultyLevel : MonoBehaviour
{
    public int[] diffArray = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

    public Text[] diffTexts;

    public GameObject guy15Icon;
    public Sprite[] guy15Animations;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Guy15Animate(0));
    }

    private void Awake()
    {
        Master.instance.totalScore = 0;
        if (Master.instance && Master.instance.masterDiffs.Length > 1)
        {
            for (int i = 0; i < diffArray.Length; i++)
            {
                diffArray[i] = Master.instance.masterDiffs[i];
                diffTexts[i].text = "Level: " + diffArray[i];
                Master.instance.totalScore += 100 * diffArray[i];
            }
        }
    }

    public void LevelUp(int i)
    {
        i--; //cuz we hate starting at 0 for this one.
        if(diffArray[i] < 20)
        {
            diffArray[i]++;
            diffTexts[i].text = "Level: " + diffArray[i];
            Master.instance.totalScore += 100;
        }
    }

    public void LevelDown(int i)
    {
        i--; //cuz we hate starting at 0 for this one.

        if (diffArray[i] > 0)
        {
            diffArray[i]--;
            diffTexts[i].text = "Level: " + diffArray[i];
            Master.instance.totalScore -= 100;
        }
    }

    public void AllLevelUp()
    {
        for (int i = 0; i < diffArray.Length; i++)
        {
            if (diffArray[i] < 20)
            {
                diffArray[i]++;
                diffTexts[i].text = "Level: " + diffArray[i];
                Master.instance.totalScore += 100;
            }
        }
    }

    public void AllLevelDown()
    {
        for (int i = 0; i < diffArray.Length; i++)
        {
            if (diffArray[i] > 0)
            {
                diffArray[i]--;
                diffTexts[i].text = "Level: " + diffArray[i];
                Master.instance.totalScore -= 100;
            }
        }
    }

    public void BeginDay()
    {
        Master.instance.masterDiffs = diffArray;
        SoundManager.instance.PlaySound(SoundType.GUI, 0, 1);
        SceneManager.LoadScene("MainGame");
    }



    public IEnumerator Guy15Animate(int frame)
    {
        guy15Icon.GetComponent<Image>().sprite = guy15Animations[frame];

        frame++;

        yield return new WaitForSeconds(0.035f);

        if (frame != guy15Animations.Length)
        {
            StartCoroutine(Guy15Animate(frame));
        }

    }
}
