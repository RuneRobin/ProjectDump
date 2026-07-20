using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public static ItemDisplay instance;
    
    public GameObject firstItem;
    public GameObject secondItem;
    public GameObject thirdItem;
    public GameObject fourthItem;
    public GameObject fifthItem;

    public int nextItem = 0;

    public int squareScrap = 0;
    public int circleScrap = 0;
    public int triangleScrap = 0;
    public int basicScrap = 0;

    //public GameObject[] allItems;
    public List<GameObject> allItems;
    private GameObject currItem;

    public int maxNumberOfItems = 5;
    public List<GameObject> heldItems;

    public string itemToAdd;

    private PlayerScript player;

    #region itemLevels
    public int teddyLevel = 0;
    public int bagOfMarblesLevel = 0;
    public int fishingRodLevel = 0;
    public int starFragmentLevel = 0;
    public int pharaohCowlLevel = 0;
    public int tBoneSteakLevel = 0;
    public int squigglyBracketLevel = 0;
    public int dnaSplicerLevel = 0;
    public int microphoneLevel = 0;
    public int conchShellLevel = 0;
    public int spiderInABoxLevel = 0;
    public int bouncyBallLevel = 0;
    public int telescopeLevel = 0;
    public int sproutLevel = 0;
    public int solSapphireLevel = 0;
    public int mugLevel = 0;
    public int bagFullOfColdHardCashLevel = 0;
    public int almanacLevel = 0;
    public int firmwareUpdateLevel = 0;
    public int corpsePaintLevel = 0;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        allItems = new List<GameObject>(Resources.LoadAll<GameObject>("Sprites/Items/Game Items"));
        player = PlayerScript.instance;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItemToUI()
    {

        foreach (GameObject item in allItems)
        {
            if (item.name == itemToAdd)
            {
                nextItem++;
                currItem = item;
                heldItems.Add(item);
            }
        }

        if(nextItem == 1)
        {
            //currItem = Instantiate(currItem, firstItem.transform.position, firstItem.transform.rotation);
            //currItem.transform.parent = firstItem.transform;
            firstItem.GetComponent<Image>().sprite = currItem.GetComponent<SpriteRenderer>().sprite;
        }
        if (nextItem == 2)
        {
            //currItem = Instantiate(currItem, secondItem.transform.position, secondItem.transform.rotation);
            //currItem.transform.parent = secondItem.transform;
            secondItem.GetComponent<Image>().sprite = currItem.GetComponent<SpriteRenderer>().sprite;
        }
        if (nextItem == 3)
        {
            //currItem = Instantiate(currItem, thirdItem.transform.position, thirdItem.transform.rotation);
            //currItem.transform.parent = thirdItem.transform;
            thirdItem.GetComponent<Image>().sprite = currItem.GetComponent<SpriteRenderer>().sprite;
        }
        if (nextItem == 4)
        {
            //currItem = Instantiate(currItem, fourthItem.transform.position, fourthItem.transform.rotation);
            //currItem.transform.parent = fourthItem.transform;
            fourthItem.GetComponent<Image>().sprite = currItem.GetComponent<SpriteRenderer>().sprite;
        }
        if (nextItem == 5)
        {
            //currItem = Instantiate(currItem, fifthItem.transform.position, fifthItem.transform.rotation);
            //currItem.transform.parent = fifthItem.transform;
            fifthItem.GetComponent<Image>().sprite = currItem.GetComponent<SpriteRenderer>().sprite;
        }

        itemToAdd = null;
        currItem = null;

    }


    public void TeddyBear()
    {
        teddyLevel++;
        if (teddyLevel == 1)
        {
            player.maxHealth += 30;
            player.health += 30;
        }
        if (teddyLevel == 2)
        {
            player.maxHealth += 30;
            player.health += 30;
        }
        if (teddyLevel == 3)
        {
            player.maxHealth += 40;
            player.health += 40;
        }
    }

    public void BagOfMarbles()
    {
        bagOfMarblesLevel++;
        if (bagOfMarblesLevel == 1)
        {
            player.bonusGunArmBulletCount++;
            player.UpgradeUpdater("BonusGunArmBulletCount");
        }
        if (bagOfMarblesLevel == 2)
        {
            player.bonusGunArmBulletCount++;
            player.UpgradeUpdater("BonusGunArmBulletCount");
        }
        if (bagOfMarblesLevel == 3)
        {
            player.bonusGunArmBulletCount++;
            player.UpgradeUpdater("BonusGunArmBulletCount");
        }
    }

    public void FishingRod()
    {
        fishingRodLevel++;
        if (fishingRodLevel == 1)
        {
            player.bonusArmWindUpReduction += 0.5f;
            player.UpgradeUpdater("BonusWindUpReduction");
        }
        if (fishingRodLevel == 2)
        {
            player.bonusArmWindUpReduction += 0.5f;
            player.UpgradeUpdater("BonusWindUpReduction");
        }
        if (fishingRodLevel == 3)
        {
            player.bonusArmWindUpReduction += 0.5f;
            player.UpgradeUpdater("BonusWindUpReduction");
        }
    }

    public void StarFragment()
    {
        starFragmentLevel++;
        if (starFragmentLevel == 1)
        {
            player.bonusSwordArmCritDamage += 0.3f;
            player.UpgradeUpdater("BonusSwordArmCritDamage");
        }
        if (starFragmentLevel == 2)
        {
            player.bonusSwordArmCritDamage += 0.3f;
            player.UpgradeUpdater("BonusSwordArmCritDamage");
        }
        if (starFragmentLevel == 3)
        {
            player.bonusSwordArmCritDamage += 0.4f;
            player.UpgradeUpdater("BonusSwordArmCritDamage");
        }
    }

    public void PharaohCowl()
    {
        pharaohCowlLevel++;
        if (pharaohCowlLevel == 1)
        {
            player.bonusArmBurn++;
            player.UpgradeUpdater("BonusArmBurn");
        }
        if (pharaohCowlLevel == 2)
        {
            player.bonusArmBurn++;
            player.UpgradeUpdater("BonusArmBurn");
        }
        if (pharaohCowlLevel == 3)
        {
            player.bonusArmBurn++;
            player.UpgradeUpdater("BonusArmBurn");
        }
    }

    public void TBoneSteak()
    {
        tBoneSteakLevel++;
        if (tBoneSteakLevel == 1)
        {
            player.blockRegen++;
        }
        if (tBoneSteakLevel == 2)
        {
            player.blockRegen++;
        }
        if (tBoneSteakLevel == 3)
        {
            player.blockRegen++;
        }
    }

    public void SquigglyBracket()
    {
        squigglyBracketLevel++;
        if (squigglyBracketLevel == 1)
        {
            player.bonusArmDurability++;
            player.UpgradeUpdater("BonusDurability");
        }
        if (squigglyBracketLevel == 2)
        {
            player.bonusArmDurability++;
            player.UpgradeUpdater("BonusDurability");
        }
        if (squigglyBracketLevel == 3)
        {
            player.bonusArmDurability++;
            player.UpgradeUpdater("BonusDurability");
        }
    }

    public void DNASplicer()
    {
        dnaSplicerLevel++;
        if (dnaSplicerLevel == 1)
        {
            player.speed += 3;
        }
        if (dnaSplicerLevel == 2)
        {
            player.speed += 3;
        }
        if (dnaSplicerLevel == 3)
        {
            player.speed += 4;
        }
    }

    public void Microphone()
    {
        microphoneLevel++;
        if (microphoneLevel == 1)
        {
            Master.instance.difficulty++;
        }
        if (microphoneLevel == 2)
        {
            Master.instance.difficulty++;
        }
        if (microphoneLevel == 3)
        {
            Master.instance.difficulty++;
        }
    }

    public void ConchShell()
    {
        conchShellLevel++;
        if (conchShellLevel == 1)
        {
            player.bonusArmFreeze++;
            player.UpgradeUpdater("BonusArmFreeze");
        }
        if (conchShellLevel == 2)
        {
            player.bonusArmFreeze++;
            player.UpgradeUpdater("BonusArmFreeze");
        }
        if (conchShellLevel == 3)
        {
            player.bonusArmFreeze++;
            player.UpgradeUpdater("BonusArmFreeze");
        }
    }

    public void SpiderInABox()
    {
        spiderInABoxLevel++;
        if (spiderInABoxLevel == 1)
        {
            player.bonusGunArmFirerate += 0.1f;
            player.UpgradeUpdater("BonusGunArmFirerate");
        }
        if (spiderInABoxLevel == 2)
        {
            player.bonusGunArmFirerate += 0.1f;
            player.UpgradeUpdater("BonusGunArmFirerate");
        }
        if (spiderInABoxLevel == 3)
        {
            player.bonusGunArmFirerate += 0.2f;
            player.UpgradeUpdater("BonusGunArmFirerate");
        }
    }

    public void BouncyBall()
    {
        bouncyBallLevel++;
        if (bouncyBallLevel == 1)
        {
            player.bonusFistArmSpeed += 10;
            player.bonusSwordArmSpeed += 50;
            player.bonusGunArmBulletSpeed += 10;
            player.UpgradeUpdater("BonusFistArmSpeed");
            player.UpgradeUpdater("BonusSwordArmSpeed");
            player.UpgradeUpdater("BonusGunArmBulletSpeed");
        }
        if (bouncyBallLevel == 2)
        {
            player.bonusFistArmSpeed += 10;
            player.bonusSwordArmSpeed += 50;
            player.bonusGunArmBulletSpeed += 10;
            player.UpgradeUpdater("BonusFistArmSpeed");
            player.UpgradeUpdater("BonusSwordArmSpeed");
            player.UpgradeUpdater("BonusGunArmBulletSpeed");
        }
        if (bouncyBallLevel == 3)
        {
            player.bonusFistArmSpeed += 10;
            player.bonusSwordArmSpeed += 50;
            player.bonusGunArmBulletSpeed += 10;
            player.UpgradeUpdater("BonusFistArmSpeed");
            player.UpgradeUpdater("BonusSwordArmSpeed");
            player.UpgradeUpdater("BonusGunArmBulletSpeed");
        }
    }

    public void Telescope()
    {
        telescopeLevel++;
        if (telescopeLevel == 1)
        {
            player.bonusFistArmWoOExtension += 0.5f;
            player.UpgradeUpdater("BonusFistArmWoOExtension");
        }
        if (telescopeLevel == 2)
        {
            player.bonusFistArmWoOExtension += 0.5f;
            player.UpgradeUpdater("BonusFistArmWoOExtension");
        }
        if (telescopeLevel == 3)
        {
            player.bonusFistArmWoOExtension += 1.5f;
            player.UpgradeUpdater("BonusFistArmWoOExtension");
        }
    }

    public void Sprout()
    {
        sproutLevel++;
        if (sproutLevel == 1)
        {
            player.bonusFistArmDamage++;
            player.UpgradeUpdater("BonusFistArmDamage");
        }
        if (sproutLevel == 2)
        {
            player.bonusFistArmDamage++;
            player.UpgradeUpdater("BonusFistArmDamage");
        }
        if (sproutLevel == 3)
        {
            player.bonusFistArmDamage++;
            player.UpgradeUpdater("BonusFistArmDamage");
        }
    }

    public void SolSapphire()
    {
        solSapphireLevel++;
        if (solSapphireLevel == 1)
        {
            player.bonusSwordArmDamage++;
            player.UpgradeUpdater("BonusSwordArmDamage");
        }
        if (solSapphireLevel == 2)
        {
            player.bonusSwordArmDamage++;
            player.UpgradeUpdater("BonusSwordArmDamage");
        }
        if (solSapphireLevel == 3)
        {
            player.bonusSwordArmDamage++;
            player.UpgradeUpdater("BonusSwordArmDamage");
        }
    }

    public void Mug()
    {
        mugLevel++;
        if (mugLevel == 1)
        {
            player.bonusGunArmDamage++;
            player.UpgradeUpdater("BonusGunArmDamage");
        }
        if (mugLevel == 2)
        {
            player.bonusGunArmDamage++;
            player.UpgradeUpdater("BonusGunArmDamage");
        }
        if (mugLevel == 3)
        {
            player.bonusGunArmDamage++;
            player.UpgradeUpdater("BonusGunArmDamage");
        }
    }

    public void BagFullOfColdHardCash()
    {
        bagFullOfColdHardCashLevel++;
        if (bagFullOfColdHardCashLevel == 1)
        {
            player.bonusArmBleed++;
            player.UpgradeUpdater("BonusArmBleed");
        }
        if (bagFullOfColdHardCashLevel == 2)
        {
            player.bonusArmBleed++;
            player.UpgradeUpdater("BonusArmBleed");
        }
        if (bagFullOfColdHardCashLevel == 3)
        {
            player.bonusArmBleed++;
            player.UpgradeUpdater("BonusArmBleed");
        }
    }

    public void Almanac()
    {
        almanacLevel++;
        if (almanacLevel == 1)
        {
            player.bonusArmShock++;
            player.UpgradeUpdater("BonusArmShock");
        }
        if (almanacLevel == 2)
        {
            player.bonusArmShock++;
            player.UpgradeUpdater("BonusArmShock");
        }
        if (almanacLevel == 3)
        {
            player.bonusArmShock++;
            player.UpgradeUpdater("BonusArmShock");
        }
    }

    public void FirmwareUpdate()
    {
        firmwareUpdateLevel++;
        if (firmwareUpdateLevel == 1)
        {
            player.bonusArmVirus++;
            player.UpgradeUpdater("BonusArmVirus");
        }
        if (firmwareUpdateLevel == 2)
        {
            player.bonusArmVirus++;
            player.UpgradeUpdater("BonusArmVirus");
        }
        if (firmwareUpdateLevel == 3)
        {
            player.bonusArmVirus++;
            player.UpgradeUpdater("BonusArmVirus");
        }
    }

    public void CorpsePaint()
    {
        corpsePaintLevel++;
        if (corpsePaintLevel == 1)
        {
            player.bonusArmMaxVirus += 5;
            player.UpgradeUpdater("BonusArmMaxVirus");
        }
        if (corpsePaintLevel == 2)
        {
            player.bonusArmMaxVirus += 5;
            player.UpgradeUpdater("BonusArmMaxVirus");
        }
        if (corpsePaintLevel == 3)
        {
            player.bonusArmMaxVirus += 20;
            player.UpgradeUpdater("BonusArmMaxVirus");
        }
    }

}
