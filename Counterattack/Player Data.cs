using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health = 0;
    public int blockRegen = 0;
    public int armour = 0; //maybe remove
    public int bulletCount = 0;
    public int bulletSpeed = 0;
    public int swordSwingSpeed = 0;
    public int moveSpeed = 0;
    public int size = 0; //remove
    public int moneyMultiplier = 0;
    //public int exp = 0; //remove
    public int maxNumberOfItems = 0;
    public int luck = 0;
    public int damage = 0;

    public string selectedCharacterName;

    public int moneySaved;

    public string[] nameOfUnlockedBlueprints = new string[0];

    public PlayerData (UpgradeLoader data)
    {
        health = data.health;
        blockRegen = data.blockRegen;
        armour = data.armour;
        bulletCount = data.bulletCount;
        bulletSpeed = data.bulletSpeed;
        swordSwingSpeed = data.swordSwingSpeed;
        moveSpeed = data.moveSpeed;
        size = data.size;
        moneyMultiplier = data.moneyMultiplier;
        //exp = data.exp;
        maxNumberOfItems = data.maxNumberOfItems;
        luck = data.luck;
        damage = data.damage;
        selectedCharacterName = data.characterName;

        moneySaved = data.moneySaved;

        List<string> _nameOfUnlockedBlueprints = new List<string>();
        foreach(string name in data.unlockableBlueprints.Keys)
        {
            if(data.unlockableBlueprints[name].isUnlocked)
            {
                _nameOfUnlockedBlueprints.Add(name);
            }
        }
        nameOfUnlockedBlueprints = _nameOfUnlockedBlueprints.ToArray();
        _nameOfUnlockedBlueprints.Clear();
    }

}
