using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SloopyVonTrigletop : MonoBehaviour
{
    private RunnerScript runScript;

    private float normalSpeed;
    public float speedBoost = 0;


    // Start is called before the first frame update
    void Awake()
    {
        runScript = gameObject.GetComponent<RunnerScript>();
        normalSpeed = runScript.speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speedBoost += Time.deltaTime * 5;
        runScript.speed = normalSpeed + speedBoost;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        runScript.speed = normalSpeed;
        speedBoost = 0;
    }
}
