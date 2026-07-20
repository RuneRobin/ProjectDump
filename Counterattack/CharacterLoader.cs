using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterLoader : MonoBehaviour
{
    public static CharacterLoader instance;

    private PlayerScript player;
    
    private bool OnOff = true;
    public string currEncounter;
    public string currAreaAndNode;

    public GameObject playerPosition;
    public GameObject playerGameObject;
    public GameObject encounterButtons;
    public GameObject windUpTimers;

    private List<GameObject> upgradeButtonList = new List<GameObject>();

    public GameObject shopEncounterGameObject;
    public GameObject enemyEncounterGameObject;
    public GameObject eliteEncounterGameObject;
    public GameObject restEncounterGameObject;
    public GameObject bossEncounterGameObject;

    public GameObject armUpgradePrefab;

    public List<GameObject> activeEnemies;
    public bool currentlyFighting = false;
    private bool fightStarted = false; //for resetting player position

    public List<GameObject> swordTypeArmList; //for sword-types to swing around player in equalateral distance

    public void Start()
    {
        PlayerData data = SaveSystem.LoadUpgrades();
        //GameObject player;

        playerGameObject = Instantiate(Resources.Load("Characters/" + data.selectedCharacterName) as GameObject, playerPosition.transform.position, transform.rotation);

        playerGameObject.name = data.selectedCharacterName;

        //parryPlayer.AddComponent<PlayerScript>();
        player = PlayerScript.instance;
        playerGameObject.GetComponent<ParryHitbox>().player = player;

        Camera.main.transform.parent = playerGameObject.transform;
    }

    public CharacterLoader()
    {
        instance = this;
    }

    public void Update()
    {
        if(Input.GetKeyDown("m") && currentlyFighting == false) //brings up map out of battle
        {
            gameObject.transform.Find("Map").gameObject.SetActive(OnOff);
            OnOff = !OnOff;
        }

        //if(currentlyFighting == false && swordTypeSwipeQueue != null)
        {
            //swordTypeSwipeQueue.Clear();
        }

        if (currentlyFighting == false && windUpTimers.gameObject.activeSelf == true)
        {
            windUpTimers.SetActive(false);
        }
        if (currentlyFighting == true && windUpTimers.gameObject.activeSelf == false)
        {
            windUpTimers.SetActive(true);
        }

        if (currentlyFighting == true && Master.instance.currentEnemyCount == 0 && Master.instance.enemiesLeft == 0)
        {
            currentlyFighting = false;
            fightStarted = false;
            Master.instance.stagesCleared++;

            player.timesArmsHaveWoundUp = 0; //for clockwork arm
            player.timesArmsHaveMissed = 0; //for tantrum arm

            encounterButtons.transform.Find("RewardsScreen").gameObject.SetActive(true);
        }

        if(currentlyFighting == true && playerGameObject.transform.position != playerPosition.transform.position && fightStarted == false)
        {
            playerGameObject.transform.position = playerPosition.transform.position;
            fightStarted = true;
        }
    }

    public void CurrentEncounter()
    {
        //sets the encounter
        if(currEncounter == "ShopEncounter")
        {
            shopEncounterGameObject.SetActive(true);
            encounterButtons.transform.Find("Shop").gameObject.SetActive(true);
        }
        if(currEncounter == "RestEncounter")
        {
            restEncounterGameObject.SetActive(true);
            encounterButtons.transform.Find("LeaveRest").gameObject.SetActive(true);
            encounterButtons.transform.Find("Rest").gameObject.SetActive(true);
            encounterButtons.transform.Find("Upgrade").gameObject.SetActive(true);
        }
        if(currEncounter == "EnemyEncounter")
        {
            enemyEncounterGameObject.SetActive(true);
            currentlyFighting = true;
        }
        if(currEncounter == "EliteEncounter")
        {

        }
        if(currEncounter == "BossEncounter")
        {
            enemyEncounterGameObject.SetActive(true);
            currentlyFighting = true;
        }

        GameObject.Find("Map").gameObject.SetActive(false);
        OnOff = !OnOff;
    }

    public void LeaveShop()
    {
        gameObject.transform.Find("Map").gameObject.SetActive(true);
        OnOff = !OnOff;
        shopEncounterGameObject.SetActive(false);
        encounterButtons.transform.Find("Shop").GetComponent<ShopMenu>().armButton.gameObject.SetActive(true);
        encounterButtons.transform.Find("Shop").GetComponent<ShopMenu>().armButton2.gameObject.SetActive(true);
        encounterButtons.transform.Find("Shop").gameObject.SetActive(false);
    }

    public void Rest()
    {
        player.health += Mathf.CeilToInt(player.maxHealth * 0.3f);
        player.SetHealth(player.health);
        encounterButtons.transform.Find("Rest").gameObject.SetActive(false);
    }

    public void UpgradeMenu()
    {
        encounterButtons.transform.Find("Upgrade").gameObject.SetActive(false); //eh?
        encounterButtons.transform.Find("Scroll Rect").gameObject.SetActive(true);


        foreach (GameObject arm in player.currArmCollection)
        {
            GameObject newButton;

            newButton = Instantiate(armUpgradePrefab, transform.position, transform.rotation);

            newButton.transform.Find("ArmSprite").GetComponent<Image>().sprite = arm.GetComponent<SpriteRenderer>().sprite;
            newButton.transform.Find("ArmSprite").GetComponent<Image>().color = arm.GetComponent<SpriteRenderer>().color; //change this when getting the arm Models instead
            
            upgradeButtonList.Add(newButton);
            newButton.GetComponent<Button>().onClick.AddListener(delegate {Upgrade(arm.GetComponent<ArmStats>().purchaseCost, arm);});
            
            newButton.transform.SetParent(encounterButtons.transform.Find("Scroll Rect").Find("Mask").Find("Grid").gameObject.transform,false);
        }
    }

    private void Upgrade(int cost, GameObject arm)
    {
        if(player.money >= cost && arm.GetComponent<ArmStats>().level < 5)
        {
            arm.GetComponent<ArmStats>().level++;
            arm.GetComponent<ArmStats>().levelling = true;
            player.money -= cost;
            arm.GetComponent<ArmStats>().purchaseCost++; //change all this to be dynamic to the rarity and scaling with level.
        }
    }

    public void LeaveRestArea()
    {
        foreach (GameObject button in upgradeButtonList) //clears the buttons
        {
            Destroy(button);
        }
        upgradeButtonList.Clear();

        gameObject.transform.Find("Map").gameObject.SetActive(true);
        OnOff = !OnOff;
        restEncounterGameObject.SetActive(false);
        encounterButtons.transform.Find("Rest").gameObject.SetActive(false);
        encounterButtons.transform.Find("LeaveRest").gameObject.SetActive(false);
        encounterButtons.transform.Find("Upgrade").gameObject.SetActive(false); //eh?
        encounterButtons.transform.Find("Scroll Rect").gameObject.SetActive(false);
    }

    public void LeaveRewardsScreen()
    {
        gameObject.transform.Find("Map").gameObject.SetActive(true);
        OnOff = !OnOff;
        encounterButtons.transform.Find("RewardsScreen").GetComponent<RewardOptions>().goldButton.gameObject.SetActive(true);
        encounterButtons.transform.Find("RewardsScreen").GetComponent<RewardOptions>().newArmButton.gameObject.SetActive(true);
        enemyEncounterGameObject.SetActive(false);
        encounterButtons.transform.Find("RewardsScreen").gameObject.SetActive(false);
    }

}
