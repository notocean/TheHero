using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    private int damage;
    [SerializeField] private GameObject hitSlashObj;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip slashHitAudio;

    private void Start() {
        audioSource.volume = GameManager.Instance.soundFactor;
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other) {
        EnemyInforTest enemyInfor = other.GetComponent<EnemyInforTest>();
        if (enemyInfor != null) {
            enemyInfor.TakeDamage(damage);
            Vector3 pos = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            Instantiate(hitSlashObj, pos, hitSlashObj.transform.rotation);

            audioSource.PlayOneShot(slashHitAudio, 0.8f);
        }
    }
}
