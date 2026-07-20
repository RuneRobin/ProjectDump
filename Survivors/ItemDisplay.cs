using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public GameObject firstItem;
    public GameObject secondItem;
    public GameObject thirdItem;
    public GameObject fourthItem;
    public GameObject fifthItem;

    public int nextItem = 0;

    //public GameObject[] allItems;
    public List<GameObject> allItems;
    private GameObject currItem;

    public int maxNumberOfItems = 5;
    public List<GameObject> heldItems;

    public string itemToAdd;

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
            currItem = Instantiate(currItem, firstItem.transform.position, firstItem.transform.rotation);
            currItem.transform.parent = firstItem.transform;
            //firstItem.sprite = currItem.GetComponent<SpriteRenderer>().sprite;
        }
        if (nextItem == 2)
        {
            currItem = Instantiate(currItem, secondItem.transform.position, secondItem.transform.rotation);
            currItem.transform.parent = secondItem.transform;
            //secondItem.sprite = currItem.GetComponent<SpriteRenderer>().sprite;
        }
        if (nextItem == 3)
        {
            currItem = Instantiate(currItem, thirdItem.transform.position, thirdItem.transform.rotation);
            currItem.transform.parent = thirdItem.transform;
            //thirdItem.sprite = currItem.GetComponent<SpriteRenderer>().sprite;
        }
        if (nextItem == 4)
        {
            currItem = Instantiate(currItem, fourthItem.transform.position, fourthItem.transform.rotation);
            currItem.transform.parent = fourthItem.transform;
            //fourthItem.sprite = currItem.GetComponent<SpriteRenderer>().sprite;
        }
        if (nextItem == 5)
        {
            currItem = Instantiate(currItem, fifthItem.transform.position, fifthItem.transform.rotation);
            currItem.transform.parent = fifthItem.transform;
            //fifthItem.sprite = currItem.GetComponent<SpriteRenderer>().sprite;
        }

        itemToAdd = null;
        currItem = null;

    }


    public void TeddyBear()
    {
        teddyLevel++;
        if (teddyLevel == 1)
        {
            PlayerMovement.instance.healthBoost += 100;
        }
        if (teddyLevel == 2)
        {
            PlayerMovement.instance.healthBoost += 100;
        }
        if (teddyLevel == 3)
        {
            PlayerMovement.instance.healthBoost += 100;
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Teddy Bear") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Teddy Bear") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Teddy Bear") as GameObject);

        }
    }

    public void BagOfMarbles()
    {
        bagOfMarblesLevel++;
        if (bagOfMarblesLevel == 1)
        {
            PlayerMovement.instance.countBoost += 1;
        }
        if (bagOfMarblesLevel == 2)
        {
            PlayerMovement.instance.countBoost += 1;
        }
        if (bagOfMarblesLevel == 3)
        {
            PlayerMovement.instance.countBoost += 1;
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Bag of Marbles") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Bag of Marbles") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Bag of Marbles") as GameObject);
        }
    }

    public void FishingRod()
    {
        fishingRodLevel++;
        if (fishingRodLevel == 1)
        {
            PlayerMovement.instance.sizeBoost += 1;
        }
        if (fishingRodLevel == 2)
        {
            PlayerMovement.instance.sizeBoost += 1;
        }
        if (fishingRodLevel == 3)
        {
            PlayerMovement.instance.sizeBoost += 1;
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Fishing Rod") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Fishing Rod") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Fishing Rod") as GameObject);
        }
    }

    public void StarFragment()
    {
        starFragmentLevel++;
        if (starFragmentLevel == 1)
        {
            PlayerMovement.instance.pickUpRange += 1;
        }
        if (starFragmentLevel == 2)
        {
            PlayerMovement.instance.pickUpRange += 1;
        }
        if (starFragmentLevel == 3)
        {
            PlayerMovement.instance.pickUpRange += 1;
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Star Fragment") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Star Fragment") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Star Fragment") as GameObject);
        }
    }

    public void PharaohCowl()
    {
        pharaohCowlLevel++;
        if (pharaohCowlLevel == 1)
        {
            PlayerMovement.instance.moneyBoost += 1;
        }
        if (pharaohCowlLevel == 2)
        {
            PlayerMovement.instance.moneyBoost += 1;
        }
        if (pharaohCowlLevel == 3)
        {
            PlayerMovement.instance.moneyBoost += 1;
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Pharaoh's Cowl") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Pharaoh's Cowl") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Pharaoh's Cowl") as GameObject);
        }
    }

    public void TBoneSteak()
    {
        tBoneSteakLevel++;
        if (tBoneSteakLevel == 1)
        {
            PlayerMovement.instance.armourBoost += 1;
        }
        if (tBoneSteakLevel == 2)
        {
            PlayerMovement.instance.armourBoost += 1;
        }
        if (tBoneSteakLevel == 3)
        {
            PlayerMovement.instance.armourBoost += 1;
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/T-Bone Steak") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/T-Bone Steak") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/T-Bone Steak") as GameObject);
        }
    }

    public void SquigglyBracket()
    {
        squigglyBracketLevel++;
        if (squigglyBracketLevel == 1)
        {
            //cooldown
        }
        if (squigglyBracketLevel == 2)
        {
            //cooldown
        }
        if (squigglyBracketLevel == 3)
        {
            //cooldown
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Squiggly Bracket") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Squiggly Bracket") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Squiggly Bracket") as GameObject);
        }
    }

    public void DNASplicer()
    {
        dnaSplicerLevel++;
        if (dnaSplicerLevel == 1)
        {
            //unknown as of yet
        }
        if (dnaSplicerLevel == 2)
        {
            //unknown as of yet
        }
        if (dnaSplicerLevel == 3)
        {
            //unknown as of yet
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/DNA Splicer") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/DNA Splicer") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/DNA Splicer") as GameObject);
        }
    }

    public void Microphone()
    {
        microphoneLevel++;
        if (microphoneLevel == 1)
        {
            //enemy spawns
        }
        if (microphoneLevel == 2)
        {
            //enemy spawns
        }
        if (microphoneLevel == 3)
        {
            //enemy spawns
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Microphone") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Microphone") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Microphone") as GameObject);
        }
    }

    public void ConchShell()
    {
        conchShellLevel++;
        if (conchShellLevel == 1)
        {
            //???
        }
        if (conchShellLevel == 2)
        {
            //???
        }
        if (conchShellLevel == 3)
        {
            //???
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Conch Shell") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Conch Shell") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Conch Shell") as GameObject);
        }
    }

    public void SpiderInABox()
    {
        spiderInABoxLevel++;
        if (spiderInABoxLevel == 1)
        {
            //???
        }
        if (spiderInABoxLevel == 2)
        {
            //???
        }
        if (spiderInABoxLevel == 3)
        {
            //???
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Spider In A Box") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Spider In A Box") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Spider In A Box") as GameObject);
        }
    }

    public void BouncyBall()
    {
        bouncyBallLevel++;
        if (bouncyBallLevel == 1)
        {
            //???
        }
        if (bouncyBallLevel == 2)
        {
            //???
        }
        if (bouncyBallLevel == 3)
        {
            //???
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Bouncy Ball") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Bouncy Ball") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Bouncy Ball") as GameObject);
        }
    }

    public void Telescope()
    {
        telescopeLevel++;
        if (telescopeLevel == 1)
        {
            //???
        }
        if (telescopeLevel == 2)
        {
            //???
        }
        if (telescopeLevel == 3)
        {
            //???
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Telescope") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Telescope") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Telescope") as GameObject);
        }
    }

    public void Sprout()
    {
        sproutLevel++;
        if (sproutLevel == 1)
        {
            //???
        }
        if (sproutLevel == 2)
        {
            //???
        }
        if (sproutLevel == 3)
        {
            //???
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Sprout") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Sprout") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Sprout") as GameObject);
        }
    }

    public void SolSapphire()
    {
        solSapphireLevel++;
        if (solSapphireLevel == 1)
        {
            //???
        }
        if (solSapphireLevel == 2)
        {
            //???
        }
        if (solSapphireLevel == 3)
        {
            //???
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Sol Sapphire") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Sol Sapphire") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Sol Sapphire") as GameObject);
        }
    }

    public void Mug()
    {
        mugLevel++;
        if (mugLevel == 1)
        {
            //???
        }
        if (mugLevel == 2)
        {
            //???
        }
        if (mugLevel == 3)
        {
            //???
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Mug") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Mug") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Mug") as GameObject);
        }
    }

    public void BagFullOfColdHardCash()
    {
        bagFullOfColdHardCashLevel++;
        if (bagFullOfColdHardCashLevel == 1)
        {
            //???
        }
        if (bagFullOfColdHardCashLevel == 2)
        {
            //???
        }
        if (bagFullOfColdHardCashLevel == 3)
        {
            //???
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Bag Full Of Cold Hard Cash") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Bag Full Of Cold Hard Cash") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Bag Full Of Cold Hard Cash") as GameObject);
        }
    }

    public void Almanac()
    {
        almanacLevel++;
        if (almanacLevel == 1)
        {
            //???
        }
        if (almanacLevel == 2)
        {
            //???
        }
        if (almanacLevel == 3)
        {
            //???
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Almanac") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Almanac") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Almanac") as GameObject);
        }
    }

    public void FirmwareUpdate()
    {
        firmwareUpdateLevel++;
        if (firmwareUpdateLevel == 1)
        {
            //???
        }
        if (firmwareUpdateLevel == 2)
        {
            //???
        }
        if (firmwareUpdateLevel == 3)
        {
            //???
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Firmware Update") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Firmware Update") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Firmware Update") as GameObject);
        }
    }

    public void CorpsePaint()
    {
        corpsePaintLevel++;
        if (corpsePaintLevel == 1)
        {
            //???
        }
        if (corpsePaintLevel == 2)
        {
            //???
        }
        if (corpsePaintLevel == 3)
        {
            //???
            GameObject.Find("Item Menu").transform.gameObject.GetComponent<ItemMenu>().itemList.Remove(Resources.Load("Sprites/Items/Game Items/Corpse Paint") as GameObject);
            allItems.Remove(Resources.Load("Sprites/Items/Game Items/Corpse Paint") as GameObject);
            heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Corpse Paint") as GameObject);
        }
    }

}
