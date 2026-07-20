using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void ButtonPressed(string btnPressed)
    {
        if(btnPressed == "Start")
        {
            SceneConst.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(SceneConst.instance.SFXCollection[0]);
            SceneManager.LoadScene("Main Game");
            if(GameObject.Find("SceneConstObject"))
            {
                SceneConst.instance.autoStart = false;
            }
        }
        else if (btnPressed == "AutoStart")
        {
            SceneConst.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(SceneConst.instance.SFXCollection[1]);
            SceneManager.LoadScene("Main Game");
            if (GameObject.Find("SceneConstObject"))
            {
                SceneConst.instance.autoStart = true;
            }
        }
        else if (btnPressed == "Quit")
        {
            Application.Quit();
        }
    }

}
