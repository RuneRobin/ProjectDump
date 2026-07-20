using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiliHealAoE : MonoBehaviour
{
    public List<GameObject> alreadyHealed;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HealCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision) //this will heal the allies once then add them to a list of healed
    {
        AllyScript allyScript = collision.gameObject.GetComponent<AllyScript>(); //saves time, makes lines below smaller.
        if (collision.gameObject.tag == "Ally" && !alreadyHealed.Contains(collision.gameObject) && allyScript.health < allyScript.maxHealth)
        {
            Debug.Log("healed ally");
            allyScript.health += gameObject.GetComponentInParent<AllyScript>().regen;
            alreadyHealed.Add(collision.gameObject);
        }
    }

    public IEnumerator HealCooldown() //this will empty the list of healed at the intervals of lili's cooldown so they can be healed again
    {
        alreadyHealed = new List<GameObject>();
        yield return new WaitForSeconds(gameObject.GetComponentInParent<AllyScript>().cooldown);
        StartCoroutine(HealCooldown());
    }
}
