using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DataScriptablObject", order = 1)]
public class DataScriptableObject : ScriptableObject {
    public float soundFactor;
    public float musicFactor;
    public int maxPoint;

    public List<string> itemIds;
}