using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robin : MonoBehaviour
{
    public Friend friendScript;
    public int currLevel = 0;
    
    public float cooldown = 1;
    public float featherVolley = 5;
    public float featherSpeed = 10;
    public Vector3 featherDirection;
    private GameObject feather;
    private GameObject featherPrefab;

    // Start is called before the first frame update
    public void Start()
    {
        friendScript = gameObject.GetComponent<Friend>();
        friendScript.swapScript = this;
        featherPrefab = Resources.Load("Sprites/Bullets/Feather") as GameObject;
        for (int i = 0; i < friendScript.level; i++)
        {
            OnLevelUp(); //for when Jack gets the script
        }
        StartCoroutine(AttackCooldown());
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        //float vertical = Input.GetAxis("Vertical"); //input directions
        //float horizontal = Input.GetAxis("Horizontal");
    }

    public IEnumerator AttackCooldown()
    {
        int count = Mathf.RoundToInt(featherVolley * PlayerMovement.instance.tofuSupp);
        for (var i = 0; i < count; i++)
        {
            float angle = i * 360 / count;
            feather = Instantiate(featherPrefab, transform.position, transform.rotation);
            
            Rigidbody2D featherRigidbody = feather.GetComponent<Rigidbody2D>();
            featherDirection = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0); //rotates bullet in angle its aimed at

            featherRigidbody.rotation = -angle;
            featherRigidbody.velocity = featherDirection * featherSpeed; //speed of feathers
            yield return new WaitForSeconds(1f / count);
        }

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
