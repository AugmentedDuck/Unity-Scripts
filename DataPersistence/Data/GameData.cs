using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Make public fields with the data types you want to save
    //eg public int health;

    //the values in here, will be the data made with "New Game"
    public GameData()
    {
        //eg health = 100;
    }

    public int GetPercentageComplete()
    {
        //Does nothing for now
        return 0;
    }
}
