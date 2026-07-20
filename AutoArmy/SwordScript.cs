using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public UnitScript us;
    public InfantryScript infSc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (us.faction == "Undead")
        {
            gameObject.transform.Find("Blade").GetComponent<SpriteRenderer>().color = new Color(0.1226415f, 0.1226415f, 0.1226415f);
        }
        else if (us.faction == "Highlander")
        {
            gameObject.transform.Find("Blade").GetComponent<SpriteRenderer>().color = new Color(0.8490566f, 0.5914292f, 0.07609467f);
        }
        else if (us.faction == "Elemental")
        {
            gameObject.transform.Find("Blade").GetComponent<SpriteRenderer>().color = new Color(0.7743282f, 0.7082592f, 0.8679245f);
        }
        else if (us.faction == "Circus")
        {
            gameObject.transform.Find("Blade").GetComponent<SpriteRenderer>().color = new Color(0.3131625f, 0.2783019f, 1f);
        }
        else if (us.faction == "Robot")
        {
            gameObject.transform.Find("Blade").GetComponent<SpriteRenderer>().color = new Color(0.4056604f, 0.4056604f, 0.4056604f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (us.faction == "Elemental")
        {
            if (us.gameObject.GetComponent<InfantryScript>().isAttacking == false && gameObject.transform.Find("Blade").gameObject.GetComponent<SpriteRenderer>().color != new Color(0.7743282f, 0.7082592f, 0.8679245f))
            {
                gameObject.transform.Find("Blade").gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7743282f, 0.7082592f, 0.8679245f);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<UnitScript>() && collision.gameObject.GetComponent<UnitScript>().army != us.army && us.gameObject.GetComponent<InfantryScript>().isAttacking == true)
        {
            //keep this part for aesthetics, calculate damage and effects on OnHitting
            if (us.faction == "Undead")
            {
                //nothing as of yet
            }
            else if (us.faction == "Highlander")
            {
                //nothing as of yet
            }
            else if (us.faction == "Elemental")
            {
                int r = Random.Range(0, 4);

                if(r == 0)//fire
                {
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                }
                else if(r == 1)//water
                {
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
                }
                else if (r == 2)//earth
                {
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.2924528f, 0.106513f, 0.04000534f);
                }
                else if (r == 3)//air
                {
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
                }

            }
            else if (us.faction == "Circus")
            {
                //nothing as of yet
            }
            else if (us.faction == "Robot")
            {
                //nothing as of yet
            }

            us.OnHitting(collision.gameObject.GetComponent<UnitScript>());
        }
    }
}
