using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyInforTest : MonoBehaviour
{
    [SerializeField] GameObject damageNotifyOb;
    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage) {
        Debug.Log(damage);
        damageNotifyOb.GetComponentInChildren<TMP_Text>().text = damage.ToString();
        Instantiate(damageNotifyOb, transform.position, Quaternion.identity);
        animator.SetTrigger("takedamage");
    }
}
