using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlantScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public float moisture = 90f;
    //private bool hovering = false;
    //private float wilterRate = 0.001f;
    //private int wilterLevel = 0;
    //public float wilterMultiTimer = 10f;
    //public float wilterBonusWilter = 0;
    //private GameObject wilterLevelGameObject;

    private RoomController rc;
    public int plantNumber;

    void Start()
    {
        rc = RoomController.instance;
        /*moisture -= Random.Range(0f, 15f);
        wilterLevelGameObject = transform.GetChild(0).transform.gameObject;
        */
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        #region Plant Code (in case I need to put it back here)
        /*wilterMultiTimer -= Time.deltaTime;

        moisture -= rc.allDiffs[10] * wilterRate;

        if(wilterMultiTimer <= 0)
        {
            moisture -= wilterBonusWilter;
            wilterBonusWilter += 0.00005f;
        }

        if (hovering == true && Input.GetMouseButton(0)) //Change to M1
        {
            moisture += 0.5f;
            wilterMultiTimer = 10f;
            wilterBonusWilter = 0;
        }

        #region wilter level based on moisture
        if (moisture >= 66 && wilterLevel != 0)
        {
            if (ScreenPopUp.instance.currRoom == 1)
            {
                SoundManager.instance.PlaySound(SoundType.GUY11, 0, 1); //plays whistle going up
            }

            wilterLevel = 0;
            wilterLevelGameObject.SetActive(false);
            transform.GetChild(wilterLevel).gameObject.SetActive(true);
            wilterLevelGameObject = transform.GetChild(wilterLevel).transform.gameObject;
        }
        else if(moisture < 66 && moisture >= 33 && wilterLevel != 1)
        {
            if (ScreenPopUp.instance.currRoom == 1)
            {
                if (wilterLevel == 2) //the level went up
                {
                    SoundManager.instance.PlaySound(SoundType.GUY11, 0, 1); //plays whistle going up
                }
                else if (wilterLevel == 0) //the level went down
                {
                    SoundManager.instance.PlaySound(SoundType.GUY11, 1, 1); //plays whistle going down
                }
            }

            wilterLevel = 1;
            wilterLevelGameObject.SetActive(false);
            transform.GetChild(wilterLevel).gameObject.SetActive(true);
            wilterLevelGameObject = transform.GetChild(wilterLevel).transform.gameObject;
        }
        else if(moisture < 33 && wilterLevel != 2)
        {
            if (ScreenPopUp.instance.currRoom == 1)
            {
                SoundManager.instance.PlaySound(SoundType.GUY11, 1, 1); //plays whistle going down
            }

            wilterLevel = 2;
            wilterLevelGameObject.SetActive(false);
            transform.GetChild(wilterLevel).gameObject.SetActive(true);
            wilterLevelGameObject = transform.GetChild(wilterLevel).transform.gameObject;
        }
        #endregion

        if (moisture > 100 || moisture < 0)
        {
            Debug.Log("One of the plants has died, why weren't you helping Ryy? Game Over");
            SceneManager.LoadScene("Game Over");
        }*/
        #endregion
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rc.guy11Hovering[plantNumber] = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rc.guy11Hovering[plantNumber] = false;
    }
}
