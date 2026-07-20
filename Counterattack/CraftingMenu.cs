using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CraftingMenu : MonoBehaviour
{
    public static CraftingMenu instance;

    public Text redScrap;
    public int redCount;
    public Text blueScrap;
    public int blueCount;
    public Text greenScrap;
    public int greenCount;
    public Text basicScrap;
    public int basicCount;

    public GameObject craftingGrid;
    private List<GameObject> grids;
    private string gridColourCombination;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        grids = new List<GameObject>();

        foreach(Transform grid in craftingGrid.GetComponentsInChildren<Transform>())
        {
            if (grid != craftingGrid.transform)
            {
                grids.Add(grid.gameObject);
            }
        }
    }

    private void OnEnable()
    {
        redCount = ItemDisplay.instance.squareScrap;
        blueCount = ItemDisplay.instance.circleScrap;
        greenCount = ItemDisplay.instance.triangleScrap;
        basicCount = ItemDisplay.instance.basicScrap;

        redScrap.text = "Red Scrap: " + redCount;
        blueScrap.text = "Blue Scrap: " + blueCount;
        greenScrap.text = "Green Scrap: " + greenCount;
        basicScrap.text = "Basic Scrap: " + basicCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetBoard()
    {
        redCount = ItemDisplay.instance.squareScrap;
        blueCount = ItemDisplay.instance.circleScrap;
        greenCount = ItemDisplay.instance.triangleScrap;
        basicCount = ItemDisplay.instance.basicScrap;

        redScrap.text = "Red Scrap: " + redCount;
        blueScrap.text = "Blue Scrap: " + blueCount;
        greenScrap.text = "Green Scrap: " + greenCount;
        basicScrap.text = "Basic Scrap: " + basicCount;

        foreach (GameObject obj in grids)
        {
            obj.GetComponent<CraftMenuSlot>().ResetThisGrid();
        }
    }

    public void CreateItem()
    {
        PlayerData data = SaveSystem.LoadUpgrades();

        foreach(GameObject grid in grids)
        {
            gridColourCombination += grid.GetComponent<CraftMenuSlot>().scrapInThisSlot; //Writes down the state of the grid in a long string, to be checked below by one of the ifs
        }

        //Libra Item
        if (gridColourCombination == "RedNoneNoneNoneNoneNoneNoneNoneNone" && data.nameOfUnlockedBlueprints.Contains("Libra") && ItemDisplay.instance.teddyLevel < 3)
        {
            if (!ItemDisplay.instance.heldItems.Find(obj => obj.name == "Teddy Bear"))
            {
                ItemDisplay.instance.itemToAdd = "Teddy Bear";
                ItemDisplay.instance.AddItemToUI();
            }
            ItemDisplay.instance.squareScrap -= 1;
            ItemDisplay.instance.TeddyBear();
            ResetBoard();
        }



        gridColourCombination = null;
    }
}
