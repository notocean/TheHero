using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    private int damage;
    [SerializeField] private GameObject hitSlashObj;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip slashHitAudio;

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other) {
        Enemy_Infor enemy_Infor = other.GetComponent<Enemy_Infor>();
        Vector3 pos = other.ClosestPoint(transform.position);
        if (enemy_Infor != null) {
            enemy_Infor.TakeDamage(damage);
            audioSource.PlayOneShot(slashHitAudio);
            Instantiate(hitSlashObj, pos, hitSlashObj.transform.rotation);
        }
        EnemyInfor enemyInfor = other.GetComponent<EnemyInfor>();
        if (enemyInfor != null) {
            enemyInfor.TakeDamage(damage);
            audioSource.PlayOneShot(slashHitAudio);
            Instantiate(hitSlashObj, pos, hitSlashObj.transform.rotation);
        }
    }
}
