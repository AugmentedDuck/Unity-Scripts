using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use this interface in all the scripts that have data to be saved
public interface IDataPersistance
{
    void LoadData(GameData data);

    void SaveData(GameData data);
}
