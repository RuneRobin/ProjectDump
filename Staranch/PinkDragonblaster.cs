using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkDragonblaster : MonoBehaviour
{
    public GameObject bubblePrefab;
    private GameObject bubble;

    public float cooldown = 1;
    public float bubbleDuration = 3;

    // Start is called before the first frame update
    void Start()
    {
        bubblePrefab = Resources.Load("Bubble") as GameObject;
        StartCoroutine(Bubble());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bubble != null)
        {
            bubble.transform.localScale = new Vector2(bubble.transform.localScale.x + Time.deltaTime, bubble.transform.localScale.y + Time.deltaTime);
        }
    }

    public IEnumerator Bubble()
    {
        yield return new WaitForSeconds(cooldown);

        bubble = Instantiate(bubblePrefab, transform.position, transform.rotation);
        bubble.layer = gameObject.layer;
        //bubble.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color - new Color(0,0,0,0.5f);

        yield return new WaitForSeconds(bubbleDuration);

        Destroy(bubble);

        StartCoroutine(Bubble());
    }
}
