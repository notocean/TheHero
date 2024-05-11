using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIPlayManager : MonoBehaviour
{
    public static UIPlayManager Instance {  get; private set; }
    [SerializeField] private GameObject pauseFrameObj;
    [SerializeField] private GameObject loseFrameObj;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text highestScore;
    [SerializeField] private TMP_Text gold;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.Instance.PauseGame();
            SetActiveUI(ButtonType.Pause);
        }
    }

    public void SetActiveUI(ButtonType buttonType) {
        switch (buttonType) {
            case ButtonType.Pause:
                pauseFrameObj.SetActive(true);
                break;
            case ButtonType.Resume:
                pauseFrameObj.SetActive(false);
                break;
        }
        loseFrameObj.SetActive(false);
    }

    public void ShowLoseNotify() {
        score.text = GameManager.Instance.currentScore.ToString();
        GameManager.Instance.ChangeHighestScore();
        highestScore.text = GameManager.Instance.highestScore.ToString();
        GameManager.Instance.PauseGame();
        loseFrameObj.SetActive(true);
    }

    public void ShowGold(int value) {
        gold.text = value.ToString();
    }
}
