using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OoBBoxScript : MonoBehaviour
{
    //public Canvas canvas;
    //private RectTransform rt;
    private BoxCollider2D bCollider;

    // Start is called before the first frame update
    void Start()
    {
        bCollider = gameObject.transform.GetComponent<BoxCollider2D>();
        //rt = canvas.GetComponent<RectTransform>();

        float aspect = Screen.width / Screen.height;
        float size = Camera.main.orthographicSize;

        float width = 2.0f * size * aspect;
        float height = 2.0f * size;

        //bCollider.size = new Vector2(width, height);
        //bCollider.size = new Vector2(Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Arm" && collision.GetComponent<ArmStats>().armType != "Sword") || collision.tag == "AllyProjectile")
        {
            if(GameObject.Find("Tantrum Arm"))
            {
                GameObject.Find("Tantrum Arm").GetComponent<TantrumArm>().tantrumStacks++;
            }
        }
    }

    
}
