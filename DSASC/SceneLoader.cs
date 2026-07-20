using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string name)
    {
        if(Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().isPlaying == false)
        {
            Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().resource = Master.instance.MusicTracks[10];
            Master.instance.musicTime = 0;
            Master.instance.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
            Debug.Log("Started song again");
        }
        SoundManager.instance.PlaySound(SoundType.GUI, 0, 1);

        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        SoundManager.instance.PlaySound(SoundType.GUI, 0, 1);
        Application.Quit();
    }
}
