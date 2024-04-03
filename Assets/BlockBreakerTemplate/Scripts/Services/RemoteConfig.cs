using Firebase.Extensions;
using Firebase.RemoteConfig;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

public class RemoteConfig
{
    public event Action<Dictionary<string, Difficulty>> OnRemoteConfigValuesFetched;

    public Task CheckRemoteConfigValues()
    {
        Debug.Log("Fetching data...");
        Task fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }
    private void FetchComplete(Task fetchTask)
    {
        if (!fetchTask.IsCompleted)
        {
            Debug.LogError("Retrieval hasn't finished.");
            return;
        }

        var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        var info = remoteConfig.Info;
        if (info.LastFetchStatus != LastFetchStatus.Success)
        {
            Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
            return;
        }

        remoteConfig.ActivateAsync()
          .ContinueWithOnMainThread(
            task => {
                
                string configData = remoteConfig.GetValue("Game_configs").StringValue;
                try
                {
                    Dictionary<string, Difficulty> allDifficulties = 
                        JsonConvert.DeserializeObject<Dictionary<string, Difficulty>>(configData);
                    
                    OnRemoteConfigValuesFetched?.Invoke(allDifficulties);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    throw;
                }

            });
    }

}