using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragThisItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    [HideInInspector] public Transform parentAfterDrag;

    public string scrapName; //set in inspector to avoid using name
    
    void Start()
    {
        originalParent = transform.parent;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if ((scrapName == "RedScrap" && CraftingMenu.instance.redCount > 0) || (scrapName == "BlueScrap" && CraftingMenu.instance.blueCount > 0) || (scrapName == "GreenScrap" && CraftingMenu.instance.greenCount > 0) || (scrapName == "BasicScrap" && CraftingMenu.instance.basicCount > 0) || originalParent == null)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root); //In this case, the canvas
            transform.SetAsLastSibling(); // so it shows up above everything
            GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if ((scrapName == "RedScrap" && CraftingMenu.instance.redCount > 0) || (scrapName == "BlueScrap" && CraftingMenu.instance.blueCount > 0) || (scrapName == "GreenScrap" && CraftingMenu.instance.greenCount > 0) || (scrapName == "BasicScrap" && CraftingMenu.instance.basicCount > 0) || originalParent == null)
        {
            transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 1);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if ((scrapName == "RedScrap" && CraftingMenu.instance.redCount > 0) || (scrapName == "BlueScrap" && CraftingMenu.instance.blueCount > 0) || (scrapName == "GreenScrap" && CraftingMenu.instance.greenCount > 0) || (scrapName == "BasicScrap" && CraftingMenu.instance.basicCount > 0) || originalParent == null)
        {
            transform.SetParent(parentAfterDrag);
            GetComponent<Image>().raycastTarget = true;
            transform.position = transform.parent.position;

            if (parentAfterDrag != originalParent && originalParent != null)
            {
                originalParent.GetComponent<ScrapPile>().scrapHasMoved = true; //Instantiates another scrap on the scrap pile, doing this in order to unparent the scrap itself and allow it to be above all UI
                originalParent = null;
            }
        }
    }
}
