using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletScript : MonoBehaviour
{
    public string enemyFaction = "aa";
    public GameObject source;

    private UnitScript us;

    //for elemental hue effect
    private SpriteRenderer col;
    private Color32[] colors;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        us = source.GetComponent<UnitScript>();

        SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
        if (us.faction == "Undead")
        {
            spr.sprite = MasterController.instance.bulletSprites[0];
        }
        else if (us.faction == "Highlander")
        {
            spr.sprite = MasterController.instance.bulletSprites[1];
        }
        else if (us.faction == "Elemental")
        {
            spr.sprite = MasterController.instance.bulletSprites[2];

            col = gameObject.GetComponent<SpriteRenderer>();
            colors = new Color32[7]
            {
                new Color(255,0,0,255),
                new Color(255,165,0,255),
                new Color(255,255,0,255),
                new Color(0,255,0,255),
                new Color(0,0,255,255),
                new Color(75,0,130,255),
                new Color(238,130,238,255)
            };

            StartCoroutine(Cycle());
        }
        else if (us.faction == "Circus")
        {
            spr.sprite = MasterController.instance.bulletSprites[3];
        }
        else if (us.faction == "Robot")
        {
            spr.sprite = MasterController.instance.bulletSprites[4];
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<UnitScript>() && collision.gameObject.GetComponent<UnitScript>().army == enemyFaction)
        {
            us.OnHitting(collision.gameObject.GetComponent<UnitScript>());
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Cycle() //For elemental hue effect
    {
        int i = 0;
        while(true)
        {
            for(float interlopant = 0f; interlopant < 1f; interlopant+= 0.01f)
            {
                col.color = Color.Lerp(colors[i%7], colors[(i + 1)%7], interlopant);
                yield return null;
            }
            i++;
        }
        
    }
}
