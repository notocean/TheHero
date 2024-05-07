using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Infor : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 300.0f;
    [SerializeField] HealBarScr HealBar;
    [SerializeField] Animator at;
    [SerializeField] Component ct;
    
    void Start()
    {
        health = maxHealth;
        HealBar?.UpHeathBar(health,maxHealth);
    }

    void Update()
    {
        
    }

    public void TakeDame(float dame){
        this.health = Mathf.Clamp(health + dame, 0, maxHealth);
        HealBar?.UpHeathBar(health,maxHealth);
        at.SetTrigger("Damage");
        if(this.health <= 0){
            at.SetBool("Die", true);
            Destroy(ct);
            Destroy(gameObject,3.5f);
        }
    }
}
