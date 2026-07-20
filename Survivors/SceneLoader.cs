using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public void LoadMainScene() //Can make multiple of these and assign them to each button as necessary
    {
        Master.instance.moneyCollectedDuringRun = 0;
        Master.instance.enemiesDefeated = 0;
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadPermMenu()
    {
        SceneManager.LoadScene("Permanent Upgrade Menu");
    }

    public void LoadCharSelect()
    {
        SceneManager.LoadScene("Character Select");
    }

}
