using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClosetDoor : MonoBehaviour
{
    private bool open = false;
    private float timerToClose = 1f;

    private void FixedUpdate()
    {
        if(open == true)
        {
            timerToClose -= Time.deltaTime;
            if(timerToClose <= 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                open = false;
                timerToClose = 1f;
                SoundManager.instance.PlaySound(SoundType.GUY9, 1, 1);
            }
        }
    }

    public void OpenDoor()
    {
        if(open == false)
        {
            gameObject.transform.rotation = Quaternion.Euler(0,45,0);
            open = true;
            SoundManager.instance.PlaySound(SoundType.GUY9, 0, 1);
        }
    }
}
