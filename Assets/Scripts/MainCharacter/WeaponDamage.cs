using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    private int damage;
    [SerializeField] private GameObject hitSlashObj;

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    public int GetDamage() {
        return damage;
    }

    public GameObject GetHitSlashObj() {
        return hitSlashObj;
    }
}
