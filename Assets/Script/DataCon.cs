using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class DataCon : MonoBehaviour
{
    //싱글톤===================
    static GameObject _container;
    static GameObject Container{
        get
        {
            return _container;
        }
    }
    static DataCon _instance;
    static DataCon Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject(); 
                _container.name = "DataCon";
                _instance = _container.AddComponent(typeof(DataCon)) as DataCon;
;           }
            return _instance;
        }
    }

    //==================
    //작업중! 지우지 마시오
    
    public string GameDataFileName = ".json";
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if(_gameData==null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        if (File.Exists(filePath))
        {
            Debug.Log("load success!");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            Debug.Log("create new file");
            _gameData = new GameData();
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("Saved!");
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
