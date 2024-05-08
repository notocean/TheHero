using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour {
    [field: SerializeField] public string Id { get; set; }
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public int RunSpeed { get; set; }
    [field: SerializeField] public int AttackSpeed { get; set; }
    [field: SerializeField] public int AttackDamage { get; set; }
    [field: SerializeField] public int Armor { get; set; }
    [field: SerializeField] public Image Image { get; set; }

    public void Clicked() {
        SelectItemController.Instance.SetSelectingItem(this);
    }

    public override bool Equals(object other) {
        Item item = other as Item;
        return Id.Equals(item.Id);
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }
}
