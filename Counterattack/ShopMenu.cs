using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopMenu : MonoBehaviour
{
    private List<GameObject> availableArms;

    public Button armButton;
    public Button armButton2;

    public Image armBtnImage;
    public Image armBtnImage2;

    private GameObject purchasedArm;
    private GameObject arm;
    private GameObject arm2;

    public GameObject armSpawnArea;

    private PlayerScript player;

    // Start is called before the first frame update
    private void Awake()
    {
        availableArms = new List<GameObject>(Resources.LoadAll<GameObject>("Weapons"));
        player = PlayerScript.instance;
    }

    void OnEnable()
    {
        List<int> rn = RandomPick(availableArms.Count, 0, availableArms.Count);

        arm = availableArms[rn[0]];
        armBtnImage.sprite = arm.GetComponent<SpriteRenderer>().sprite;
        armBtnImage.color = arm.GetComponent<SpriteRenderer>().color;

        arm2 = availableArms[rn[1]];
        armBtnImage2.sprite = arm2.GetComponent<SpriteRenderer>().sprite;
        armBtnImage2.color = arm2.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void Purchase()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;


        if (player.money >= 1)
        {
            player.money -= 1;
            if(name == "Arm")
            {
                purchasedArm = Instantiate(arm, (Random.insideUnitCircle * (armSpawnArea.GetComponent<CircleCollider2D>().radius * armSpawnArea.GetComponent<Transform>().localScale.x)) + (Vector2)armSpawnArea.transform.position, transform.rotation);
                purchasedArm.transform.parent = player.transform;
                armButton.gameObject.SetActive(false);
            }
            if(name == "Arm 2")
            {
                purchasedArm = Instantiate(arm2, (Random.insideUnitCircle * (armSpawnArea.GetComponent<CircleCollider2D>().radius * armSpawnArea.GetComponent<Transform>().localScale.x)) + (Vector2)armSpawnArea.transform.position, transform.rotation);
                purchasedArm.transform.parent = player.transform;
                armButton2.gameObject.SetActive(false);
            }
        }
    }
}
