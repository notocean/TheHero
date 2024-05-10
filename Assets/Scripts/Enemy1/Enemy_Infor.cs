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
    [SerializeField] GameObject goldObj;
    [SerializeField] int goldValue;
    [SerializeField] Collider _collider;
    
    void Start()
    {
        health = maxHealth;
        HealBar?.UpHeathBar(health,maxHealth);
    }

    public void TakeDamage(float dame){
        this.health = Mathf.Clamp(health - dame, 0, maxHealth);
        HealBar?.UpHeathBar(health,maxHealth);
        if(this.health <= 0){
            at.SetTrigger("Die");
            Destroy(ct, 1f);
            HealBar.gameObject.SetActive(false);
            Destroy(gameObject, 2f);
            _collider.enabled = false;
            EnemySpawnController.Instance.SpawnEnemy();
            GameObject gold = Instantiate(goldObj, transform.position, goldObj.transform.rotation);
            gold.GetComponentInChildren<ItemCollected>().SetValue(goldValue);
        }
    }
}
