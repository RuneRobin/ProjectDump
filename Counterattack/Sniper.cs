using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public float cooldown = 3f;
    public int damage = 10;

    private GameObject parryPlayer;

    private GameObject three;
    private GameObject two;
    private GameObject one;

    // Start is called before the first frame update
    void Start()
    {
        parryPlayer = GameObject.Find("ParryPlayer");
        three = gameObject.transform.Find("Three").gameObject;
        two = gameObject.transform.Find("Two").gameObject;
        one = gameObject.transform.Find("One").gameObject;

        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Attack()
    {
        float randomTiming = Random.Range(0.2f, 1f);

        yield return new WaitForSeconds(randomTiming);
        three.gameObject.SetActive(true);

        yield return new WaitForSeconds(randomTiming);
        two.gameObject.SetActive(true);

        yield return new WaitForSeconds(randomTiming);
        one.gameObject.SetActive(true);

        yield return new WaitForSeconds(randomTiming);
        if(Input.GetMouseButton(1))
        {
            GetComponent<EnemyStats>().health -= 10;
        }
        else
        {
            parryPlayer.GetComponent<ParryHitbox>().TakeDamage(damage);
        }

        three.gameObject.SetActive(false);
        two.gameObject.SetActive(false);
        one.gameObject.SetActive(false);

        yield return new WaitForSeconds(cooldown);

        StartCoroutine(Attack());
    }

}
