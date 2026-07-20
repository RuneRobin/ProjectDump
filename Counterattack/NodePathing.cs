using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodePathing : MonoBehaviour
{
    public GameObject leftNode;
    public GameObject rightNode;

    public GameObject reverseLeftNode;
    public GameObject reverseRightNode;

    public bool firstTimeRound = true;
    public bool activeNode = false;
    Button button;

    //for animating nodes
    public float timeOffset;
    public float speed = 6;

    // Start is called before the first frame update
    void Start()
    {
        timeOffset = Random.Range(0f, 100f);
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        //make only active nodes derive from this.
        if (activeNode == true)
        {
            transform.localScale = new Vector3(1 + Mathf.Sin((Time.time + timeOffset) * speed) * 0.2f, 1 + Mathf.Sin((Time.time + timeOffset - 0.2f) * speed) * 0.2f, 1);
            button.interactable = true;
        }
    }

    public void CurrentNode()
    {
        GetComponentInParent<NodeManager>().currNodeType = gameObject.tag;

        if (firstTimeRound == true)
        {
            foreach(GameObject node in GetComponentInParent<NodeManager>().nodes)
            {
                node.GetComponent<NodePathing>().activeNode = false;
                node.GetComponent<Button>().interactable = false;
                node.gameObject.transform.localScale = new Vector2(1, 1);
            }
            leftNode.GetComponent<NodePathing>().activeNode = true;
            rightNode.GetComponent<NodePathing>().activeNode = true;
        }
        if(firstTimeRound == false)
        {
            foreach (GameObject node in GetComponentInParent<NodeManager>().nodes)
            {
                node.GetComponent<NodePathing>().activeNode = false;
                node.GetComponent<Button>().interactable = false;
                node.gameObject.transform.localScale = new Vector2(1, 1);
            }

            if(reverseLeftNode.name == "Centre")
            {
                GetComponentInParent<NodeManager>().currNodeType = string.Empty;
            }
            else
            {
                reverseLeftNode.GetComponent<NodePathing>().activeNode = true;
                reverseRightNode.GetComponent<NodePathing>().activeNode = true;
                reverseLeftNode.GetComponent<NodePathing>().firstTimeRound = false;
                reverseRightNode.GetComponent<NodePathing>().firstTimeRound = false;
            }
        }

        activeNode = false;
        button.interactable = false;

        CharacterLoader.instance.currEncounter = gameObject.tag;
        CharacterLoader.instance.currAreaAndNode = gameObject.name;
        CharacterLoader.instance.CurrentEncounter();

    }

}
