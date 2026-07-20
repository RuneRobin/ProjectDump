using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;

    public float cooldown = 30;
    public float waveDuration = 3;
    public float waveSize = 10;
    private GameObject soundWave;
    private GameObject soundWavePrefab;

    // Start is called before the first frame update
    public void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        soundWavePrefab = Resources.Load("Sprites/Bullets/Soundwave") as GameObject;
        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }
        StartCoroutine(AttackCooldown());
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (soundWave != null)
        {
            //soundWave.transform.position = transform.position + new Vector3(0,0,-0.5f);
        }
    }

    public IEnumerator AttackCooldown()
    {
        soundWave = Instantiate(soundWavePrefab, transform.position, transform.rotation);
        soundWave.transform.localScale = Vector2.one * waveSize;
        soundWave.GetComponent<Soundwave>().duration = waveDuration;
        soundWave.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(cooldown * PlayerMovement.instance.moyaSupp);

        friendScript.jackBool = true;

        StartCoroutine(AttackCooldown());
    }

    #region Level Ups
    public void OnLevelUp()
    {
        currLevel++;

        if (friendScript.level == 1)
        {

        }
        else if (friendScript.level == 2)
        {


        }
        else if (friendScript.level == 3)
        {


        }
        else if (friendScript.level == 4)
        {


        }
        else if (friendScript.level == 5)
        {


        }
        else if (friendScript.level == 6)
        {


        }
        else if (friendScript.level == 7)
        {


        }
        else if (friendScript.level == 8)
        {

        }
    }
    #endregion

}
