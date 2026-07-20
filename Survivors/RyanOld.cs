using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyanOld : Friend
{
    public float throwDuration = 2;
    public float retreatDuration = 5;
    public GameObject spearPoint;
    private Vector2 hitboxOffset = new Vector2(3,0);
    public Vector2 reach = new Vector2(3,0);


    private Vector2 start;
    private Vector2 end;
    private Vector2 flip = new Vector2(-1, 0);
    private bool isRight = true;

    // Start is called before the first frame update
    public override void FriendInit()
    {
        spearPoint = Instantiate(spearPoint, (Vector2)transform.position, transform.rotation);
        spearPoint.transform.position = (Vector2)spearPoint.transform.position + hitboxOffset;
        spearPoint.transform.parent = gameObject.transform;
        StartCoroutine(AttackCooldown());

    }


    public override void FriendUpdate()
    {
        //start = (Vector2)transform.position + hitboxOffset;
        //end = start + reach;

        if (Input.GetKey("d") && isRight == false)
        {
                start *= flip;
                end *= flip;
                hitboxOffset *= flip;
                reach *= flip;
                spearPoint.transform.localPosition *= flip;

                isRight = true;
        }
        else if(Input.GetKey("a") && isRight == true)
        {
                start *= flip;
                end *= flip;
                hitboxOffset *= flip;
                reach *= flip;
                spearPoint.transform.localPosition *= flip;

                isRight = false;
        }

    }

    public IEnumerator AttackCooldown()
    {
        float elapsedTime = 0;
        start = hitboxOffset;
        end = start + reach;

        while (elapsedTime < throwDuration)
        {
            spearPoint.transform.position = (Vector2)transform.position + Vector2.Lerp(start, end, elapsedTime/throwDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        spearPoint.transform.position = end;
        

        elapsedTime = 0;

        while (elapsedTime < retreatDuration)
        {
            spearPoint.transform.position = (Vector2)transform.position + Vector2.Lerp(end, start, elapsedTime/retreatDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        spearPoint.transform.position = start;
        

        StartCoroutine(AttackCooldown());
    }

}