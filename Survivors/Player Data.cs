using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health = 0;
    public int regen = 0;
    public int armour = 0;
    public int count = 0;
    public int bSpeed = 0;
    public int mSpeed = 0;
    public int size = 0;
    public int money = 0;
    public int exp = 0;
    public int capacity = 0;
    public int luck = 0;
    public int damage = 0;
    public string selectedCharacterName;

    public int moneyOnHand;

    public string[] nameOfUnlockedCharacters = new string[0];

    public PlayerData (UpgradeLoader data)
    {
        health = data.health;
        regen = data.regen;
        armour = data.armour;
        count = data.count;
        bSpeed = data.bSpeed;
        mSpeed = data.mSpeed;
        size = data.size;
        money = data.money;
        exp = data.exp;
        capacity = data.capacity;
        luck = data.luck;
        damage = data.damage;
        selectedCharacterName = data.characterName;

        moneyOnHand = data.moneyOnHand;

        List<string> _nameOfUnlockedCharacters = new List<string>();
        foreach(string name in data.unlockableCharacters.Keys)
        {
            if(data.unlockableCharacters[name].isUnlocked)
            {
                _nameOfUnlockedCharacters.Add(name);
            }
        }
        nameOfUnlockedCharacters = _nameOfUnlockedCharacters.ToArray();
        _nameOfUnlockedCharacters.Clear();
    }

}
