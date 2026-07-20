using UnityEngine;

public class UndeadSoldier : MonoBehaviour
{
    private UnitScript us;

    public bool secondLife = true;
    public float lifesteal = 0;
    public float lifestealUpgradeTimer = 1.5f;
    public bool debuffFirstAttacker = true;
    public float zombieChance = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        us = gameObject.GetComponent<UnitScript>();
        us.faction = "Undead";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        lifestealUpgradeTimer -= Time.deltaTime;
        if(lifestealUpgradeTimer <= 0)
        {
            lifesteal += 0.1f;
            lifestealUpgradeTimer = 1.5f;
        }
    }
}
