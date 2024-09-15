using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] string mainOfSavePath;

    void Awake()
    {
        mainOfSavePath = Application.persistentDataPath;
    }

    private void Start()
    {
        //levelSkinSetDatabase.startInitAllSkinSets();
        //Save("/levelSkinSetDatabase.json", levelSkinSetDatabase._skinSetData);
    }
    public void Save(string path, Object obj)
    {
        path = mainOfSavePath + path;
        var json = JsonUtility.ToJson(obj);
        System.IO.File.WriteAllText(path, json);
    }

    public void Load(string path, object obj)
    {
        path = mainOfSavePath + path;
        // Does the file exist?
        if (File.Exists(path))
            JsonUtility.FromJsonOverwrite(path, obj);
    }
}
