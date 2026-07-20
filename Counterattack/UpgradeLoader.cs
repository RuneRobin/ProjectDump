using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class UpgradeLoader : MonoBehaviour
{
    public static UpgradeLoader instance;

    public int health; //+10
    public int blockRegen; //re-do, block maybe
    public int armour; //re-do, remove?
    public int bulletCount; //+2
    public int bulletSpeed; //+30
    public int swordSwingSpeed;
    public int moveSpeed; //re-do, remove?
    public int size; //re-do, remove?
    public int moneyMultiplier; //+2.0x
    //public int exp; //re-do, purchase cost reduction?
    public int maxNumberOfItems; //+100
    public int luck; //+100
    public int damage; //+5
    public string characterName;

    public int moneySaved;

    public Dictionary<string, UnlockableBlueprint> unlockableBlueprints = new Dictionary<string, UnlockableBlueprint>(); //think emoji
    public int unlockedCharacterCount;
    
    #region unlockable characters
    public bool libraUnlocked = false; //reincorporate as blueprints
    public bool ushiUnlocked = false;
    public bool tofuUnlocked = false;
    public bool moyasiUnlocked = false;
    public bool capUnlocked = false;
    public bool shiftUnlocked = false;
    public bool ryytikkiUnlocked = false;
    public bool keyRatUnlocked = false;
    public bool ryanUnlocked = false;
    public bool honeyUnlocked = false;
    public bool denimUnlocked = false;
    public bool soundUnlocked = false;
    public bool jackUnlocked = false;
    #endregion


    public void Start()
    {
        PlayerData data = SaveSystem.LoadUpgrades();
        instance = this;

        #region Buttons for PermUpgradeMenu
        health = data.health;
        if (GameObject.Find("HealthText"))
        {
            GameObject.Find("HealthText").GetComponent<Text>().text = "Health: " + health;
        }

        blockRegen = data.blockRegen;
        if (GameObject.Find("RegenText"))
        {
            GameObject.Find("RegenText").GetComponent<Text>().text = "Regen: " + blockRegen;
        }

        armour = data.armour;
        if (GameObject.Find("ArmourText"))
        {
            GameObject.Find("ArmourText").GetComponent<Text>().text = "Armour: " + armour;
        }

        bulletCount = data.bulletCount;
        if (GameObject.Find("BCountText"))
        {
            GameObject.Find("BCountText").GetComponent<Text>().text = "Bullet Count: " + bulletCount;
        }

        bulletSpeed = data.bulletSpeed;
        if (GameObject.Find("BSpeedText"))
        {
            GameObject.Find("BSpeedText").GetComponent<Text>().text = "Bullet Speed: " + bulletSpeed;
        }

        moveSpeed = data.moveSpeed;
        if (GameObject.Find("MoveText"))
        {
            GameObject.Find("MoveText").GetComponent<Text>().text = "Movement Speed: " + moveSpeed;
        }

        size = data.size;
        if (GameObject.Find("SizeText"))
        {
            GameObject.Find("SizeText").GetComponent<Text>().text = "Attack Size: " + size;
        }

        moneyMultiplier = data.moneyMultiplier;
        if (GameObject.Find("MoneyMultText"))
        {
            GameObject.Find("MoneyMultText").GetComponent<Text>().text = "Money Multiplier: " + moneyMultiplier;
        }

        //exp = data.exp;
        //if (GameObject.Find("EXPText"))
        {
            //GameObject.Find("EXPText").GetComponent<Text>().text = "EXP Multiplier: " + exp;
        }

        maxNumberOfItems = data.maxNumberOfItems;
        if (GameObject.Find("CapacityText"))
        {
            GameObject.Find("CapacityText").GetComponent<Text>().text = "Max Friends Active: " + maxNumberOfItems;
        }

        luck = data.luck;
        if (GameObject.Find("LuckText"))
        {
            GameObject.Find("LuckText").GetComponent<Text>().text = "Luck: " + luck;
        }

        damage = data.damage;
        if (GameObject.Find("DamageText"))
        {
            GameObject.Find("DamageText").GetComponent<Text>().text = "Damage: " + damage;
        }

        moneySaved = data.moneySaved;
        if(GameObject.Find("Money Text"))
        {
            GameObject.Find("Money Text").GetComponent<Text>().text = "Money: " + moneySaved;
        }
        #endregion

        characterName = data.selectedCharacterName;

        unlockedCharacterCount = 0;

        unlockableBlueprints["Libra"] = new UnlockableBlueprint(false, 0);
        unlockableBlueprints["Ushi"] = new UnlockableBlueprint(false, 0);
        unlockableBlueprints["Tofu"] = new UnlockableBlueprint(false, 0);
        unlockableBlueprints["Moyasi"] = new UnlockableBlueprint(false, 0);
        unlockableBlueprints["Cap"] = new UnlockableBlueprint(false, 0);
        unlockableBlueprints["Shift"] = new UnlockableBlueprint(false, 0);
        unlockableBlueprints["Ryytikki"] = new UnlockableBlueprint(false, 0);
        unlockableBlueprints["Key"] = new UnlockableBlueprint(false, 0);
        unlockableBlueprints["Ryan"] = new UnlockableBlueprint(false, 0);
        unlockableBlueprints["Honey"] = new UnlockableBlueprint(false, 0);
        unlockableBlueprints["Denim"] = new UnlockableBlueprint(false, 0);
        unlockableBlueprints["Sound"] = new UnlockableBlueprint(false, 0);
        unlockableBlueprints["Jack"] = new UnlockableBlueprint(false, 0);
        unlockableBlueprints["Robin"] = new UnlockableBlueprint(false, 13);

        foreach(string name in data.nameOfUnlockedBlueprints)
        {
            unlockableBlueprints[name].isUnlocked = true;
            if (GameObject.Find(name))
            {
                GameObject.Find("char_" + name).SetActive(false);
                GameObject.Find(name).GetComponent<Image>().color = new Color(255, 255, 255, 255);
                GameObject.Find(name).GetComponent<Button>().onClick.RemoveAllListeners();
                GameObject.Find(name).GetComponent<Button>().onClick.AddListener(CharacterSelecter);
            }
            unlockedCharacterCount++;//umm idk how to unsave
            //ok so the saving and loading works fine
            //i just need to clear the save for testing
            //delete the save file stupid
        }

        if (Master.instance)
        {
            moneySaved += Master.instance.moneyCollectedDuringRun;
            Master.instance.moneyCollectedDuringRun = 0;
            SaveSystem.SaveUpgrades(this);
        }
    }

    #region upgrades
    public void HealthUpgrade()
    {
        if(EventSystem.current.currentSelectedGameObject.name == "Less" && health > 0)
        {
            health--;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && health < 10)
        {
            health++;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("HealthText").GetComponent<Text>().text = "Health: " + health;
    }

    public void MovementUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && moveSpeed > 0)
        {
            moveSpeed -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && moveSpeed < 100)
        {
            moveSpeed += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("MoveText").GetComponent<Text>().text = "Movement Speed: " + moveSpeed;
    }

    public void RegenUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && blockRegen > 0)
        {
            blockRegen -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && blockRegen < 100)
        {
            blockRegen += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("RegenText").GetComponent<Text>().text = "Regen: " + blockRegen;
    }

    public void CountUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && bulletCount > 0)
        {
            bulletCount--;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && bulletCount < 2)
        {
            bulletCount++;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("BCountText").GetComponent<Text>().text = "Bullet Count: " + bulletCount;
    }

    public void ArmourUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && armour > 0)
        {
            armour -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && armour < 100)
        {
            armour += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("ArmourText").GetComponent<Text>().text = "Armour: " + armour;
    }

    public void SizeUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && size > 0)
        {
            size -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && size < 100)
        {
            size += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("SizeText").GetComponent<Text>().text = "Attack Size: " + size;
    }

    public void BulletSpeedUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && bulletSpeed > 0)
        {
            bulletSpeed -= 5;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && bulletSpeed < 30)
        {
            bulletSpeed += 5;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("BSpeedText").GetComponent<Text>().text = "Bullet Speed: " + bulletSpeed;
    }

    public void DamageUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && damage > 0)
        {
            damage--;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && damage < 5)
        {
            damage++;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("DamageText").GetComponent<Text>().text = "Damage: " + damage;
    }

    public void MoneyUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && moneyMultiplier > 0)
        {
            moneyMultiplier--;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && moneyMultiplier < 2)
        {
            moneyMultiplier++;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("MoneyMultText").GetComponent<Text>().text = "Money Multiplier: " + moneyMultiplier;
    }

    /*public void EXPUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && exp > 0)
        {
            exp -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && exp < 100)
        {
            exp += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("EXPText").GetComponent<Text>().text = "EXP Multiplier: " + exp;
    }*/

    public void CapacityUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && maxNumberOfItems > 0)
        {
            maxNumberOfItems -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && maxNumberOfItems < 100)
        {
            maxNumberOfItems += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("CapacityText").GetComponent<Text>().text = "Max Friends Active: " + maxNumberOfItems;
    }

    public void LuckUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && luck > 0)
        {
            luck--;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && luck < 100)
        {
            luck++;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("LuckText").GetComponent<Text>().text = "Luck: " + luck;
    }
    #endregion


    public void SaveUpgrades()
    {
        SaveSystem.SaveUpgrades(this); //really shouldn't be using the same name but eh fuck it, who's gonna fire me, myself?
    }

    public void CharacterSelecter()
    {
        characterName = EventSystem.current.currentSelectedGameObject.name;
        SaveSystem.SaveUpgrades(this);
        SceneManager.LoadScene("Main Game");
    }

    public void CharacterUnlocker()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        
        if (name.StartsWith("char_"))
        {
            string characterName = name.Substring(5);
            if (!unlockableBlueprints[characterName].isUnlocked && (/*moneySaved >= unlockableBlueprints[characterName].price &&*/ unlockedCharacterCount >= unlockableBlueprints[characterName].requiredUnlocks)) //test if character is not unl-coked
            {
                unlockedCharacterCount++;
                unlockableBlueprints[characterName].isUnlocked = true;
                //moneySaved -= unlockableBlueprints[characterName].price;
                GameObject.Find("Money Text").GetComponent<Text>().text = "Money: " + moneySaved;
                EventSystem.current.currentSelectedGameObject.transform.parent.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                EventSystem.current.currentSelectedGameObject.transform.parent.GetComponent<Button>().onClick.AddListener(CharacterSelecter);
                EventSystem.current.currentSelectedGameObject.SetActive(false);
                SaveSystem.SaveUpgrades(this);
            }
        }
    }

    public void BlueprintUnlocker()
    {
        //PlayerData data = SaveSystem.LoadUpgrades();

        List<string> lockedBPs = new List<string>();

        foreach(string bp in unlockableBlueprints.Keys)
        {
            if(unlockableBlueprints[bp].isUnlocked == false && unlockedCharacterCount >= unlockableBlueprints[bp].requiredUnlocks)
            {
                lockedBPs.Add(bp);
            }
        }

        if (lockedBPs.Count > 0)
        {
            var randomBP = lockedBPs.ElementAt(Random.Range(0, lockedBPs.Count));

            unlockableBlueprints[randomBP].isUnlocked = true;
            unlockedCharacterCount++;

            //GameObject.Find(randomBP).transform.GetComponent<Image>().color = new Color(255, 255, 255, 255); //remove later
            Debug.Log(randomBP);
        }

        
        SaveSystem.SaveUpgrades(this);
    }

    public void BlueprintLocker()
    {
        PlayerData data = SaveSystem.LoadUpgrades();

        data.nameOfUnlockedBlueprints = new string[0];
        unlockedCharacterCount = 0;

        foreach(string name in unlockableBlueprints.Keys)
        {
            unlockableBlueprints[name].isUnlocked = false;
        }

        SaveSystem.SaveUpgrades(this);
    }

    public void TestMoney() //delete later
    {
        moneySaved += 10000;
        SaveSystem.SaveUpgrades(this);
        GameObject.Find("Money Text").GetComponent<Text>().text = "Money: " + moneySaved;
    }

    public class UnlockableBlueprint
    {
        public bool isUnlocked = false;
        //public int price;
        public int requiredUnlocks;

        public UnlockableBlueprint(bool isUnlocked, int requiredUnlocks)
        {
            this.isUnlocked = isUnlocked;
            //this.price = price;
            this.requiredUnlocks = requiredUnlocks;
        }
    }
}