using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureEight : MonoBehaviour
{
    //the figure eight spinner script
    public float speed = 1;
    public float xScale = 1;
    public float yScale = 1;

    public Vector3 pivot = new Vector3();
    private Vector3 pivotOffset;
    private float phase;
    private bool invert = false;
    private float twoPi = Mathf.PI * 2;

    private Vector3 currPos = new Vector3();

    public bool eight2Inf = false;
    public float xMult;
    public float yMult;    
    
    // Start is called before the first frame update
    void Start()
    {
        pivot = transform.position;

        yMult = (invert ? -1 : 1);
                xMult = 1;

    }

    // Update is called once per frame
    void Update()
    {

        phase += Mathf.Abs(speed) * Time.deltaTime;
        if(phase > twoPi)
        {
            invert = !invert;
            phase -= twoPi;
        }

        if(phase < 0)
        {
            phase += twoPi;
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            eight2Inf = !eight2Inf;
        }

        if (eight2Inf == false)
        {
            pivotOffset = Vector3.up * 2 * yScale;
            xMult = Mathf.Sin(phase * Mathf.Sign(speed)) * xScale;
            yMult = Mathf.Cos(phase * Mathf.Sign(speed)) * yScale;
            yMult *= (invert ? -1 : 1);
        }
        else
        {
            pivotOffset = Vector3.right * 2 * xScale;
            xMult = Mathf.Cos(phase * Mathf.Sign(speed)) * xScale;
            yMult = Mathf.Sin(phase * Mathf.Sign(speed)) * yScale;
            xMult *= (invert ? -1 : 1);
        }

        currPos = pivot + (invert ? pivotOffset : Vector3.zero);

        currPos.x += xMult;
        currPos.y += yMult;
        transform.position = currPos;
        

        

        //gameObject.transform.rotation = Quaternion.Euler(0,0,Random.Range(-10, 10));
    }

}
