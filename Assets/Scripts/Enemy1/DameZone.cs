using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameZone : MonoBehaviour
{
    [SerializeField] int damage = 100;
    
    private void OnTriggerEnter(Collider c) {
        MainInfor mainInfor = c.GetComponent<MainInfor>();
        if (mainInfor != null) {
            mainInfor.TakeDamage(damage);
        }
    }
}
