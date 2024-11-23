/*
   SDK - LootLockerSDK
   Link - https://github.com/LootLocker/unity-sdk
*/


using System;
using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.Networking;

public class LeaderboardSubmitSystem : MonoBehaviour
{
    string _leaderboardKey = "record_path_lb";
    public void InitializeSubmitRecord(int recordValue, Action<bool> callback)
    {
        StartCoroutine(CheckInternetConnection(isConnected =>
        {
            if (isConnected)
            {
                Debug.Log("Internet Available!");
                LootLockerSDKManager.CheckWhiteLabelSession(response =>
                {
                    if (response)
                    {
                        Debug.Log("session is valid, you can start a game session");
                        StartSession((bool callback_local) =>
                        {
                            if (callback_local)
                            {
                                SubmitRecordToLeaderboard(recordValue, (bool callback_local2) =>
                                {
                                    callback(callback_local2);
                                });
                            }
                            else callback(callback_local);
                        });
                    }
                    else
                    {
                        Debug.Log("session is NOT valid, we should show the login form");
                        callback(false);
                    }
                });
            }
            else
            {
                callback(false);
            }
        }));
    }

    void StartSession(Action<bool> callback)
    {
        LootLockerSDKManager.StartWhiteLabelSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");
                callback(false);
                return;
            }

            Debug.Log("session started successfully");
            callback(true);
        });
    }

    void SubmitRecordToLeaderboard(int recordValue, Action<bool> callback)
    {
        LootLockerSDKManager.SubmitScore("", recordValue, _leaderboardKey, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("Could not submit score!");
                Debug.Log(response.errorData.ToString());
                callback(false);
                return;
            }
            Debug.Log("Successfully submitted score!");
            callback(true);

        });
    }

    IEnumerator CheckInternetConnection(Action<bool> action)
    {
        UnityWebRequest request = new UnityWebRequest("https://google.com");
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.Log("Error");
            action(false);
        }
        else
        {
            Debug.Log("Success");
            action(true);
        }
    }
}
