using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StallionCustard : MonoBehaviour
{
    private GameObject gloopPrefab;
    private GameObject gloop;

    public float cooldown = 5;
    public float interval = 0.3f;
    public int gloopAmount = 3;

    // Start is called before the first frame update
    void Start()
    {
        gloopPrefab = Resources.Load("Spills Bills") as GameObject;

        StartCoroutine(Glooping());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public IEnumerator Glooping()
    {
        yield return new WaitForSeconds(cooldown);

        for (int i = 0; i < gloopAmount; i++)
        {
            gloop = Instantiate(gloopPrefab, transform.position, transform.rotation);
            gloop.layer = gameObject.layer;
            gloop.GetComponent<SpillsBills>().origin = gameObject;
            yield return new WaitForSeconds(interval);
        }

        StartCoroutine(Glooping());
    }
}
