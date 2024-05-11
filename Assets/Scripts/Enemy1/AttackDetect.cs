using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackDetect : MonoBehaviour
{
    public UnityEvent<bool> attackEvent = new UnityEvent<bool>();
    public bool CanAttack { get; set; }

    private void Awake() {
        CanAttack = true;
    }

    private void OnTriggerStay(Collider other) {
        MainInfor mainInfor = other.GetComponent<MainInfor>();
        if (mainInfor != null && CanAttack) {
            CanAttack = false;
            attackEvent.Invoke(true);
        }
    }

    public void StopAttack() {
        CanAttack = true;
        attackEvent.Invoke(false);
    }
}
