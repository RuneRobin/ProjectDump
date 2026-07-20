using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
public class LineController : MonoBehaviour
{
    public List<Transform> dots;
    LineRenderer lr;

    private GameObject mouseTransform;
    
    // Start is called before the first frame update
    private void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.positionCount = dots.Count;
        mouseTransform = new GameObject();
    }

    // Update is called once per frame
    private void Update()
    {
        mouseTransform.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dots[0] = gameObject.transform.parent.transform;
        dots[1] = mouseTransform.transform;
        lr.SetPositions(dots.ConvertAll(n => n.position - new Vector3(0, 0, 5)).ToArray());
    }

    public Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);
        return positions;
    }

    public float GetWidth()
    {
        return lr.startWidth;
    }

    public void OnDestroy()
    {
        Destroy(mouseTransform);
    }
}


