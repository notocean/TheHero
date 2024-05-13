using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private DataScriptableObject _dataScriptableObject;
    private AudioSource audioSource;

    public float soundFactor { get; private set; }
    public float musicFactor { get; private set; }
    public int highestScore { get; private set; }
    public int currentScore { get; private set; }

    [SerializeField] private List<Item> items;
    public List<string> itemIds { get; private set; }

    public bool IsPlaying { get; set; }

    private void Awake() {
        if (Instance == null) {
            Instance = FindObjectOfType<GameManager>();
            if (Instance == null) {
                GameObject obj = new GameObject("GameManager");
                Instance = obj.AddComponent<GameManager>();
            }
        }
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        currentScore = 0;

        audioSource = GetComponent<AudioSource>();
        audioSource?.Play();
        LoadData();
        IsPlaying = false;
    }

    public void LoadData() {
        _dataScriptableObject.LoadFromFile();
        soundFactor = _dataScriptableObject.soundFactor;
        musicFactor = _dataScriptableObject.musicFactor;
        highestScore = _dataScriptableObject.highestScore;

        itemIds = _dataScriptableObject.itemIds;
        audioSource.volume = musicFactor;
    }

    public void ChangeSoundFactor(float value) {
        soundFactor = value;
        _dataScriptableObject.soundFactor = soundFactor;
    }

    public void ChangeMusicFactor(float value) {  
        musicFactor = value;
        _dataScriptableObject.musicFactor = musicFactor;
        audioSource.volume = musicFactor;
    }

    public void SetSelectedItems() {
        itemIds = UIStartManager.Instance.GetItemIds();
        _dataScriptableObject.itemIds = itemIds;
        _dataScriptableObject.SaveToFile();
    }

    public List<Item> GetItems() {
        return items;
    }

    public void AddScore() {
        currentScore++;
        if (currentScore > highestScore) {
            highestScore = currentScore;
            _dataScriptableObject.highestScore = highestScore;
        }
        UIPlayManager.Instance.ShowGold(currentScore);
        _dataScriptableObject.SaveToFile();
    }

    public void ChangeScene(int i) {
        currentScore = 0;
        if (i == 1) {
            IsPlaying = true;
        }
        SceneManager.LoadScene(i);
    }

    public void PauseGame() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MainController>().mainInputAction.Disable();
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MainController>().mainInputAction.Enable();
        Time.timeScale = 1f;
    }

    public void Quit() {
        // other data is saved when it is changed
        SetSelectedItems();
        Application.Quit();
    }
}
