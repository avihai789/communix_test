using Firebase.Extensions;
using Firebase.RemoteConfig;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class AllDifficulties
{
    public Difficulty Easy;
    public Difficulty Medium;
    public Difficulty Hard;
}

public class RemoteConfig : MonoBehaviour
{
    
    private void Awake()
    {
        CheckRemoteConfigValues();
    }

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
                Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");

                print("Total values: "+remoteConfig.AllValues.Count);
                
                string configData = remoteConfig.GetValue("Game_configs").StringValue;
                try
                {

                    var allDifficulties = JsonConvert.DeserializeObject<AllDifficulties>(configData);
                    print(allDifficulties);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    throw;
                }

            });
    }

}