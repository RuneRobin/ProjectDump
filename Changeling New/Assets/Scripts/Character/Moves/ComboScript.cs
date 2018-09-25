using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboScript : MonoBehaviour {

    public GameObject player;
    public GameObject gameMaster;
    public Sprite skeleton;
    public Sprite zombie;
    public Sprite vampire;
    public int comboLevel = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown("a") && comboLevel == 0)
        {
            FirstAttack();
            comboLevel++;
        }
        else if(Input.GetKeyDown("a") && comboLevel == 1)
        {
            StopAllCoroutines();
            SecondAttack();
            comboLevel++;
        }
        else if (Input.GetKeyDown("a") && comboLevel == 2)
        {
            StopAllCoroutines();
            ThirdAttack();
            comboLevel++;
        }
        else if (Input.GetKeyDown("a") && comboLevel == 3)
        {
            StopAllCoroutines();
            FourthAttack();
            comboLevel++;
        }
        else if (Input.GetKeyDown("a") && comboLevel == 4)
        {
            StopAllCoroutines();
            FifthAttack();
            comboLevel++;
        }

    }

    public void FirstAttack()
    {
        Debug.Log("First Attack");

        if (gameMaster.GetComponent<ComboList>().comboOne == 0)
        {
            player.GetComponent<SpriteRenderer>().sprite = skeleton;
        }
        else if (gameMaster.GetComponent<ComboList>().comboOne == 1)
        {
            player.GetComponent<SpriteRenderer>().sprite = zombie;
        }
        else if (gameMaster.GetComponent<ComboList>().comboOne == 2)
        {
            player.GetComponent<SpriteRenderer>().sprite = vampire;
        }

        StartCoroutine(AttackCooldown());

    }

    public void SecondAttack()
    {
        Debug.Log("Second Attack");

        if (gameMaster.GetComponent<ComboList>().comboTwo == 0)
        {
            player.GetComponent<SpriteRenderer>().sprite = skeleton;
        }
        else if (gameMaster.GetComponent<ComboList>().comboTwo == 1)
        {
            player.GetComponent<SpriteRenderer>().sprite = zombie;
        }
        else if (gameMaster.GetComponent<ComboList>().comboTwo == 2)
        {
            player.GetComponent<SpriteRenderer>().sprite = vampire;
        }

        StartCoroutine(AttackCooldown());
    }

    public void ThirdAttack()
    {
        Debug.Log("Third Attack");

        if (gameMaster.GetComponent<ComboList>().comboThree == 0)
        {
            player.GetComponent<SpriteRenderer>().sprite = skeleton;
        }
        else if (gameMaster.GetComponent<ComboList>().comboThree == 1)
        {
            player.GetComponent<SpriteRenderer>().sprite = zombie;
        }
        else if (gameMaster.GetComponent<ComboList>().comboThree == 2)
        {
            player.GetComponent<SpriteRenderer>().sprite = vampire;
        }

        StartCoroutine(AttackCooldown());
    }

    public void FourthAttack()
    {
        Debug.Log("Fourth Attack");

        if (gameMaster.GetComponent<ComboList>().comboFour == 0)
        {
            player.GetComponent<SpriteRenderer>().sprite = skeleton;
        }
        else if (gameMaster.GetComponent<ComboList>().comboFour == 1)
        {
            player.GetComponent<SpriteRenderer>().sprite = zombie;
        }
        else if (gameMaster.GetComponent<ComboList>().comboFour == 2)
        {
            player.GetComponent<SpriteRenderer>().sprite = vampire;
        }

        StartCoroutine(AttackCooldown());
    }

    public void FifthAttack()
    {
        Debug.Log("Fifth Attack");

        if (gameMaster.GetComponent<ComboList>().comboFive == 0)
        {
            player.GetComponent<SpriteRenderer>().sprite = skeleton;
        }
        else if (gameMaster.GetComponent<ComboList>().comboFive == 1)
        {
            player.GetComponent<SpriteRenderer>().sprite = zombie;
        }
        else if (gameMaster.GetComponent<ComboList>().comboFive == 2)
        {
            player.GetComponent<SpriteRenderer>().sprite = vampire;
        }

        StartCoroutine(AttackCooldown());
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PlayerScript>().playerSprite;
        comboLevel = 0;
        StopAllCoroutines();
    }
}
