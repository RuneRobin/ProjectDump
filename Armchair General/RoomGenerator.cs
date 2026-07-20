using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    //public List<GameObject> roomList;
    //public List<GameObject> openExits;
    //public GameObject currExit;

    public GameObject[] walls; //0 - up, 1 - down, 2 - right, 3 - left
    public GameObject[] doors;

    public bool[] testStatus;

    void Start()
    {
        //roomList = new List<GameObject>(Resources.LoadAll<GameObject>("Rooms"));
        //openExits = new List<GameObject>(openExits);
        GenerateRoom(testStatus);
    }

    void GenerateRoom(bool[] status)
    {
        for(int i = 0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
        
        
        
        
        /*GameObject roomToBuild = roomList[Random.Range(0, roomList.Count -1)];//gets random room from list
        GameObject newRoom = Instantiate(roomToBuild, transform.position + currExit.transform.position, Quaternion.identity); //instantiates it in a certain area
        foreach(GameObject exits in newRoom.GetComponentsInChildren<GameObject>()) //for each exit empty object in this room, add them to exit List
        {
            if(exits.name.Contains("Exit"))
            {
                openExits.Add(exits);
            }
        }
        if(roomList.Count > 0 && openExits.Count > 0)
        {
            currExit = openExits[Random.Range(0, openExits.Count - 1)];
        }*/
    }
}
