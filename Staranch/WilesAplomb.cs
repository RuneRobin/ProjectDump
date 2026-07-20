using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilesAplomb : MonoBehaviour
{
    public GameObject currTarget;
    public List<GameObject> targets;

    public int absolution = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject racer in GameObject.FindGameObjectsWithTag("Racer"))
        {
            if (racer != gameObject && racer.layer != 7) //7 is BigGame
            {
                targets.Add(racer);
            }
        }
        NewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(absolution >= 5)
        {
            absolution -= 5;
            gameObject.GetComponent<RunnerScript>().speed++;
        }
    }

    public void NewTarget()
    {
        currTarget = targets[Random.Range(0, targets.Count-1)];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == currTarget)
        {
            absolution++;
            NewTarget();
        }
    }
}
