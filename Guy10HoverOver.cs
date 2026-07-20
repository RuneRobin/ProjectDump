using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Guy10HoverOver : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    private RoomController rc;
    public Sprite[] allSprites;
    public GameObject[] moveLocations;
    public GameObject currLocation;

    public int guy10Diff;
    private int guy10TimesBothered;
    private float guy10MoveCountdown;
    private float guy10StepTimer = 0f;
    private int guy10StepDirection = -1;
    private bool guy10Relocating = false;
    private bool guy10HoveredOver = false;

    public void Start()
    {
        rc = RoomController.instance;
        guy10Diff = rc.allDiffs[9];
        guy10MoveCountdown = 0.5f + (4.5f / 19f * (20f - guy10Diff)); //diff1 = 5 second intervals. diff20 = 0.5 second intervals
        guy10StepTimer = 0.01f + (0.49f / 19f * (20f - guy10Diff)); //diff1 = 0.5 second steps. diff20 = 0.01 second steps
        currLocation = moveLocations[0];
        gameObject.GetComponent<AudioSource>().Play();//plays keyboard noises
    }

    public void FixedUpdate()
    {
        if (guy10MoveCountdown > 0)
        {
            guy10MoveCountdown -= Time.deltaTime;
        }

        if(guy10MoveCountdown <= 0 && guy10Relocating == false) //change to moving
        {
            guy10Relocating = true;
            currLocation = moveLocations[Random.Range(0, moveLocations.Length)];
            if (guy10HoveredOver == false)
            {
                GetComponent<Image>().sprite = allSprites[2]; //Moving Sprite bother
            }
            else
            {
                GetComponent<Image>().sprite = allSprites[3]; //Moving Sprite
            }
            if(currLocation.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z); //flips image
            }
        }

        if (guy10Relocating == true && guy10HoveredOver == false) //Moving towards new location
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, currLocation.transform.position, Time.deltaTime * guy10Diff);

            if(gameObject.GetComponent<AudioSource>().isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Stop();//stops playing keyboard noises when relocating
            }

            if (guy10StepTimer >= 0)
            {
                guy10StepTimer -= Time.deltaTime;

                if(guy10StepTimer <= 0)
                {
                    int guy10Rotation = 0;

                    if(transform.rotation.z > 0)
                    {
                        guy10Rotation = 0;
                        guy10StepDirection = -1;
                    }
                    else if(transform.rotation.z == 0)
                    {
                        guy10Rotation = 14;
                    }
                    else if(transform.rotation.z < 0)
                    {
                        guy10Rotation = 0;
                        guy10StepDirection = 1;
                    }

                    transform.rotation = Quaternion.Euler(new Vector3(0,0,guy10Rotation * guy10StepDirection));
                    guy10StepTimer = 0.01f + (0.49f / 19f * (20f - guy10Diff)); //diff1 = 0.5 second steps. diff20 = 0.01 second steps
                }
            }
            
            if (transform.position == currLocation.transform.position)
            {
                guy10Relocating = false;
                guy10MoveCountdown = 0.5f + (4.5f / 19f * (20f - guy10Diff)); //diff1 = 5 second intervals. diff20 = 0.5 second intervals
                gameObject.GetComponent<AudioSource>().Play();//plays keyboard noises
                GetComponent<Image>().sprite = allSprites[0]; //back turned
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0)); //unrotates just in case
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); //flips image back again if flipped in the first place
                }
            }
        } 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlaySound(SoundType.GUY10, 0, 1); //plays bother sound
        guy10TimesBothered++;
        guy10HoveredOver = true;
        if(guy10TimesBothered >= rc.guy10Patience)
        {
            rc.NowImGettingRealPissedOff();
            guy10TimesBothered = 0;
        }

        if (guy10Relocating == false)
        {
            gameObject.GetComponent<Image>().sprite = allSprites[1]; //back turned bother
            gameObject.GetComponent<AudioSource>().Stop();//stops playing keyboard noises
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = allSprites[3]; //walk bother
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        guy10HoveredOver = false;
        if (guy10Relocating == false)
        {
            gameObject.GetComponent<Image>().sprite = allSprites[0]; //back turned
            gameObject.GetComponent<AudioSource>().Play();//plays keyboard noises
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = allSprites[2]; //walk
        }
    }
}
