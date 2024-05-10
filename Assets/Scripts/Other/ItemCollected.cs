using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ItemCollected : MonoBehaviour
{
    private int value;

    private void OnTriggerEnter(Collider other) {
        MainInfor mainInfor = other.GetComponent<MainInfor>();
        if (mainInfor != null) {
            for (int i = 0; i < value; i++)
                GameManager.Instance.AddScore();
            Destroy(gameObject);
        }
    }

    public void SetValue(int value) {
        this.value = value;
    }
}
