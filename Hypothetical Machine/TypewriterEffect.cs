using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.1f;
    public string fullText;
    private string currentText = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        //StartCoroutine(ShowText());
    }

    public IEnumerator ShowText(string textObject)
    {
        for(int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i+1);
            gameObject.GetComponent<TextMeshProUGUI>().text = currentText; //change to wherever the text is
            yield return new WaitForSeconds(delay);
        }
    }
}
