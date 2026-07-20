using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MasterController : MonoBehaviour
{
    [HideInInspector]
    public static MasterController instance;

    [Header("Army One")]
    public GameObject generalOne;
    public int armyOneFaction = 0;
    public List<GameObject> armyOne = new List<GameObject>();
    public List<GameObject> formationsOne = new List<GameObject>();
    public List<GameObject> factionFormationsOne = new List<GameObject>();
    public int currentFormationOne = 0;
    public GameObject armyOneHPGameObject;
    private float armyOneMaxHPValue;
    public float armyOneCurrentHPValue;

    [Header("Army Two")]
    public GameObject generalTwo;
    public int armyTwoFaction = 0;
    public List<GameObject> armyTwo = new List<GameObject>();
    public List<GameObject> formationsTwo = new List<GameObject>();
    public List<GameObject> factionFormationsTwo = new List<GameObject>();
    public int currentFormationTwo = 0;
    public GameObject armyTwoHPGameObject;
    private float armyTwoMaxHPValue;
    public float armyTwoCurrentHPValue;

    [Header("Misc")]
    public List<Sprite> generalClassesSprites = new List<Sprite>();
    public List<Sprite> unitsClassesSprites = new List<Sprite>();

    public float factionSetupTimer = 5;
    public float formationSetupTimer = 60;

    public GameObject selectionButtons;
    private bool battleBegun = false;

    public TextMeshProUGUI winnerText;
    private bool winnerDecided = false;

    [Header("Auto Features")]
    public bool autoBattle = false;
    public int autoCountdown = 0;

    [Header("Prefabs, etc.")]
    public GameObject swordPrefab;
    public GameObject projPrefab;
    public GameObject staffPrefab;
    public GameObject healPrefab;
    public GameObject lilguyPrefab;

    public List<Sprite> bulletSprites = new List<Sprite>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("ArmyOne"))
        {
            armyOne.Add(g);
            armyOneMaxHPValue += g.GetComponent<UnitScript>().maxHealth;
            g.GetComponent<UnitScript>().positionInArmy = armyOne.Count; //while this functionally works fine, the numbers won't match how they're numbered in the scene,
                                                                         //but it doesn't matter because they'll be given classes based on what No in the army they are anyways,
                                                                         //and the posititons of that are fine
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("ArmyTwo"))
        {
            armyTwo.Add(g);
            armyTwoMaxHPValue += g.GetComponent<UnitScript>().maxHealth;
            g.GetComponent<UnitScript>().positionInArmy = armyTwo.Count; //same here as above
        }

        armyOneCurrentHPValue = armyOneMaxHPValue;
        armyTwoCurrentHPValue = armyTwoMaxHPValue;

        
        if(SceneConst.instance.autoStart == true)
        {
            autoBattle = true;
            armyOneFaction = 0;
            armyTwoFaction = 1;
            autoCountdown = (int)factionSetupTimer;
            AutoSelectFaction(armyOneFaction, armyTwoFaction);
        }
        else
        {
            autoBattle = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(factionSetupTimer > 0)
        {
            factionSetupTimer -= Time.deltaTime;

            //auto battle setup
            if(autoBattle == true && factionSetupTimer+1 < autoCountdown)
            {
                SceneConst.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(SceneConst.instance.SFXCollection[autoCountdown + 1]);

                autoCountdown--;
                AutoSelectFaction(armyOneFaction,armyTwoFaction);
            }
        }
        
        if(formationSetupTimer > 0 && factionSetupTimer <= 0)
        {
            formationSetupTimer -= Time.deltaTime;
            
            if(autoBattle == true && formationSetupTimer > 2)
            {
                formationSetupTimer = 2;
                currentFormationOne = Random.Range(0, formationsOne.Count);
                currentFormationTwo = Random.Range(0, formationsTwo.Count);
                selectionButtons.SetActive(false);
            }

            for(int i = 0; i < armyOne.Count; i++)
            {
                if (armyOne[i].transform.position != formationsOne[currentFormationOne].transform.GetChild(i).transform.position)
                {
                    Vector3 direction = (formationsOne[currentFormationOne].transform.GetChild(i).transform.position - armyOne[i].transform.position).normalized;
                    float distance = Vector2.Distance(armyOne[i].transform.position, formationsOne[currentFormationOne].transform.GetChild(i).position);
                    if (distance > 0.1f)
                    {
                        armyOne[i].transform.GetComponent<Rigidbody2D>().MovePosition((Vector2)armyOne[i].transform.position + (new Vector2(direction.x * armyOne[i].GetComponent<UnitScript>().speed, direction.y * armyOne[i].GetComponent<UnitScript>().speed)) * Time.deltaTime);
                    }
                    else
                    {
                        armyOne[i].transform.position = formationsOne[currentFormationOne].transform.GetChild(i).transform.position;
                    }
                }
            }
            for (int i = 0; i < armyTwo.Count; i++)
            {
                if (armyTwo[i].transform.position != formationsTwo[currentFormationTwo].transform.GetChild(i).transform.position)
                {
                    Vector3 direction = (formationsTwo[currentFormationTwo].transform.GetChild(i).transform.position - armyTwo[i].transform.position).normalized;
                    float distance = Vector2.Distance(armyTwo[i].transform.position, formationsTwo[currentFormationTwo].transform.GetChild(i).position);
                    if (distance > 0.1f)
                    {
                        armyTwo[i].transform.GetComponent<Rigidbody2D>().MovePosition((Vector2)armyTwo[i].transform.position + (new Vector2(direction.x * armyTwo[i].GetComponent<UnitScript>().speed, direction.y * armyTwo[i].GetComponent<UnitScript>().speed)) * Time.deltaTime);
                    }
                    else
                    {
                        armyTwo[i].transform.position = formationsTwo[currentFormationTwo].transform.GetChild(i).transform.position;
                    }
                }
            }
        }

        armyOneHPGameObject.GetComponent<Image>().fillAmount = armyOneCurrentHPValue / armyOneMaxHPValue;
        armyTwoHPGameObject.GetComponent<Image>().fillAmount = armyTwoCurrentHPValue / armyTwoMaxHPValue;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); //Main Menu
        }

        if (factionSetupTimer > 0) //chooses a set of generals for the fight through player input
        {
            if (Input.GetKeyDown(KeyCode.A) && autoBattle == false)
            {
                CycleGeneralOne(1);
            }
            if (Input.GetKeyDown(KeyCode.D) && autoBattle == false)
            {
                CycleGeneralTwo(1);
            }
        }

        if (factionSetupTimer <= 0 && formationSetupTimer > 0 && autoBattle == false) //chooses a formation for troops after the generals have been chosen, alternative to clicking buttons on the screen
        {
            if(selectionButtons.activeInHierarchy == false)
            {
                selectionButtons.SetActive(true);
            }
            
            if (Input.GetKeyDown(KeyCode.Q) && autoBattle == false)
            {
                currentFormationOne++;
                if (currentFormationOne >= formationsOne.Count)
                {
                    currentFormationOne = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.E) && autoBattle == false)
            {
                currentFormationTwo++;
                if (currentFormationTwo >= formationsTwo.Count)
                {
                    currentFormationTwo = 0;
                }
            }
        }

        if (factionSetupTimer <= 0 && formationsOne.Count < 5 && formationsTwo.Count < 5) //check to add the two unique faction formations after faction setup timer reaches 0
        {
            #region Army One Unique Formation Add to regular formation
            if (armyOneFaction == 0)
            {
                formationsOne.Add(factionFormationsOne[0]);
                formationsOne.Add(factionFormationsOne[1]);
            }
            else if (armyOneFaction == 1)
            {
                formationsOne.Add(factionFormationsOne[2]);
                formationsOne.Add(factionFormationsOne[3]);
            }
            else if (armyOneFaction == 2)
            {
                formationsOne.Add(factionFormationsOne[4]);
                formationsOne.Add(factionFormationsOne[5]);
            }
            else if (armyOneFaction == 3)
            {
                formationsOne.Add(factionFormationsOne[6]);
                formationsOne.Add(factionFormationsOne[7]);
            }
            else if (armyOneFaction == 4)
            {
                formationsOne.Add(factionFormationsOne[8]);
                formationsOne.Add(factionFormationsOne[9]);
            }
            #endregion

            #region Army Two Unique Formation Add to regular formation
            if (armyTwoFaction == 0)
            {
                formationsTwo.Add(factionFormationsTwo[0]);
                formationsTwo.Add(factionFormationsTwo[1]);
            }
            else if (armyTwoFaction == 1)
            {
                formationsTwo.Add(factionFormationsTwo[2]);
                formationsTwo.Add(factionFormationsTwo[3]);
            }
            else if (armyTwoFaction == 2)
            {
                formationsTwo.Add(factionFormationsTwo[4]);
                formationsTwo.Add(factionFormationsTwo[5]);
            }
            else if (armyTwoFaction == 3)
            {
                formationsTwo.Add(factionFormationsTwo[6]);
                formationsTwo.Add(factionFormationsTwo[7]);
            }
            else if (armyTwoFaction == 4)
            {
                formationsTwo.Add(factionFormationsTwo[8]);
                formationsTwo.Add(factionFormationsTwo[9]);
            }
            #endregion
        }

        if(formationSetupTimer <= 0 && battleBegun == false)
        {
            battleBegun = true;

            SceneConst.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(SceneConst.instance.SFXCollection[20]); //trumpet begin

            if (selectionButtons.activeInHierarchy == true)
            {
                selectionButtons.SetActive(false);
            }

            #region adds class scripts to all units based on number in the army
            for (int i = 0; i < armyOne.Count; i++)
            {
                if(i < 15)
                {
                    armyOne[i].AddComponent<InfantryScript>();
                }
                else if(i >= 15 && i < 18)
                {
                    armyOne[i].AddComponent<ArcherScript>();
                }
                else if(i >= 18)
                {
                    armyOne[i].AddComponent<PriestScript>();
                }
            }

            for (int i = 0; i < armyTwo.Count; i++)
            {
                if (i < 15)
                {
                    armyTwo[i].AddComponent<InfantryScript>();
                }
                else if (i >= 15 && i < 18)
                {
                    armyTwo[i].AddComponent<ArcherScript>();
                }
                else if (i >= 18)
                {
                    armyTwo[i].AddComponent<PriestScript>();
                    
                }
            }
            #endregion

            #region add faction scripts to all units and enable their colliders
            if (armyOneFaction == 0)
            {
                for (int i = 0; i < armyOne.Count; i++)
                {
                    armyOne[i].AddComponent<UndeadSoldier>();
                    armyOne[i].GetComponent<CircleCollider2D>().enabled = true;
                }
            }
            else if (armyOneFaction == 1)
            {
                for (int i = 0; i < armyOne.Count; i++)
                {
                    armyOne[i].AddComponent<HighlanderSoldier>();
                    armyOne[i].GetComponent<CircleCollider2D>().enabled = true;
                }
            }
            else if (armyOneFaction == 2)
            {
                for (int i = 0; i < armyOne.Count; i++)
                {
                    armyOne[i].AddComponent<ElementalSoldier>();
                    armyOne[i].GetComponent<CircleCollider2D>().enabled = true;
                }
            }
            else if (armyOneFaction == 3)
            {
                for (int i = 0; i < armyOne.Count; i++)
                {
                    armyOne[i].AddComponent<CircusClown>();
                    armyOne[i].GetComponent<CircleCollider2D>().enabled = true;
                }
            }
            else if (armyOneFaction == 4)
            {
                for (int i = 0; i < armyOne.Count; i++)
                {
                    armyOne[i].AddComponent<RobotSoldier>();
                    armyOne[i].GetComponent<CircleCollider2D>().enabled = true;
                }
            }//ArmyOne

            if (armyTwoFaction == 0)
            {
                for (int i = 0; i < armyTwo.Count; i++)
                {
                    armyTwo[i].AddComponent<UndeadSoldier>();
                    armyTwo[i].GetComponent<CircleCollider2D>().enabled = true;
                }
            }
            else if (armyTwoFaction == 1)
            {
                for (int i = 0; i < armyTwo.Count; i++)
                {
                    armyTwo[i].AddComponent<HighlanderSoldier>();
                    armyTwo[i].GetComponent<CircleCollider2D>().enabled = true;
                }
            }
            else if (armyTwoFaction == 2)
            {
                for (int i = 0; i < armyTwo.Count; i++)
                {
                    armyTwo[i].AddComponent<ElementalSoldier>();
                    armyTwo[i].GetComponent<CircleCollider2D>().enabled = true;
                }
            }
            else if (armyTwoFaction == 3)
            {
                for (int i = 0; i < armyTwo.Count; i++)
                {
                    armyTwo[i].AddComponent<CircusClown>();
                    armyTwo[i].GetComponent<CircleCollider2D>().enabled = true;
                }
            }
            else if (armyTwoFaction == 4)
            {
                for (int i = 0; i < armyTwo.Count; i++)
                {
                    armyTwo[i].AddComponent<RobotSoldier>();
                    armyTwo[i].GetComponent<CircleCollider2D>().enabled = true;
                }
            }//ArmyTwo
            #endregion
        }

        #region Determine Winner by who reaches 0 HP total army health first
        if (((armyOneCurrentHPValue <= 0 && armyTwoCurrentHPValue <= 0) || armyOneCurrentHPValue <= 0 || armyTwoCurrentHPValue <= 0) && winnerDecided == false)
        {
            winnerDecided = true;
            if(armyOneCurrentHPValue > armyTwoCurrentHPValue)
            {
                #region winner is army one
                if(armyOneFaction == 0)
                {
                    winnerText.text = "The Lich Wins!!!";
                    winnerText.gameObject.SetActive(true);
                }
                else if (armyOneFaction == 1)
                {
                    winnerText.text = "High King Gallagher Wins!!!";
                    winnerText.gameObject.SetActive(true);
                }
                else if (armyOneFaction == 2)
                {
                    winnerText.text = "The Storm Wins!!!";
                    winnerText.gameObject.SetActive(true);
                }
                else if (armyOneFaction == 3)
                {
                    winnerText.text = "Ringleader R.R. Wins!!!";
                    winnerText.gameObject.SetActive(true);
                }
                else if (armyOneFaction == 4)
                {
                    winnerText.text = "R4-X Prime Wins!!!";
                    winnerText.gameObject.SetActive(true);
                }
                #endregion
                SceneConst.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(SceneConst.instance.SFXCollection[21]); //trumpet winner
            }
            else if(armyTwoCurrentHPValue > armyOneCurrentHPValue)
            {
                #region winner is army two
                if (armyTwoFaction == 0)
                {
                    winnerText.text = "The Lich Wins!!!";
                    winnerText.gameObject.SetActive(true);
                }
                else if (armyTwoFaction == 1)
                {
                    winnerText.text = "High King Gallagher Wins!!!";
                    winnerText.gameObject.SetActive(true);
                }
                else if (armyTwoFaction == 2)
                {
                    winnerText.text = "The Storm Wins!!!";
                    winnerText.gameObject.SetActive(true);
                }
                else if (armyTwoFaction == 3)
                {
                    winnerText.text = "Ringleader R.R. Wins!!!";
                    winnerText.gameObject.SetActive(true);
                }
                else if (armyTwoFaction == 4)
                {
                    winnerText.text = "R4-X Prime Wins!!!";
                    winnerText.gameObject.SetActive(true);
                }
                #endregion
                SceneConst.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(SceneConst.instance.SFXCollection[21]); //trumpet winner
            }
            else
            {
                winnerText.text = "Draw???";
                winnerText.gameObject.SetActive(true);
            }
        }
        #endregion
    }

    private void CycleGeneralOne(int next)
    {
        armyOneFaction += next;
        if (armyOneFaction >= generalClassesSprites.Count)
        {
            armyOneFaction = 0;
        }

        generalOne.GetComponent<SpriteRenderer>().sprite = generalClassesSprites[armyOneFaction];
        foreach (GameObject g in armyOne)
        {
            g.GetComponent<SpriteRenderer>().sprite = unitsClassesSprites[armyOneFaction];
        }          
    }

    private void CycleGeneralTwo(int next)
    {
        armyTwoFaction += next;
        if (armyTwoFaction >= generalClassesSprites.Count)
        {
            armyTwoFaction = 0;
        }

        generalTwo.GetComponent<SpriteRenderer>().sprite = generalClassesSprites[armyTwoFaction];
        foreach (GameObject g in armyTwo)
        {
            g.GetComponent<SpriteRenderer>().sprite = unitsClassesSprites[armyTwoFaction];
        }        
    }

    public void ArmyHealthUpdater(string faction)
    {
        float ch = 0;

        if (faction == "ArmyOne")
        {
            foreach (GameObject g in armyOne)
            {
                if (g != null)
                {
                    ch += g.GetComponent<UnitScript>().health;
                }
            }
            armyOneCurrentHPValue = ch;
        }
        else if (faction == "ArmyTwo")
        {
            foreach (GameObject g in armyTwo)
            {
                if(g != null)
                {
                    ch += g.GetComponent<UnitScript>().health;
                }
            }
            armyTwoCurrentHPValue = ch;
        }

        
    }

    public void FormationSelectionButtonLeft(int formation)
    {
        currentFormationOne = formation;
    }

    public void FormationSelectionButtonRight(int formation)
    {
        currentFormationTwo = formation;
    }

    public void AutoSelectFaction(int lastFactionArmyOne, int lastFactionArmyTwo)
    {
        List<int> otherFactions = new List<int>() { 0, 1, 2, 3, 4 };

        otherFactions.Remove(lastFactionArmyOne);
        armyOneFaction = otherFactions[Random.Range(0, otherFactions.Count)];

        otherFactions.Add(lastFactionArmyOne);
        otherFactions.Remove(lastFactionArmyTwo);
        otherFactions.Remove(armyOneFaction);
        armyTwoFaction = otherFactions[Random.Range(0, otherFactions.Count)];

        CycleGeneralOne(0);
        CycleGeneralTwo(0);
    }
}
