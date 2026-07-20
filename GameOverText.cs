using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    public Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = Master.instance.loseText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
