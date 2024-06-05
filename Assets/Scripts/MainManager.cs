using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Color loaderColor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.Color = loaderColor;   

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json",json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            loaderColor = data.Color;
        }
    }
}


[Serializable]
public class SaveData
{
    public Color Color;
}