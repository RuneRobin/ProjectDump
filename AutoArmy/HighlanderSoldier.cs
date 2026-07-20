using UnityEngine;

public class HighlanderSoldier : MonoBehaviour
{
    private UnitScript us;

    public float statBoost = 1; //on friendly death, increase by 0.1

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        us = gameObject.GetComponent<UnitScript>();
        us.faction = "Highlander";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
