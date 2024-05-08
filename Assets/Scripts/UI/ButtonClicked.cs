using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
    [SerializeField] private ButtonType buttonType;

    public void Clicked() {
        switch (buttonType) {
            case ButtonType.Start:
                UIStartManager.Instance.SetActiveUI(buttonType);
                break;
            case ButtonType.Setting:
                UIStartManager.Instance.SetActiveUI(buttonType);
                break;
            case ButtonType.Quit:
                GameManager.Instance.Quit();
                break;
            case ButtonType.Fight:
                GameManager.Instance.SetSelectedItems();
                GameManager.Instance.ChangeScene(1);
                break;
            case ButtonType.Pause:
                GameManager.Instance.PauseGame();
                UIPlayManager.Instance.SetActiveUI(buttonType);
                break;
            case ButtonType.Resume:
                GameManager.Instance.ResumeGame();
                UIPlayManager.Instance.SetActiveUI(buttonType);
                break;
            case ButtonType.ReStart:
                GameManager.Instance.ResumeGame();
                GameObject.FindGameObjectWithTag("Player").GetComponent<MainController>().mainInputAction.Dispose();
                GameManager.Instance.ChangeScene(1);
                break;
            case ButtonType.Exit:
                GameManager.Instance.ResumeGame();
                GameObject.FindGameObjectWithTag("Player").GetComponent<MainController>().mainInputAction.Dispose();
                GameManager.Instance.ChangeScene(0);
                break;
        }
    }
}
