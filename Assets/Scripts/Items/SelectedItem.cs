using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItem : MonoBehaviour
{
    public Item item { get; private set; }
    [SerializeField] private Image image;

    public void Clicked() {
        SelectItemController.Instance.GetItem(this);
        if (item != null) {
            SelectItemController.Instance.ShowInfor(item);
        }
    }

    public void SetItem(Item item) {
        if (item != null) {
            this.item = item;
            image.sprite = item.Image.sprite;
        }
        GameManager.Instance.SetSelectedItems();
    }
}
