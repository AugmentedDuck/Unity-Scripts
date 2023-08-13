using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirectoryPath = ""; //Should be empty
    private string dataFileName = ""; //Should be empty

    private bool useEncryption = false;
    private readonly string encryptionPassphrase = " YOUR ENCRYPTION KEY "; //The key for your XOR encryption, should be fairly long for "best" security - key saved locally so not truely safe

    public FileDataHandler(string dataDirectoryPath, string dataFileName, bool useEncryption)
    {
        this.dataDirectoryPath = dataDirectoryPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load(string profileID)
    {
        //Combines file path and name, works for all OS's
        string fullPath = Path.Combine(dataDirectoryPath, profileID, dataFileName);
        GameData loadedData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                //Load data from file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                //Deserialize the data from JSON
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data at: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data, string profileID)
    {
        //Combines file path and name, works for all OS's
        string fullPath = Path.Combine(dataDirectoryPath, profileID, dataFileName);

        try
        {
            //Create the directory if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serialize to JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            if(useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            //Write to file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data at: " + fullPath + "\n" + e);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();
        
        IEnumerable<DirectoryInfo> directoryInfos = new DirectoryInfo(dataDirectoryPath).EnumerateDirectories();

        foreach (DirectoryInfo directoryInfo in directoryInfos)
        {
            string profileID = directoryInfo.Name;

            //If data file doesn't exist - skip it
            string fullPath = Path.Combine(dataDirectoryPath, profileID, dataFileName);
            if (!File.Exists(fullPath)){
                Debug.LogWarning("Skipping directory when loading profiles - does not contain data at: " + profileID);
                continue;
            }

            GameData profileData = Load(profileID);

            //This file should contain data
            if(profileData != null)
            {
                profileDictionary.Add(profileID, profileData);
            }
            else
            {
                Debug.LogError("Tried loading profileID " + profileID + ", but something went wrong");
            }
        }

        return profileDictionary;
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionPassphrase[i % encryptionPassphrase.Length]); //The XOR algorithm
        }
        return modifiedData;
    }
}
