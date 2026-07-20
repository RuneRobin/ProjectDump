using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Library : MonoBehaviour
{
    public List<string> libraryText = new List<string>(); //Every single line in the game
    public List<List<int>> libraryTextOrder = new List<List<int>>(); //the group of lines chosen per choice
    public List<List<int>> libraryBookmarks = new List<List<int>>(); //the numbers correlating to the choices you can go to



    public Dictionary<int, List<List<int>>> bookmarks = new Dictionary<int, List<List<int>>>();//int is the key or "name" of the bookmark, the first List in the int List List is the order
                                                                                               //of the text that is to be displayed, with element 0 being the main text, and the rest being
                                                                                               //the button texts, the 2nd List determines which bookmarks are attached to which buttons
    public static Library instance;

    //Typewriter stuff
    public float delay = 0.01f;
    //public string fullText;
    private string currentText = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        
        //HypotheticalDisplayRandomizer(); //this is for the above 4 to display a random set of hypotheticals each time
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Main Text/Button Logic displayer with typewriter effect (DO NOT TOUCH)
    public void CurrentChoice(int DictKey)
    {
        StopAllCoroutines(); //stops the typewriter coroutines from happening in the background after a button is pressed.

        if (bookmarks.ContainsKey(DictKey))
        {
            StartCoroutine(ShowScreenText(Master.instance.mainScreenText, libraryText[bookmarks[DictKey].First().First()], DictKey));
        }
        else
        {
            Debug.Log("Dictionary Key Not Found");
        }

        foreach(GameObject btn in Master.instance.buttons)
        {
            btn.SetActive(false);
        }

    }

    public IEnumerator ShowScreenText(TextMeshProUGUI textObject, string fullText, int DictKey)
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);
            textObject.text = currentText;
            yield return new WaitForSeconds(delay);
        }

        for (int i = 0; i < bookmarks[DictKey].First().Count-1; i++)
        {
            //int k = bookmarks[DictKey][i];

            StartCoroutine(ShowButtonText(Master.instance.buttons[i], libraryText[bookmarks[DictKey][0][i+1]], bookmarks[DictKey][1][i]));
        }

        yield return null;
    }

    public IEnumerator ShowButtonText(GameObject btn, string fullText, int newDictKey)
    {
        btn.SetActive(true);
        btn.GetComponent<Button>().onClick.RemoveAllListeners();

        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }

        
        btn.GetComponent<Button>().onClick.AddListener(() => CurrentChoice(newDictKey));
    }
    #endregion

    public void HypotheticalDisplayRandomizer()
    {
        #region List maker and shuffler
        List<int> r = new List<int>();
        for (int j = 9; j < 30; j++)
        {
            r.Add(j);
        } //makes a list of 9-29 for the 21 hypotheticals

        //list shuffler
        for (int t = 0; t < r.Count; t++)
        {
            int tmp = r[t];
            int y = UnityEngine.Random.Range(t, r.Count);
            r[t] = r[y];
            r[y] = tmp;
        }
        #endregion

        for (int i = 1; i < 5; i++)
        {
            if (!bookmarks.ContainsKey(i))
            {
                libraryTextOrder.Add(new List<int>() { i+4, r[0], r[1], r[2], r[3] });
                libraryBookmarks.Add(new List<int>() { r[0] - 4, r[1] - 4, r[2] - 4, r[3] - 4 });
                bookmarks.Add(i, new List<List<int>>() { libraryTextOrder[i], libraryBookmarks[i] });
            }
            else
            {
                bookmarks[i][0] = new List<int>() { i + 4, r[0], r[1], r[2], r[3] };
                bookmarks[i][1] = new List<int>() { r[0] - 4, r[1] - 4, r[2] - 4, r[3] - 4 };
            }
        }
    }

}
