using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FriendGridScript : MonoBehaviour
{

    public GameObject[] friendPlacement;

    public Vector2 lastPlacement;
    public GameObject currentlyHolding;

    public Sprite spriteToSwap;
    public Sprite spriteToSwap2;
    public Sprite tempHolder;

    public GameObject gameObjectToSwap;
    public GameObject gameObjectToSwap2;

    
    //Add(default(T)) where T is the type
    //Arrays have set sizes
    //Lists are dynamic


    // Start is called before the first frame update
    public void Awake()
    {
        foreach (GameObject button in friendPlacement) 
        {
            button.GetComponent<Button>().onClick.AddListener(() => { GrabbingOrPlacing(button.gameObject); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentlyHolding != null)
        {
            currentlyHolding.transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 1);
        }

        if (Input.GetKeyDown("h"))
        {
            gameObject.SetActive(false);
        }
    }

    public void OnEnable()
    {
        Time.timeScale = 0;

        int count = 1;

        foreach (GameObject button in friendPlacement) //the 16 spots preset on the menu to have their sprites replaced with whatever friend is designated there in-game
        {
            if (PlayerMovement.instance.friendArray[count] != null)
            {
                button.GetComponent<Image>().sprite = PlayerMovement.instance.friendArray[count].GetComponent<SpriteRenderer>().sprite;
                count++;
            }
        }
    }

    public void GrabbingOrPlacing(GameObject buttonPressed) //For the buttons to share
    {
        bool no2ndrun = false; //to stop it from doing the other one immediately afterwards
        int index = 0;
        int index2 = 0;

        if (currentlyHolding != null)
        {
            no2ndrun = true;
            spriteToSwap2 = buttonPressed.GetComponent<Image>().sprite;

            tempHolder = spriteToSwap; //temp stores first image
            spriteToSwap = spriteToSwap2; //first image becomes second
            spriteToSwap2 = tempHolder; //second image becomes first

            buttonPressed.GetComponent<Image>().sprite = spriteToSwap2;
            currentlyHolding.GetComponent<Image>().sprite = spriteToSwap;
            currentlyHolding.GetComponent<Image>().raycastTarget = true; //reset so it cant be clicked through

            foreach (GameObject friend in PlayerMovement.instance.friendArray)
            {
                if (friend != null)
                {
                    if (friend.GetComponent<SpriteRenderer>().sprite == spriteToSwap) //The 2nd clicked. Sprites got swapped dummy
                    {
                        gameObjectToSwap2 = friend;
                        index2 = System.Array.IndexOf(PlayerMovement.instance.friendArray, friend);
                    }

                    if (friend.GetComponent<SpriteRenderer>().sprite == spriteToSwap2) //The 1st clicked
                    {
                        gameObjectToSwap = friend;
                        index = System.Array.IndexOf(PlayerMovement.instance.friendArray, friend);
                    }
                }
            }

            PlayerMovement.instance.friendArray[index] = gameObjectToSwap2;
            PlayerMovement.instance.friendArray[index2] = gameObjectToSwap;

            //currentlyHolding.transform.position = buttonPressed.transform.position;
            //buttonPressed.transform.position = lastPlacement; //swap places

            currentlyHolding.transform.position = lastPlacement;

            currentlyHolding = null;

        }

        if (no2ndrun == false && currentlyHolding == null)
        {
            Debug.Log(buttonPressed.name);
            lastPlacement = buttonPressed.transform.position; //the original position of what you are grabbing right now
            currentlyHolding = buttonPressed; //the current object you are holding
            spriteToSwap = buttonPressed.GetComponent<Image>().sprite;
            buttonPressed.GetComponent<Image>().raycastTarget = false; //so you can click through it
        }

        
    }

    public void OnDisable()
    {
        Time.timeScale = 1;
    }
}
