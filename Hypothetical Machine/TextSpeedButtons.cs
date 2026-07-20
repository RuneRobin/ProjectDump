using SimpleTwineDialogue;
using UnityEngine;

public class TextSpeedButtons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextSpeed(float value)
    {
        TextAdventure.instance.delay += value;
        if (TextAdventure.instance.delay < 0f)
        {
            TextAdventure.instance.delay = 0f;
        }
        else if(TextAdventure.instance.delay > 0.1f)
        {
            TextAdventure.instance.delay = 0.1f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
