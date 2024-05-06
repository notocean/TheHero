using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private DataScriptableObject _dataScriptableObject;

    public float soundFactor { get; private set; }
    public float musicFactor { get; private set; }
    public int maxPoint { get; private set; }

    [SerializeField] private List<Item> items;
    public List<string> itemIds { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    public void LoadData() {
        soundFactor = _dataScriptableObject.soundFactor;
        musicFactor = _dataScriptableObject.musicFactor;
        maxPoint = _dataScriptableObject.maxPoint;

        itemIds = _dataScriptableObject.itemIds;
    }

    public void ChangeSoundFactor(float value) {
        soundFactor = value;
        _dataScriptableObject.soundFactor = soundFactor;
    }

    public void ChangeMusicFactor(float value) {  
        musicFactor = value;
        _dataScriptableObject.musicFactor = musicFactor;
    }

    public void SetSelectedItems() {
        itemIds = UIManager.Instance.GetItemIds();
        _dataScriptableObject.itemIds = itemIds;
    }

    public List<Item> GetItems() {
        return items;
    }

    public void ChangeScene(int i) {
        SceneManager.LoadScene(i);
    }

    public void Quit() {
        // other data is saved when it is changed
        SetSelectedItems();
        Application.Quit();
    }
}
