using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TarotMessages : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Despawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Despawn()
    {
        yield return new WaitForSeconds(1.5f);
        {
            Destroy(gameObject);
        }
    }
}
