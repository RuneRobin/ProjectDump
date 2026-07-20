using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmStats : MonoBehaviour
{
    public int level;
    [HideInInspector] public bool levelling = false;
    public string armType;
    public int purchaseCost;

    public bool isAttacking = false;
    [HideInInspector] public Vector3 startingPos;
    [HideInInspector] public Quaternion startingRot;

    [HideInInspector] public float timeOffset; //floating offset


    [Header("Basic Stats")]
    public int damage;
    public int bonusDamage;
    public float speed;

    [Header("Fists")]
    public int bonusFistDamage;
    public float bonusFistSpeed;
    public float cooldownTimer;
    public float bonusCooldown;

    [Header("Swords")]
    public int bonusSwordDamage;
    //public float bonusSwordSpeed;
    public float critChance = 5f;
    public float bonusCritChance;
    public float critDamage = 1.5f;
    public float bonusCritDamage;

    [Header("Guns")]
    public int bonusGunDamage;
    public float bonusGunBulletSpeed;
    public int bulletCount;
    public int bonusBulletCount;
    public float firerate;
    public float bonusFirerate = 1;

    [Header("Durability")]
    public bool isDurable;
    public int durability;
    public int bonusDurability;

    [Header("Wind-Up")]
    public bool isWindUp;
    public float windUp;
    public float bonusWindUpReduction;
    public float windUpTimer;
    public Image timerFill;

    [Header("Follow-Up")]
    public bool isFollowUp;
    public bool followUp;

    [Header("Misc")]
    public bool autonomous;
    public bool drone; //??

    [Header("Bleed")]
    public bool canBleed;
    public int bleed;
    public int bonusBleed;

    [Header("Burn")]
    public bool canBurn;
    public int burn;
    public int bonusBurn;

    [Header("Freeze")]
    public bool canFreeze;
    public int freeze;
    public int bonusFreeze;

    [Header("Shock")]
    public bool canShock;
    public int shock;
    public int bonusShock;

    [Header("Virus")]
    public bool canVirus;
    public int virus;
    public int bonusVirus;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
