using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBeamBullet : MonoBehaviour
{
    public int speed = 10;
    private Transform parent;
    public EnemyBehaviour eB;

    public bool onTarget = false;

    private void Awake()
    {
        eB = gameObject.GetComponent<EnemyBehaviour>();
    }
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        StartCoroutine(TimeToDespawn());
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.left * Time.deltaTime * speed, Space.Self);
    }

    public IEnumerator TimeToDespawn()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if(eB.parried == true)
        {
            gameObject.transform.parent.GetComponent<SquareBeamGuy>().parriedCount++;
            gameObject.transform.parent.GetComponent<SquareBeamGuy>().rampUp = true;
        }
    }

}
