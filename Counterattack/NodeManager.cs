using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeManager : MonoBehaviour
{
    public Sprite enemy;
    public Sprite shop;
    public Sprite restArea;
    public Sprite elite;

    private int nOfShops = 2;
    private int nOfElites = 1;
    private int nOfRestAreas = 2;
    private int nOfEnemies = 9;

    public List<GameObject> nodes;
    public List<int> randomNumbers;

    public string currNodeType;
    public NodeManager otherNodeManager1;
    public NodeManager otherNodeManager2;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Image node in gameObject.GetComponentsInChildren<Image>())
        {
            nodes.Add(node.gameObject);
        }

        nodes.RemoveAt(nodes.Count -1); //Removes boss node

        randomNumbers = RandomPick(nodes.Count, 0, nodes.Count);

        RandomizerNodes();

    }

    static List<int> RandomPick(int count, int minValue, int maxValue) //makes random numbers to pick for the upgrade menu
    {
        HashSet<int> uniqueNumbers = new HashSet<int>();

        while (uniqueNumbers.Count < count)
        {
            int newNumber = Random.Range(minValue, maxValue);
            uniqueNumbers.Add(newNumber); // HashSet ensures all values are unique
        }

        return new List<int>(uniqueNumbers);
    }

    void RandomizerNodes()
    {
        do
        {
            if (nOfShops > 0)
            {
                nodes[randomNumbers[0]].GetComponent<Image>().sprite = shop;
                nodes[randomNumbers[0]].tag = "ShopEncounter";
                nOfShops--;
                randomNumbers.RemoveAt(0);
            }
            if (nOfElites > 0)
            {
                nodes[randomNumbers[0]].GetComponent<Image>().sprite = elite;
                nodes[randomNumbers[0]].tag = "EliteEncounter";
                nOfElites--;
                randomNumbers.RemoveAt(0);
            }
            if (nOfEnemies > 0)
            {
                nodes[randomNumbers[0]].GetComponent<Image>().sprite = enemy;
                nodes[randomNumbers[0]].tag = "EnemyEncounter";
                nOfEnemies--;
                randomNumbers.RemoveAt(0);
            }
            if (nOfRestAreas > 0)
            {
                nodes[randomNumbers[0]].GetComponent<Image>().sprite = restArea;
                nodes[randomNumbers[0]].tag = "RestEncounter";
                nOfRestAreas--;
                randomNumbers.RemoveAt(0);
            }
        } while (nOfElites > 0 || nOfEnemies > 0 || nOfRestAreas > 0 || nOfShops > 0);

    }


    // Update is called once per frame
    void Update()
    {
       if(currNodeType != string.Empty) //disables the other 2 map paths until the player has returned from that paths journey.
        {
            otherNodeManager1.gameObject.SetActive(false); //right now they are simply turned off, maybe find a way to just make it unable to go there? Or perhaps this is good...
            otherNodeManager2.gameObject.SetActive(false);
        }
       else
        {
            otherNodeManager1.gameObject.SetActive(true);
            otherNodeManager2.gameObject.SetActive(true);
        }
    }
}
