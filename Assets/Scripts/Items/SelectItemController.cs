using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectItemController : MonoBehaviour
{
    public static SelectItemController Instance { get; private set; }
    Item selectingItem;

    [SerializeField] GameObject nameItemPos;
    [SerializeField] GameObject inforItemPos;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public void SetSelectingItem(Item item) {
        ShowInfor(item);
        selectingItem = item;
    }

    public void RemoveSelectingItem(Item item) {
        if (item == null || selectingItem == null) {
            selectingItem = null;
            return;
        }
        if (item.Equals(selectingItem)) {
            selectingItem = null;
        }
    }

    public void ShowInfor(Item itemInfor) {
        nameItemPos.GetComponent<TMP_Text>().text = itemInfor.Name;
        string infor = "";
        infor = ConnectString(infor, itemInfor.RunSpeed, "% run speed\n");
        infor = ConnectString(infor, itemInfor.AttackSpeed, "% attack speed\n");
        infor = ConnectString(infor, itemInfor.AttackDamage, " attack damage\n");
        infor = ConnectString(infor, itemInfor.Armor, " armor\n");
        inforItemPos.GetComponent<TMP_Text>().text = infor;
    }

    private string ConnectString(string str, int value, string str2) {
        if (value != 0) {
            str += "+ " + value.ToString() + str2;
        }
        return str;
    }

    public void GetItem(SelectedItem selectedItem) {
        selectedItem.GetComponent<SelectedItem>().SetItem(selectingItem);
        RemoveSelectingItem(selectingItem);
    }
}
