using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemMenu : MonoBehaviour
{
    public List<GameObject> itemList;
    private List<int> randomNumberList;

    private ItemDisplay itemDisplayer;

    private bool callList = false;
    private bool noMoreItems = false;

    public Button firstItem;
    public Text firstItemText;
    public Image firstImage;

    public Button secondItem;
    public Text secondItemText;
    public Image secondImage;

    public Button thirdItem;
    public Text thirdItemText;
    public Image thirdImage;

    public Text iText;

    // Start is called before the first frame update
    void Awake()
    {
        //allItems = Resources.LoadAll<GameObject>("Sprites/Items/Game Items");
        itemDisplayer = ItemDisplay.instance;
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

    void OnEnable()
    {
        if (callList == false) 
        {
            itemList = new List<GameObject>(itemDisplayer.allItems); //if there is space for more new items it will pull from all possible items
            callList = true;
        }
        if(noMoreItems == false && itemDisplayer.nextItem == itemDisplayer.maxNumberOfItems)
        {
            itemList = itemDisplayer.heldItems;
            noMoreItems = true;

            //itemList = new List<GameObject>(itemDisplayer.heldItems); //if the player has the maximum amount of items, it will only pull from them
            //something to do here about max level items
        }

        randomNumberList = RandomPick(itemList.Count, 0, itemList.Count);

        if (itemList.Count >= 1)
        {
            firstImage.sprite = itemList[randomNumberList[0]].GetComponent<SpriteRenderer>().sprite;
            firstItem.onClick.RemoveAllListeners();
            iText = firstItemText;
            firstItem.onClick.AddListener(Categorizer());
            firstItemText = iText;
            randomNumberList.RemoveAt(0);
        }
        else //catches if there are not enough items. At 0 it should not give you the option to click on new friend to begin with but deal with that later
        {
            firstImage.sprite = null;
            firstItem.onClick.RemoveAllListeners();
            firstItemText.text = "No more items here";
        }

        if (itemList.Count >= 2)
        {
            secondImage.sprite = itemList[randomNumberList[0]].GetComponent<SpriteRenderer>().sprite;
            secondItem.onClick.RemoveAllListeners();
            iText = secondItemText;
            secondItem.onClick.AddListener(Categorizer());
            secondItemText = iText;
            randomNumberList.RemoveAt(0);
        }
        else //catches if there are not enough friends. At 0 it should not give you the option to click on new friend to begin with but deal with that later
        {
            secondImage.sprite = null;
            secondItem.onClick.RemoveAllListeners();
            secondItemText.text = "No more items here";
        }

        if (itemList.Count >= 3)
        {
            thirdImage.sprite = itemList[randomNumberList[0]].GetComponent<SpriteRenderer>().sprite;
            thirdItem.onClick.RemoveAllListeners();
            iText = thirdItemText;
            thirdItem.onClick.AddListener(Categorizer());
            thirdItemText = iText;
            randomNumberList.RemoveAt(0);
        }
        else //catches if there are not enough friends. At 0 it should not give you the option to click on new friend to begin with but deal with that later
        {
            thirdImage.sprite = null;
            thirdItem.onClick.RemoveAllListeners();
            thirdItemText.text = "No more items here";
        }


    }

    private void OnDisable()
    {
        Time.timeScale = 1; //unpauses game
    }

    private UnityAction Categorizer()
    {
        #region finds items from all items
        if (noMoreItems == false)
        {
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Teddy Bear")
            {
                string itemText = "Teddy Bear goes rawr hahaha rawr hahaha";
                iText.text = itemText;
                return TeddyBear;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Bag of Marbles")
            {
                string itemText = "*Marbels";
                iText.text = itemText;
                return BagOfMarbles;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Fishing Rod")
            {
                string itemText = "Okay but does this game have fishing?";
                iText.text = itemText;
                return FishingRod;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Star Fragment")
            {
                string itemText = "can be sold for a high price";
                iText.text = itemText;
                return StarFragment;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Pharaoh's Cowl")
            {
                string itemText = "tut.caw";
                iText.text = itemText;
                return PharaohCowl;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "T-Bone Steak")
            {
                string itemText = "yummers";
                iText.text = itemText;
                return TBoneSteak;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Squiggly Bracket")
            {
                string itemText = "you forgot the ;";
                iText.text = itemText;
                return SquigglyBracket;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "DNA Splicer")
            {
                string itemText = "catgirl time :)";
                iText.text = itemText;
                return DNASplicer;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Microphone")
            {
                string itemText = "AAAAAAAAAAAAA";
                iText.text = itemText;
                return Microphone;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Conch Shell")
            {
                string itemText = "Nothing.";
                iText.text = itemText;
                return ConchShell;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Spider In A Box")
            {
                string itemText = "never should've come here";
                iText.text = itemText;
                return SpiderInABox;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Bouncy Ball")
            {
                string itemText = "delicious";
                iText.text = itemText;
                return BouncyBall;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Telescope")
            {
                string itemText = "The Fucking Crusades";
                iText.text = itemText;
                return Telescope;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Sprout")
            {
                string itemText = "Sprout <3";
                iText.text = itemText;
                return Sprout;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Sol Sapphire")
            {
                string itemText = "where's that damn...";
                iText.text = itemText;
                return SolSapphire;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Mug")
            {
                string itemText = "ulon";
                iText.text = itemText;
                return Mug;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Bag Full Of Cold Hard Cash")
            {
                string itemText = "500k, specifically";
                iText.text = itemText;
                return BagFullOfColdHardCash;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Almanac")
            {
                string itemText = "We MUST fill it";
                iText.text = itemText;
                return Almanac;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Firmware Update")
            {
                string itemText = "v1.0.2.1.1 to v1.0.2.1.1.1";
                iText.text = itemText;
                return FirmwareUpdate;
            }
            if (itemDisplayer.allItems[randomNumberList[0]].name == "Corpse Paint")
            {
                string itemText = "Death of the Party(Endearing)";
                iText.text = itemText;
                return CorpsePaint;
            }
        }
        #endregion

        if (noMoreItems == true)
        {
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Teddy Bear")
            {
                string itemText = "Teddy Bear goes rawr hahaha rawr hahaha";
                iText.text = itemText;
                return TeddyBear;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Bag of Marbles")
            {
                string itemText = "*Marbels";
                iText.text = itemText;
                return BagOfMarbles;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Fishing Rod")
            {
                string itemText = "Okay but does this game have fishing?";
                iText.text = itemText;
                return FishingRod;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Star Fragment")
            {
                string itemText = "can be sold for a high price";
                iText.text = itemText;
                return StarFragment;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Pharaoh's Cowl")
            {
                string itemText = "tut.caw";
                iText.text = itemText;
                return PharaohCowl;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "T-Bone Steak")
            {
                string itemText = "yummers";
                iText.text = itemText;
                return TBoneSteak;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Squiggly Bracket")
            {
                string itemText = "you forgot the ;";
                iText.text = itemText;
                return SquigglyBracket;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "DNA Splicer")
            {
                string itemText = "catgirl time :)";
                iText.text = itemText;
                return DNASplicer;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Microphone")
            {
                string itemText = "AAAAAAAAAAAAA";
                iText.text = itemText;
                return Microphone;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Conch Shell")
            {
                string itemText = "Nothing.";
                iText.text = itemText;
                return ConchShell;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Spider In A Box")
            {
                string itemText = "never should've come here";
                iText.text = itemText;
                return SpiderInABox;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Bouncy Ball")
            {
                string itemText = "delicious";
                iText.text = itemText;
                return BouncyBall;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Telescope")
            {
                string itemText = "The Fucking Crusades";
                iText.text = itemText;
                return Telescope;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Sprout")
            {
                string itemText = "Sprout <3";
                iText.text = itemText;
                return Sprout;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Sol Sapphire")
            {
                string itemText = "where's that damn...";
                iText.text = itemText;
                return SolSapphire;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Mug")
            {
                string itemText = "ulon";
                iText.text = itemText;
                return Mug;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Bag Full Of Cold Hard Cash")
            {
                string itemText = "500k, specifically";
                iText.text = itemText;
                return BagFullOfColdHardCash;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Almanac")
            {
                string itemText = "We MUST fill it";
                iText.text = itemText;
                return Almanac;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Firmware Update")
            {
                string itemText = "v1.0.2.1.1 to v1.0.2.1.1.1";
                iText.text = itemText;
                return FirmwareUpdate;
            }
            if (itemDisplayer.heldItems[randomNumberList[0]].name == "Corpse Paint")
            {
                string itemText = "Death of the Party(Endearing)";
                iText.text = itemText;
                return CorpsePaint;
            }

        }

        return Categorizer();
    }


    #region Item Purchase Buttons
    private void TeddyBear()
    {
        if (itemDisplayer.squareScrap >= 3) //purchase cost
        {
            if (!itemDisplayer.heldItems.Find(obj => obj.name == "Teddy Bear"))
            {
                itemDisplayer.itemToAdd = "Teddy Bear";
                itemDisplayer.AddItemToUI();
            }
            itemDisplayer.squareScrap -= 3;
            itemDisplayer.TeddyBear();
            gameObject.SetActive(false);

            if(itemDisplayer.teddyLevel == 3)
            {
                itemList.Remove(Resources.Load("Sprites/Items/Game Items/Teddy Bear") as GameObject);
                itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Teddy Bear") as GameObject);
                itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Teddy Bear") as GameObject);
            }
        }
    }
    private void BagOfMarbles()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Bag of Marbles"))
        {
            itemDisplayer.itemToAdd = "Bag of Marbles";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.BagOfMarbles();
        gameObject.SetActive(false);

        if (itemDisplayer.bagOfMarblesLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Bag of Marbles") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Bag of Marbles") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Bag of Marbles") as GameObject);
        }

    }
    private void FishingRod()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Fishing Rod"))
        {
            itemDisplayer.itemToAdd = "Fishing Rod";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.FishingRod();
        gameObject.SetActive(false);

        if (itemDisplayer.fishingRodLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Fishing Rod") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Fishing Rod") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Fishing Rod") as GameObject);
        }
    }
    private void StarFragment()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Star Fragment"))
        {
            itemDisplayer.itemToAdd = "Star Fragment";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.StarFragment();
        gameObject.SetActive(false);

        if (itemDisplayer.starFragmentLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Star Fragment") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Star Fragment") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Star Fragment") as GameObject);
        }
    }
    private void PharaohCowl()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Pharaoh's Cowl"))
        {
            itemDisplayer.itemToAdd = "Pharaoh's Cowl";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.PharaohCowl();
        gameObject.SetActive(false);

        if (itemDisplayer.pharaohCowlLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Pharaoh's Cowl") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Pharaoh's Cowl") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Pharaoh's Cowl") as GameObject);
        }

    }
    private void TBoneSteak()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "T-Bone Steak"))
        {
            itemDisplayer.itemToAdd = "T-Bone Steak";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.TBoneSteak();
        gameObject.SetActive(false);

        if (itemDisplayer.tBoneSteakLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/T-Bone Steak") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/T-Bone Steak") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/T-Bone Steak") as GameObject);
        }

    }
    private void SquigglyBracket()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Squiggly Bracket"))
        {
            itemDisplayer.itemToAdd = "Squiggly Bracket";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.SquigglyBracket();
        gameObject.SetActive(false);

        if (itemDisplayer.squigglyBracketLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Squiggly Bracket") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Squiggly Bracket") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Squiggly Bracket") as GameObject);
        }

    }
    private void DNASplicer()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "DNA Splicer"))
        {
            itemDisplayer.itemToAdd = "DNA Splicer";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.DNASplicer();
        gameObject.SetActive(false);

        if (itemDisplayer.dnaSplicerLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/DNA Splicer") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/DNA Splicer") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/DNA Splicer") as GameObject);
        }
    }
    private void Microphone()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Microphone"))
        {
            itemDisplayer.itemToAdd = "Microphone";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.Microphone();
        gameObject.SetActive(false);

        if (itemDisplayer.microphoneLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Microphone") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Microphone") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Microphone") as GameObject);
        }

    }
    private void ConchShell()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Conch Shell"))
        {
            itemDisplayer.itemToAdd = "Conch Shell";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.ConchShell();
        gameObject.SetActive(false);

        if (itemDisplayer.conchShellLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Conch Shell") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Conch Shell") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Conch Shell") as GameObject);
        }

    }
    private void SpiderInABox()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Spider In A Box"))
        {
            itemDisplayer.itemToAdd = "Spider In A Box";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.SpiderInABox();
        gameObject.SetActive(false);

        if (itemDisplayer.spiderInABoxLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Spider In A Box") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Spider In A Box") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Spider In A Box") as GameObject);
        }
    }
    private void BouncyBall()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Bouncy Ball"))
        {
            itemDisplayer.itemToAdd = "Bouncy Ball";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.BouncyBall();
        gameObject.SetActive(false);

        if (itemDisplayer.bouncyBallLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Bouncy Ball") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Bouncy Ball") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Bouncy Ball") as GameObject);
        }
    }
    private void Telescope()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Telescope"))
        {
            itemDisplayer.itemToAdd = "Telescope";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.Telescope();
        gameObject.SetActive(false);

        if (itemDisplayer.telescopeLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Telescope") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Telescope") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Telescope") as GameObject);
        }
    }
    private void Sprout()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Sprout"))
        {
            itemDisplayer.itemToAdd = "Sprout";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.Sprout();
        gameObject.SetActive(false);

        if (itemDisplayer.sproutLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Sprout") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Sprout") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Sprout") as GameObject);
        }
    }
    private void SolSapphire()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Sol Sapphire"))
        {
            itemDisplayer.itemToAdd = "Sol Sapphire";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.SolSapphire();
        gameObject.SetActive(false);

        if (itemDisplayer.solSapphireLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Sol Sapphire") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Sol Sapphire") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Sol Sapphire") as GameObject);
        }
    }
    private void Mug()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Mug"))
        {
            itemDisplayer.itemToAdd = "Mug";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.Mug();
        gameObject.SetActive(false);

        if (itemDisplayer.mugLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Mug") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Mug") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Mug") as GameObject);
        }
    }
    private void BagFullOfColdHardCash()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Bag Full Of Cold Hard Cash"))
        {
            itemDisplayer.itemToAdd = "Bag Full Of Cold Hard Cash";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.BagFullOfColdHardCash();
        gameObject.SetActive(false);

        if (itemDisplayer.bagFullOfColdHardCashLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Bag Full Of Cold Hard Cash") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Bag Full Of Cold Hard Cash") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Bag Full Of Cold Hard Cash") as GameObject);
        }
    }
    private void Almanac()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Almanac"))
        {
            itemDisplayer.itemToAdd = "Almanac";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.Almanac();
        gameObject.SetActive(false);

        if (itemDisplayer.almanacLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Almanac") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Almanac") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Almanac") as GameObject);
        }
    }
    private void FirmwareUpdate()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Firmware Update"))
        {
            itemDisplayer.itemToAdd = "Firmware Update";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.FirmwareUpdate();
        gameObject.SetActive(false);

        if (itemDisplayer.firmwareUpdateLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Firmware Update") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Firmware Update") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Firmware Update") as GameObject);
        }
    }
    private void CorpsePaint()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Corpse Paint"))
        {
            itemDisplayer.itemToAdd = "Corpse Paint";
            itemDisplayer.AddItemToUI();
        }
        itemDisplayer.CorpsePaint();
        gameObject.SetActive(false);

        if (itemDisplayer.corpsePaintLevel == 3)
        {
            itemList.Remove(Resources.Load("Sprites/Items/Game Items/Corpse Paint") as GameObject);
            itemDisplayer.allItems.Remove(Resources.Load("Sprites/Items/Game Items/Corpse Paint") as GameObject);
            itemDisplayer.heldItems.Remove(Resources.Load("Sprites/Items/Game Items/Corpse Paint") as GameObject);
        }
    }
    #endregion

}
