using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileID = ""; //Name of this saveslot

    public void SetData(GameData data)
    {
        
    }

    public string GetProfileID() 
    { 
        return this.profileID; 
    }
}
