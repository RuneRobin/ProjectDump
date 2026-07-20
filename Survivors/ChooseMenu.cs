using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMenu : MonoBehaviour
{
    public Canvas canvas;

    public Button newFriend;
    public Button newItem;
    public Button advanceStage;

    // Start is called before the first frame update
    public void Start()
    {
        newFriend.onClick.AddListener(NewFriend);
        newItem.onClick.AddListener(NewItem);
        advanceStage.onClick.AddListener(AdvanceStage);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;  
    }

    public void NewFriend()
    {
        canvas.gameObject.transform.Find("Level Up Menu").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void NewItem()
    {
        canvas.gameObject.transform.Find("Item Menu").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void AdvanceStage()
    {
        //canvas.gameObject.transform.Find("Advance Stage Menu").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
