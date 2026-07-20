using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenPopUp : MonoBehaviour
{
    public static ScreenPopUp instance;

    public int currRoom = 7;

    private GameObject currScreen;

    public RoomController rc; //get rid of

    public List<GameObject> screens;

    public bool holdingSB = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        screens[currRoom].gameObject.SetActive(true);
        currScreen = screens[7];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rc.allGuysActive[18] == true && rc.guy19GloveEscaped == true && transform.gameObject.activeInHierarchy == true) //if guy19 is active and the screen is up
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void ScreenUpDown()
    {
        if ((rc.allGuysActive[18] == false || (rc.allGuysActive[18] == true && rc.guy19GloveEscaped == false)) && rc.guy7BehindYou == false) //guy19 preventing screen from flipping up unless he isnt there then its fine
        {
            if (gameObject.activeInHierarchy == false)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void CameraChanger(int number)
    {
        if (currRoom != number)
        {
            //guy17 Anfor
            if (rc.allGuysActive[16] == true && rc.ScreenGameObject.transform.Find("Room" + currRoom + "Sprites").Find("Feather").transform.gameObject.activeInHierarchy == true)
            {
                rc.ScreenGameObject.transform.Find("Room" + currRoom + "Sprites").Find("Feather").transform.gameObject.SetActive(false);
            }

            currScreen.SetActive(false);
            currRoom = number;

            screens[currRoom].SetActive(true);
            currScreen = screens[currRoom];

            //also guy17
            if (rc.allGuysActive[16] == true)
            {
                rc.ChanceToFeather();
            }
        }
    }

}
