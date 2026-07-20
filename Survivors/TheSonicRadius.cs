using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSonicRadius : MonoBehaviour
{
    public float cooldown = 2;
    public Transform originPoint;
    public float radius = 5;
    public float SPEED = 1;

    private Vector3 pos;

    private Vector3 start;
    private Vector3 end;

    private bool onCooldown = false;

    // Start is called before the first frame update
    public void Start()
    {
        SetPositions();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onCooldown)
        {
            originPoint = PlayerMovement.instance.transform;

            pos += Vector3.Normalize(end - start) * Time.deltaTime * SPEED;

            transform.position = originPoint.position + pos;

            if (pos.magnitude > radius)
            {
                onCooldown = true;
                StartCoroutine(AttackCooldown());
            }
        }
    }


    public IEnumerator AttackCooldown()
    {
        
        yield return new WaitForSeconds(cooldown);
        SetPositions();
       
    }

    public void SetPositions()
    {
        float PointA = Random.Range(0, Mathf.PI * 2);
        //float PointB = Random.Range(Random.Range(PointA + 90, PointA - 90), Random.Range(PointA + 180, PointA - 180));
        float PointB = PointA + Random.Range(Mathf.PI/2, Mathf.PI * 1.5f);

        float xA = Mathf.Cos(PointA);
        float yA = Mathf.Sin(PointA);

        float xB = Mathf.Cos(PointB);
        float yB = Mathf.Sin(PointB);

        start = new Vector3(xA, yA, 0) * radius;
        end = new Vector3(xB, yB, 0) * radius;

        pos = start;

        onCooldown = false;
    }

}