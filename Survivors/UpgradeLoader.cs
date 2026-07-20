using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeLoader : MonoBehaviour
{
    public int health;
    public int regen;
    public int armour;
    public int count;
    public int bSpeed;
    public int mSpeed;
    public int size;
    public int money;
    public int exp;
    public int capacity;
    public int luck;
    public int damage;
    public string characterName;

    public int moneyOnHand;

    public Dictionary<string, UnlockableCharacter> unlockableCharacters = new Dictionary<string, UnlockableCharacter>(); //think emoji
    public int unlockedCharacterCount;
    
    #region unlockable characters
    public bool libraUnlocked = false; //all useless?
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

        

        health = data.health;
        if (GameObject.Find("HealthText"))
        {
            GameObject.Find("HealthText").GetComponent<Text>().text = "Health: " + health;
        }

        regen = data.regen;
        if (GameObject.Find("RegenText"))
        {
            GameObject.Find("RegenText").GetComponent<Text>().text = "Regen: " + regen;
        }

        armour = data.armour;
        if (GameObject.Find("ArmourText"))
        {
            GameObject.Find("ArmourText").GetComponent<Text>().text = "Armour: " + armour;
        }

        count = data.count;
        if (GameObject.Find("BCountText"))
        {
            GameObject.Find("BCountText").GetComponent<Text>().text = "Bullet Count: " + count;
        }

        bSpeed = data.bSpeed;
        if (GameObject.Find("BSpeedText"))
        {
            GameObject.Find("BSpeedText").GetComponent<Text>().text = "Bullet Speed: " + bSpeed;
        }

        mSpeed = data.mSpeed;
        if (GameObject.Find("MoveText"))
        {
            GameObject.Find("MoveText").GetComponent<Text>().text = "Movement Speed: " + mSpeed;
        }

        size = data.size;
        if (GameObject.Find("SizeText"))
        {
            GameObject.Find("SizeText").GetComponent<Text>().text = "Attack Size: " + size;
        }

        money = data.money;
        if (GameObject.Find("MoneyMultText"))
        {
            GameObject.Find("MoneyMultText").GetComponent<Text>().text = "Money Multiplier: " + money;
        }

        exp = data.exp;
        if (GameObject.Find("EXPText"))
        {
            GameObject.Find("EXPText").GetComponent<Text>().text = "EXP Multiplier: " + exp;
        }

        capacity = data.capacity;
        if (GameObject.Find("CapacityText"))
        {
            GameObject.Find("CapacityText").GetComponent<Text>().text = "Max Friends Active: " + capacity;
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

        moneyOnHand = data.moneyOnHand;
        if(GameObject.Find("Money Text"))
        {
            GameObject.Find("Money Text").GetComponent<Text>().text = "Money: " + moneyOnHand;
        }

        unlockedCharacterCount = 0;

        unlockableCharacters["Libra"] = new UnlockableCharacter(false, 100, 0);
        unlockableCharacters["Ushi"] = new UnlockableCharacter(false, 100, 0);
        unlockableCharacters["Tofu"] = new UnlockableCharacter(false, 150, 0);
        unlockableCharacters["Moyasi"] = new UnlockableCharacter(false, 150, 0);
        unlockableCharacters["Cap"] = new UnlockableCharacter(false, 200, 0);
        unlockableCharacters["Shift"] = new UnlockableCharacter(false, 200, 0);
        unlockableCharacters["Ryytikki"] = new UnlockableCharacter(false, 300, 0);
        unlockableCharacters["Key"] = new UnlockableCharacter(false, 300, 0);
        unlockableCharacters["Ryan"] = new UnlockableCharacter(false, 400, 0);
        unlockableCharacters["Honey"] = new UnlockableCharacter(false, 400, 0);
        unlockableCharacters["Denim"] = new UnlockableCharacter(false, 500, 0);
        unlockableCharacters["Sound"] = new UnlockableCharacter(false, 500, 0);
        unlockableCharacters["Jack"] = new UnlockableCharacter(false, 600, 0);
        unlockableCharacters["Robin"] = new UnlockableCharacter(false, 0, 13);

        foreach(string name in data.nameOfUnlockedCharacters)
        {
            unlockableCharacters[name].isUnlocked = true;
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

        if (GameObject.Find("Master"))
        {
            moneyOnHand += Master.instance.moneyCollectedDuringRun;
            Master.instance.moneyCollectedDuringRun = 0;
            SaveSystem.SaveUpgrades(this);
        }

    }

    #region upgrades
    public void HealthUpgrade()
    {
        if(EventSystem.current.currentSelectedGameObject.name == "Less" && health > 0)
        {
            health -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && health < 100)
        {
            health += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("HealthText").GetComponent<Text>().text = "Health: " + health;
    }

    public void MovementUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && mSpeed > 0)
        {
            mSpeed -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && mSpeed < 100)
        {
            mSpeed += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("MoveText").GetComponent<Text>().text = "Movement Speed: " + mSpeed;
    }

    public void RegenUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && regen > 0)
        {
            regen -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && regen < 100)
        {
            regen += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("RegenText").GetComponent<Text>().text = "Regen: " + regen;
    }

    public void CountUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && count > 0)
        {
            count -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && count < 100)
        {
            count += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("BCountText").GetComponent<Text>().text = "Bullet Count: " + count;
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
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && bSpeed > 0)
        {
            bSpeed -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && bSpeed < 100)
        {
            bSpeed += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("BSpeedText").GetComponent<Text>().text = "Bullet Speed: " + bSpeed;
    }

    public void DamageUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && damage > 0)
        {
            damage -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && damage < 100)
        {
            damage += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("DamageText").GetComponent<Text>().text = "Damage: " + damage;
    }

    public void MoneyUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && money > 0)
        {
            money -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && money < 100)
        {
            money += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("MoneyMultText").GetComponent<Text>().text = "Money Multiplier: " + money;
    }

    public void EXPUpgrade()
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
    }

    public void CapacityUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && capacity > 0)
        {
            capacity -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && capacity < 100)
        {
            capacity += 10;
        }
        SaveSystem.SaveUpgrades(this);

        GameObject.Find("CapacityText").GetComponent<Text>().text = "Max Friends Active: " + capacity;
    }

    public void LuckUpgrade()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Less" && luck > 0)
        {
            luck -= 10;
        }
        if (EventSystem.current.currentSelectedGameObject.name == "More" && luck < 100)
        {
            luck += 10;
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
        SceneManager.LoadScene("SampleScene");
    }

    public void CharacterUnlocker()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;

        if (name.StartsWith("char_"))
        {
            string characterName = name.Substring(5);
            if (!unlockableCharacters[characterName].isUnlocked && (moneyOnHand >= unlockableCharacters[characterName].price && unlockedCharacterCount >= unlockableCharacters[characterName].requiredUnlocks)) //test if character is not unl-coked
            {
                unlockedCharacterCount++; 
                unlockableCharacters[characterName].isUnlocked = true;
                moneyOnHand -= unlockableCharacters[characterName].price;
                GameObject.Find("Money Text").GetComponent<Text>().text = "Money: " + moneyOnHand;
                EventSystem.current.currentSelectedGameObject.transform.parent.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                EventSystem.current.currentSelectedGameObject.transform.parent.GetComponent<Button>().onClick.AddListener(CharacterSelecter);
                EventSystem.current.currentSelectedGameObject.SetActive(false);
                SaveSystem.SaveUpgrades(this);
            }
        }

    }

    public void TestMoney() //delete later
    {
        moneyOnHand += 10000;
        SaveSystem.SaveUpgrades(this);
        GameObject.Find("Money Text").GetComponent<Text>().text = "Money: " + moneyOnHand;
    }

    public class UnlockableCharacter
    {
        public bool isUnlocked = false;
        public int price;
        public int requiredUnlocks;

        public UnlockableCharacter(bool isUnlocked, int price, int requiredUnlocks)
        {
            this.isUnlocked = isUnlocked;
            this.price = price;
            this.requiredUnlocks = requiredUnlocks;
        }
    }

}