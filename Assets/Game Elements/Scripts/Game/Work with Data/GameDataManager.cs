using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEngine.Rendering.DebugUI;

public class SaveData
{
    public int fish;
    public int food;
    public int recordPath;

    public SaveData(int recordPath)
    {
        this.recordPath = recordPath;
    }
}
public class GameDataManager : MonoBehaviour
{
    public string mainOfSavePath;
    [SerializeField] ResultMenu resultMenu;
    void Awake()
    {
        mainOfSavePath = Application.persistentDataPath;
    }

    private void Start()
    {
        resultMenu.initRecordPath(this);
        //Save("/levelSkinSetDatabase.json", levelSkinSetDatabase._skinSetData);
    }

    public void LoadRecordPath(object obj)
    {
        Load("/data.json", obj);
    }

    public void SaveRecordPath(object obj)
    {
        Save("/data.json", obj);
    }
    public void Save(string path, Object obj)
    {
        path = mainOfSavePath + path;
        var json = JsonUtility.ToJson(obj);
        System.IO.File.WriteAllText(path, json);
    }

    public void Save(string path, object obj)
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
        {

            var json = System.IO.File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, obj);
        }
    }
}
