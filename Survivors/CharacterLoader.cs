using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    public void Start()
    {
        PlayerData data = SaveSystem.LoadUpgrades();
        GameObject player;
        GameObject grid;

        player = Instantiate(Resources.Load("Characters/" + data.selectedCharacterName) as GameObject, transform.position, transform.rotation);
        grid = Instantiate(Resources.Load("Characters/Character Extras/Friend Placement Grid") as GameObject, player.transform.position, player.transform.rotation);

        player.name = data.selectedCharacterName;
        grid.name = "Friend Placement Grid";
        grid.transform.parent = player.transform;

        FindObjectOfType<Camera>().transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -100);
        FindObjectOfType<Camera>().transform.parent = player.transform;

        player.AddComponent<PlayerMovement>();
        player.GetComponent<PlayerMovement>().canvas = FindObjectOfType<Canvas>();
        

        for (int i = 0; i < grid.transform.childCount; i++)
        {
            player.GetComponent<PlayerMovement>().spots[i] = GameObject.Find("Spot" + (i+1));
        }
    }
}
