using UnityEngine;

public class RobotSoldier : MonoBehaviour
{
    private UnitScript us;

    public int bonusBullets = 1;
    public float upgradeTimer = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        us = gameObject.GetComponent<UnitScript>();
        us.faction = "Robot";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        upgradeTimer -= Time.deltaTime;
        if(upgradeTimer <= 0)
        {
            bonusBullets++;
            upgradeTimer = 10f;
            if (gameObject.GetComponent<ArcherScript>())
            {
                gameObject.GetComponent<ArcherScript>().bulletCount = bonusBullets;
            }
        }
    }
}
