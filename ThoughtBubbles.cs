using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtBubbles : MonoBehaviour
{
    private RoomController rc;
    public List<Sprite> foods = new List<Sprite>();
    private bool hungry = false;
    public GameObject thought;

    // Start is called before the first frame update
    void Start()
    {
        rc = RoomController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(rc.guy8ShoppingList.Count > 0 && foods.Count == 0)//when the shopping list has items to pick, if the foods list is empty, fill it with the shopping list sprites
        {
            foreach(int i in rc.guy8ShoppingList)
            {
                foods.Add(rc.guy8FoodButtons[i]);
                hungry = true;
            }
        }

        if(rc.guy8ShoppingList.Count == 0 && foods.Count != 0)//if the shopping list is empty and the foods list isnt, clear the foods list for next time
        {
            foods.Clear();
        }
    }

    public void ThoughtThrower()
    {
        if (rc.guy8ShoppingList.Count > 0)
        {
            for (int i = 0; i < Random.Range(2, 6); i++)
            {
                if (hungry == true)
                {
                    GameObject t;

                    t = Instantiate(thought, transform.parent.transform.position, transform.parent.transform.rotation);
                    t.GetComponent<Image>().sprite = foods[Random.Range(0, foods.Count)];
                    t.GetComponent<Rigidbody2D>().linearVelocity = Random.onUnitSphere * 2;
                    t.transform.SetParent(transform.parent, false);
                }
            }
        }
    }
}
