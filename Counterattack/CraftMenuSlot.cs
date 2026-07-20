using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftMenuSlot : MonoBehaviour, IDropHandler
{
    public string scrapInThisSlot = "None";

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragThisItem dragItem = dropped.GetComponent<DragThisItem>();
            dragItem.parentAfterDrag = transform;

            if(dragItem.scrapName == "RedScrap")
            {
                scrapInThisSlot = "Red";
            }
            else if(dragItem.scrapName == "BlueScrap")
            {
                scrapInThisSlot = "Blue";
            }
            else if (dragItem.scrapName == "GreenScrap")
            {
                scrapInThisSlot = "Gteen";
            }
            else if (dragItem.scrapName == "BasicScrap")
            {
                scrapInThisSlot = "Basic";
            }
        }
    }

    public void ResetThisGrid()
    {
        if(transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++) //wont really matter because only one gameobject, but just in case
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        scrapInThisSlot = "None";
    }

    void Update()
    {
        if(transform.childCount == 0 && scrapInThisSlot != "None")
        {
            scrapInThisSlot = "None";
        }
    }

    void OnDisable()
    {
        ResetThisGrid();
    }

}
