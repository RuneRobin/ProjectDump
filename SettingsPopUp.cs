using UnityEngine;
using UnityEngine.UI;

public class SettingsPopUp : MonoBehaviour
{
    public Slider[] sliders;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        sliders[0].value = Master.instance.musicLevel;
        sliders[1].value = Master.instance.soundLevel;
        sliders[2].value = Master.instance.funniLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettingsChanged(GameObject setting)
    {
        if (setting.name == "Music")
        {
            Master.instance.musicLevel = setting.GetComponent<Slider>().value;
        }
        else if (setting.name == "Sound")
        {
            Master.instance.soundLevel = setting.GetComponent<Slider>().value;
        }
        else if (setting.name == "Funni")
        {
            Master.instance.funniLevel = setting.GetComponent<Slider>().value;
        }
        else if (setting.name == "Settings")
        {
            setting.GetComponent<AudioSource>().volume = Master.instance.soundLevel/100;
        }

    }
}
