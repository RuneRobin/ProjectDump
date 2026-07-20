using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YourTooClose : MonoBehaviour
{

    private RoomController rc;
    public float distance;
    public float timer;
    private float boundary;

    private void Awake()
    {
        rc = RoomController.instance;
    }

    void OnEnable()
    {
        //in case difficulty changes mid game
        timer = rc.guy19TooCloseTimer;
        boundary = rc.guy19TooCloseDistance;
        GetComponent<Image>().sprite = rc.guy19GloveSprites[Random.Range(0, rc.guy19GloveSprites.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
    }

    private void FixedUpdate()
    {
        if(distance <= boundary)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            RoomController.instance.GloveHasSlipped(transform.gameObject);
        }
    }
}
