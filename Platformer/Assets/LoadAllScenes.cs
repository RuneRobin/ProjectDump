using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAllScenes : MonoBehaviour {

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadControls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Demo");
    }


}
