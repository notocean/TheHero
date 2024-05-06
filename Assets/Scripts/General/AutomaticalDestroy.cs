using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AutomaticalDestroy : MonoBehaviour
{
    [SerializeField] private float timeLife;
    private float timer = 0f;

    private void LateUpdate() {
        if (timer < timeLife) {
            timer += Time.deltaTime;
        }
        else {
            Destroy(gameObject);
        }
    }
}
