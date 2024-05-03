using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] GameObject damageNotifyOb;
    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        WeaponDamage weaponDamage = other.GetComponent<WeaponDamage>();
        if (weaponDamage != null) {
            GameObject gObject = Instantiate(damageNotifyOb, transform.position, Quaternion.Euler(0, 180, 0));
            gObject.GetComponentInChildren<TMP_Text>().text = weaponDamage.GetDamage().ToString();
            animator.SetTrigger("takedamage");
        }
    }
}
