using UnityEngine;

public class ElementalSoldier : MonoBehaviour
{
    private UnitScript us;

    public int powerCharge = 1; //Upgrades when the unit kills another unit
    public float powerChargeTimer = 5f;

    public float fireBoost = 1;
    public float waterBoost = 1;
    public float earthBoost = 1;
    public float airBoost = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        us = gameObject.GetComponent<UnitScript>();
        us.faction = "Elemental";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        powerChargeTimer -= Time.deltaTime;
        if(powerChargeTimer <= 0)
        {
            powerChargeTimer = 5f;

            for(int i = 0; i < powerCharge;i++)
            {
                int r = Random.Range(0, 4);

                if (r == 0)
                {
                    fireBoost += 0.5f;
                }
                else if (r == 1)
                {
                    waterBoost += 0.5f;
                }
                else if (r == 2)
                {
                    earthBoost += 0.5f;
                }
                else if (r == 3)
                {
                    airBoost += 0.5f;
                }
            }
        }
    }
}
