using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpecialEffects : MonoBehaviour
{
    public List<Text> textToRainbowEffect = new List<Text>();

    private Color32[] colors;
    // Start is called before the first frame update
    void Start()
    {
        colors = new Color32[5]
        {
                new Color(255,0,0,255),
                new Color(0,255,0,255),
                new Color(0,0,255,255),
                new Color(255,128,0,255),
                new Color(128,0,255,255)
        };

        for (int i = 0; i < textToRainbowEffect.Count; i++)
        {
            StartCoroutine(Cycle(textToRainbowEffect[i], i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Cycle(Text rainbowText, int offset) //For elemental hue effect
    {
        int i = 0 + offset;
        while (true)
        {
            for (float interlopant = 0f; interlopant < 1f; interlopant += 0.001f)
            {
                rainbowText.color = Color.Lerp(colors[i % 5], colors[(i + 1) % 5], interlopant);
                yield return null;
            }
            i++;
        }

    }
}
