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

    public int firstItemLevel = 0;
    public int secondItemLevel = 0;
    public int thirdItemLevel = 0;
    public int fourthItemLevel = 0;
    public int fifthItemLevel = 0;

    // Start is called before the first frame update
    void Awake()
    {
        //allItems = Resources.LoadAll<GameObject>("Sprites/Items/Game Items");
        itemDisplayer = GameObject.Find("Item Display").GetComponent<ItemDisplay>();
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
            Debug.Log("created new list");
        }
        if(noMoreItems == false && itemDisplayer.nextItem == itemDisplayer.maxNumberOfItems)
        {
            itemList = itemDisplayer.heldItems;
            noMoreItems = true;
            Debug.Log("no more items");
            
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


    private void TeddyBear()
    {
        if(!itemDisplayer.heldItems.Find(obj => obj.name == "Teddy Bear"))
        {
            itemDisplayer.itemToAdd = "Teddy Bear";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().TeddyBear();
        gameObject.SetActive(false);
    }
    private void BagOfMarbles()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Bag of Marbles"))
        {
            itemDisplayer.itemToAdd = "Bag of Marbles";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().BagOfMarbles();
        gameObject.SetActive(false);
        
    }
    private void FishingRod()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Fishing Rod"))
        {
            itemDisplayer.itemToAdd = "Fishing Rod";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().FishingRod();
        gameObject.SetActive(false);
    }
    private void StarFragment()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Star Fragment"))
        {
            itemDisplayer.itemToAdd = "Star Fragment";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().StarFragment();
        gameObject.SetActive(false);
    }
    private void PharaohCowl()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Pharaoh's Cowl"))
        {
            itemDisplayer.itemToAdd = "Pharaoh's Cowl";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().PharaohCowl();
        gameObject.SetActive(false);
    }
    private void TBoneSteak()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "T-Bone Steak"))
        {
            itemDisplayer.itemToAdd = "T-Bone Steak";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().TBoneSteak();
        gameObject.SetActive(false);
    }
    private void SquigglyBracket()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Squiggly Bracket"))
        {
            itemDisplayer.itemToAdd = "Squiggly Bracket";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().SquigglyBracket();
        gameObject.SetActive(false);
    }
    private void DNASplicer()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "DNA Splicer"))
        {
            itemDisplayer.itemToAdd = "DNA Splicer";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().DNASplicer();
        gameObject.SetActive(false);
    }
    private void Microphone()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Microphone"))
        {
            itemDisplayer.itemToAdd = "Microphone";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().Microphone();
        gameObject.SetActive(false);
    }
    private void ConchShell()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Conch Shell"))
        {
            itemDisplayer.itemToAdd = "Conch Shell";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().ConchShell();
        gameObject.SetActive(false);
    }
    private void SpiderInABox()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Spider In A Box"))
        {
            itemDisplayer.itemToAdd = "Spider In A Box";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().SpiderInABox();
        gameObject.SetActive(false);
    }
    private void BouncyBall()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Bouncy Ball"))
        {
            itemDisplayer.itemToAdd = "Bouncy Ball";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().BouncyBall();
        gameObject.SetActive(false);
    }
    private void Telescope()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Telescope"))
        {
            itemDisplayer.itemToAdd = "Telescope";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().Telescope();
        gameObject.SetActive(false);
    }
    private void Sprout()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Sprout"))
        {
            itemDisplayer.itemToAdd = "Sprout";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().Sprout();
        gameObject.SetActive(false);
    }
    private void SolSapphire()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Sol Sapphire"))
        {
            itemDisplayer.itemToAdd = "Sol Sapphire";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().SolSapphire();
        gameObject.SetActive(false);
    }
    private void Mug()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Mug"))
        {
            itemDisplayer.itemToAdd = "Mug";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().Mug();
        gameObject.SetActive(false);
    }
    private void BagFullOfColdHardCash()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Bag Full Of Cold Hard Cash"))
        {
            itemDisplayer.itemToAdd = "Bag Full Of Cold Hard Cash";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().BagFullOfColdHardCash();
        gameObject.SetActive(false);
    }
    private void Almanac()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Almanac"))
        {
            itemDisplayer.itemToAdd = "Almanac";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().Almanac();
        gameObject.SetActive(false);
    }
    private void FirmwareUpdate()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Firmware Update"))
        {
            itemDisplayer.itemToAdd = "Firmware Update";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().FirmwareUpdate();
        gameObject.SetActive(false);
    }
    private void CorpsePaint()
    {
        if (!itemDisplayer.heldItems.Find(obj => obj.name == "Corpse Paint"))
        {
            itemDisplayer.itemToAdd = "Corpse Paint";
            itemDisplayer.AddItemToUI();
        }
        GameObject.Find("Item Display").transform.gameObject.GetComponent<ItemDisplay>().CorpsePaint();
        gameObject.SetActive(false);
    }

}
