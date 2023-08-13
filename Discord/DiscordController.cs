using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
using System.Data;

public class DiscordController : MonoBehaviour
{
    public long applicationID; //Under discord developer make application and get its id
    [Space]
    public string playDetails = ""; //What should be displayed under details
    public string playState = ""; //What should be displayed under play state

    [Space]
    public string largeImage = ""; //This should be the id for a image uploaded on developer discord
    public string largeText = ""; //What should be in the large text - I use the games name

    private long time;

    private static bool instanceExists;
    public Discord.Discord discord;

    private void Awake()
    {
        //Does a manager already exist in scene, then destroy this, if not make sure this doesn't unload
        if (!instanceExists)
        {
            instanceExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);

        time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();

        UpdateStatus();
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure game is still connected to discord
        try
        {
            discord.RunCallbacks();
        }
        catch 
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        //Tries to update the Discord Rich Presence
        try
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
                {
                    Details = playDetails,
                    State = playState,
                    Assets =
                    {
                        LargeImage = largeImage,
                        LargeText = largeText
                    },
                    Timestamps =
                    {
                        Start = time,
                    }
                };

            activityManager.UpdateActivity(activity, (res) =>
                {
                    if (res != Discord.Result.Ok) Debug.LogWarning("Failed to connect to Discord!");
                });
        }
        catch
        {
            Destroy(gameObject);
        }
    }
}
