using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
    [SerializeField] private ButtonType buttonType;

    public void Clicked() {
        switch (buttonType) {
            case ButtonType.Start:
                UIManager.Instance.SetActiveUI(ButtonType.Start);
                break;
            case ButtonType.Setting1:
                UIManager.Instance.SetActiveUI(ButtonType.Setting1);
                break;
            case ButtonType.Quit:
                GameManager.Instance.Quit();
                break;
            case ButtonType.Fight:
                GameManager.Instance.SetSelectedItems();
                GameManager.Instance.ChangeScene(1);
                break;
            case ButtonType.Pause:

                break;
            case ButtonType.Resume:

                break;
            case ButtonType.ReStart:

                break;
            case ButtonType.Setting2:

                break;
            case ButtonType.Exit:

                break;
        }
    }
}
