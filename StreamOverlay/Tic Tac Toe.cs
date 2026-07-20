using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class TicTacToe : MonoBehaviour
{
    public static TicTacToe instance;
    public List<GameObject> theGrid = new List<GameObject>();
    public List<Sprite> symbols = new List<Sprite>();
    public string User1;
    public string User2;
    private bool whosTurn = false;

    public int[,] grid =
    {
        { 1, 2, 3 },
        { 4, 5, 6 },
        { 7, 8, 9 }
    }; //look into switch statement to replace this

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WriteSymbol(string move, string user)
    {
        if(User1 == string.Empty)
        {
            User1 = user;
            Debug.Log("user1 established");
        }
        else if(User2 == string.Empty)
        {
            User2 = user;
            Debug.Log("user2 established");
        }

        if(whosTurn == false && user == User1 && theGrid[int.Parse(Regex.Replace(move, "[^0-9]", "")) - 1].GetComponent<Image>().sprite == null)
        {
            theGrid[int.Parse(Regex.Replace(move,"[^0-9]",""))-1].GetComponent<Image>().sprite = symbols[0];
            whosTurn = !whosTurn; //switches between both players turn each time one makes a move during their turn
        }
        else if (whosTurn == true && user == User2 && theGrid[int.Parse(Regex.Replace(move, "[^0-9]", "")) - 1].GetComponent<Image>().sprite == null)
        {
            theGrid[int.Parse(Regex.Replace(move, "[^0-9]", "")) - 1].GetComponent<Image>().sprite = symbols[1];
            whosTurn = !whosTurn; //switches between both players turn each time one makes a move during their turn
        }
    }

}
