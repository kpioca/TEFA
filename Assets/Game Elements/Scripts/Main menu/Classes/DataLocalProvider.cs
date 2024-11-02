using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataLocalProvider : IDataProvider
{

    private const string FileName = "data";
    private const string SaveFileExtension = ".json";

    private IPersistentData _persistentData;

    private string SavePath => Application.persistentDataPath;
    private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExtension}");
    public DataLocalProvider(IPersistentData persistentData) => _persistentData = persistentData;
    public void Save()
    {
        File.WriteAllText(FullPath, JsonConvert.SerializeObject(_persistentData.saveData, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));
    }

    public bool TryLoad()
    {
        if (IsDataAlreadyExist() == false)
            return false;

        try
        {
            _persistentData.saveData = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(FullPath));
        }
        catch
        {
            return false;
        }
        
        return true;
    }

    public bool IsDataAlreadyExist() => File.Exists(FullPath);
}
