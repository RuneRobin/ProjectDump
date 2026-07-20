using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPile : MonoBehaviour
{
    private string scrapType;
    CraftingMenu cMenu;
    public GameObject scrapPrefab;
    public GameObject scrap;

    public bool scrapHasMoved = false;

    private void Awake()
    {
        cMenu = transform.parent.GetComponent<CraftingMenu>();
    }

    // Start is called before the first frame update
    void Start()
    {
        scrapType = name;
    }

    // Update is called once per frame
    void Update()
    {
        if(scrapHasMoved == true)
        {
            scrap = Instantiate(scrapPrefab, Vector3.zero, transform.rotation);
            scrap.transform.SetParent(transform,false);
            scrap.name = scrapPrefab.name;
            if (scrapType == "RedScrapPile")
            {
                cMenu.redCount--;
                cMenu.redScrap.text = "Red Scrap: " + cMenu.redCount;
            }
            else if(scrapType == "BlueScrapPile")
            {
                cMenu.blueCount--;
                cMenu.blueScrap.text = "Blue Scrap: " + cMenu.blueCount;
            }
            else if(scrapType == "GreenScrapPile")
            {
                cMenu.greenCount--;
                cMenu.greenScrap.text = "Green Scrap: " + cMenu.greenCount;
            }
            else if(scrapType == "BasicScrapPile")
            {
                cMenu.basicCount--;
                cMenu.basicScrap.text = "Basic Scrap: " + cMenu.basicCount;
            }
            scrapHasMoved = false;
        }
    }
}
