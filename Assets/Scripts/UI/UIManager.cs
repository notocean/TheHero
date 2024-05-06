using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    [SerializeField] private GameObject selectItemsObj;
    [SerializeField] private GameObject settingFrameObj;
    [SerializeField] private GameObject selectedItemsObj;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    private void Start() {
        SetSelectedItemsObj();
    }

    private void SetSelectedItemsObj() {
        List<string> ids = GameManager.Instance.itemIds;
        if (ids.Count > 0) {
            List<SelectedItem> selectedItems = selectedItemsObj.GetComponentsInChildren<SelectedItem>().ToList<SelectedItem>();
            List<Item> items = GameManager.Instance.GetItems();
            int j = 0;
            for (int i = 0; i < items.Count && j < ids.Count; i++) {
                if (ids[j] == null) {
                    j++;
                }
                else if (items[i].Id.Equals(ids[j])) {
                    selectedItems[j++].SetItem(items[i]);
                }
            }
        }
    }

    public List<string> GetItemIds() {
        List<SelectedItem> selectedItems = selectedItemsObj.GetComponentsInChildren<SelectedItem>().ToList<SelectedItem>();
        List<string> ids = new List<string>();
        for (int i = 0; i < selectedItems.Count; i++) {
            if (selectedItems[i].item != null)
                ids.Add(selectedItems[i].item.Id);
            else ids.Add(null);
        }
        return ids;
    }

    public void SetActiveUI(ButtonType buttonType) {
        switch (buttonType) {
            case ButtonType.Start:
                if (selectItemsObj.activeSelf) {
                    selectItemsObj.SetActive(false);
                }
                else {
                    selectItemsObj.SetActive(true);
                    settingFrameObj.SetActive(false);
                }
                break;
            case ButtonType.Setting1:
                if (settingFrameObj.activeSelf) {
                    settingFrameObj.SetActive(false);
                }
                else {
                    settingFrameObj.SetActive(true);
                    selectItemsObj.SetActive(false);
                }
                break;
            case ButtonType.Quit:

                break;
            case ButtonType.Fight:
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
