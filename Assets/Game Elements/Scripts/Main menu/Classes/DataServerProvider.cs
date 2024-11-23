using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.Rendering.LookDev;
using System;
using UnityEngine.Networking;

public class DataServerProvider : IDataProvider
{

    private const string FileName = "data";
    private const string SaveFileExtension = ".json";

    private IPersistentData _persistentData;
    private string SavePath => Application.persistentDataPath;
    private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExtension}");

    private int playerFileID { 
        get { return _persistentData.saveData.SaveDataFileId; }
        set { _persistentData.saveData.SaveDataFileId = value; }
    }

    public MonoBehaviour forCoroutines { get; set; }

    public DataServerProvider(IPersistentData persistentData, MonoBehaviour forCoroutines)
    {
        _persistentData = persistentData;
        this.forCoroutines = forCoroutines;
    }
    public void Save(Action<string> callback)
    {
        Save();
        LootLockerSDKManager.UpdatePlayerFile(playerFileID, FullPath, response =>
        {
            if (response.success)
            {
                Debug.Log("Successfully updated player file, url: " + response.url);
                playerFileID = response.id;
                callback("Successfully saved");
            }
            else
            {
                Debug.Log("Error updating player file");
                LootLockerSDKManager.UploadPlayerFile(FullPath, $"{FileName}{SaveFileExtension}", response =>
                {
                    if (response.success)
                    {
                        Debug.Log("Successfully uploaded player file, url: " + response.url);
                        playerFileID = response.id;
                        Save();
                        callback("Successfully saved");
                    }
                    else
                    {
                        Debug.Log("Error uploading player file");
                        callback("Error!");
                    }
                });
            }
        });

    }

    public void TryLoad(Action<string> callback)
    {
        LootLockerSDKManager.GetAllPlayerFiles((response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved player files: " + response.items.Length);
                SetSaveData(response.items, (bool callback_local) =>
                {
                    callback("Successfully loaded");
                });
            }
            else
            {
                Debug.Log("Error retrieving player storage");

                TryLoadLocal((string callback_local) =>
                {
                    callback(callback_local);
                });
            }
        });
    }


    private void SetSaveData(LootLockerPlayerFile[] files, Action<bool> callback)
    {
        int n = files.Length;
        LootLockerPlayerFile file = Array.Find(files, (file => file.name == $"{FileName}{SaveFileExtension}"));
        if (forCoroutines == null)
            throw new NullReferenceException(forCoroutines.name);
        forCoroutines.StartCoroutine(Download(file.url, (fileContent) =>
        {
            Debug.Log("File is downloaded");
            _persistentData.saveData = JsonConvert.DeserializeObject<SaveData>(fileContent);
            playerFileID = file.id;
            Save();
            callback(true);
        }));

    }
    IEnumerator Download(string url, Action<string> fileContent)
    {
        UnityWebRequest www = new UnityWebRequest(url);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            fileContent(www.downloadHandler.text);
        }
    }
    public void Save()
    {
        File.WriteAllText(FullPath, JsonConvert.SerializeObject(_persistentData.saveData, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));
    }

    public void TryLoadLocal(Action<string> callback)
    {
        if (IsDataAlreadyExist() == false)
        {
            _persistentData.saveData = new SaveData();
            Save((string callback_local) =>
            {
                callback(callback_local);
            });
            return;
        }

        try
        {
            _persistentData.saveData = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(FullPath));
            Save((string callback_local) =>
            {
                callback(callback_local);
            });
        }
        catch
        {
            _persistentData.saveData = new SaveData();
            Save((string callback_local) =>
            {
                callback(callback_local);
            });
        }
    }
    public bool IsDataAlreadyExist() => File.Exists(FullPath);

}
