using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardOptions : MonoBehaviour
{
    private List<GameObject> availableArms;

    public Button newArmButton;
    public Image newArmImage;

    public Button goldButton;

    private GameObject armInQuestion;
    private GameObject newArm;

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
        armInQuestion = availableArms[Random.Range(0, availableArms.Count)];
        newArmImage.sprite = armInQuestion.GetComponent<SpriteRenderer>().sprite;
        newArmImage.color = armInQuestion.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewArm()
    {
        newArm = Instantiate(armInQuestion, (Random.insideUnitCircle * (armSpawnArea.GetComponent<CircleCollider2D>().radius * armSpawnArea.GetComponent<Transform>().localScale.x)) + (Vector2)armSpawnArea.transform.position, transform.rotation);
        newArm.transform.parent = player.transform;
        newArmButton.gameObject.SetActive(false);
    }

    public void MoreGold()
    {
        player.money += Mathf.CeilToInt(1 + 1 * player.moneyBoost); //replace 1 with whatever range of money should be given
        Master.instance.moneyCollectedDuringRun += Mathf.CeilToInt(1 + 1 * player.moneyBoost); //replace 1 with whatever range of money should be given
        goldButton.gameObject.SetActive(false);
    }
}
