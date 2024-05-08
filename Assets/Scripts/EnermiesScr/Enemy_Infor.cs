using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Infor : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 300.0f;
    public float getHealth{
        get{
            return this.health;
        }
    }
    [SerializeField] HealBarScr HealBar;
    [SerializeField] Animator at;
    [SerializeField] Component ct;
    [SerializeField] GameObject gold;
    [SerializeField] int goldNumber;
    [SerializeField] GameObject spriteCopy;
    public Enemy_Infor instantiate;

    void Awake(){
        instantiate = this;
    }
    
    void Start()
    {
        health = maxHealth;
        HealBar?.UpHeathBar(health,maxHealth);
    }

    void Update()
    {
        
    }

    public void TakeDame(float dame){
        this.health = Mathf.Clamp(health - dame, 0, maxHealth);
        HealBar?.UpHeathBar(health,maxHealth);
        if(this.health <= 0){
            at.SetBool("Die", true);
            Destroy(ct, 1f);
            if(gold != null){
                for(int i=0;i<goldNumber;i++){
                    Instantiate(gold);
                }
            }
            if(spriteCopy != null) Instantiate(gold);
            Destroy(gameObject,3.5f);
            Destroy(this);
        }
    }
}
