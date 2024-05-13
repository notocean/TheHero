using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DataScriptablObject", order = 1)]
public class DataScriptableObject : ScriptableObject {
    public float soundFactor;
    public float musicFactor;
    public int highestScore;
    public List<string> itemIds;

    private const string filename = "gameData.txt";

    public void SaveToFile() {
        string filePath = Path.Combine(Application.persistentDataPath, filename);
        
        if (!File.Exists(filePath)) {
            File.Create(filePath);
        }

        string json = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, json);
    }

    public void LoadFromFile() { 
        string filePath = Path.Combine(Application.persistentDataPath, filename);

        if (!File.Exists(filePath))
            return;

        string json = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(json, this);
    }
}