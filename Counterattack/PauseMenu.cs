using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button resume;
    public Button options;
    public Button exit;


    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void Start()
    {
        resume.onClick.AddListener(Resume);
        options.onClick.AddListener(Options);
        exit.onClick.AddListener(ExitCurrentGame);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Options()
    {
        //lmao in a minute mate
    }

    public void ExitCurrentGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
        
    }

}
