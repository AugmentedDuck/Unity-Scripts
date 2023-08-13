using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] bool initializeDataIfNull = false; //Should a new savefile be made if a non-existing save is loaded

    [Header("File Storage Config")] //DO NOT CHANGE
    [SerializeField] private string fileName;
    [SerializeField] bool useEncryption; //Should the file be xor encrypted !! NOT TRUE ENCRYPTION !! DO NOT STORE "SECRETS" !!

    private GameData gameData;
    private List<IDataPersistance> dataPersistanceObjects;
    private FileDataHandler dataHandler;

    private string selectedProfileID = "test"; //What should the test/debug profile be called

    public static DataPersistanceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null) //Makes sure only one manager is present in any scene
        {
            Debug.Log("Found more than one Data Persistance Manager is scene. Deleting newest one");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject); //Save this object even on scene changes

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }

    private void OnEnable()
    {
        //Subscribe to load an unload events
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        //Unsubscribe from load and unload events
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    //Load a save on the scene load
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistanceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    //Save the game on scene unload
    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    //Start a new save file
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    
    public void LoadGame()
    {
        //Load any saved data using the data handler
        this.gameData = dataHandler.Load(selectedProfileID);

        if(this.gameData == null && initializeDataIfNull)
        {
            NewGame(); //If enabled, start new save if non-existant
        }

        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. Data can't be loaded");
        }

        //push data to all other scripts
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        if(this.gameData == null)
        {
            Debug.LogWarning("No data was found. Data can't be saved");
            return;
        }

        //pass data to script
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(gameData);
        }

        //Save the data using the data handler - data handler missing
        dataHandler.Save(gameData, selectedProfileID);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    //Finds all the objects which has been flagged to store data needed to be saved
    private List<IDataPersistance> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }

    public bool HasGameData()
    {
        return this.gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
}
