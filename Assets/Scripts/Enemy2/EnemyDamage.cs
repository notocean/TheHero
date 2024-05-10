using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider other) {
        MainInfor mainInfor = other.GetComponent<MainInfor>();
        if (mainInfor != null) {
            mainInfor.TakeDamage(damage);
        }
    }
}
