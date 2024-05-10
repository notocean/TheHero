using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GolemScr : MonoBehaviour
{
    Rigidbody rd;
    [SerializeField] Animator at;
    GameObject player;
    [SerializeField] AttackDetect attackDetect;
    UnityEvent<bool> attackHandle;

    bool isAttack = false;
    float speed = 3.5f;
    float timeAttackDelay = 1f, timerAttack, timerAttack2;
    int attackType = 0;
    Vector3 dirMove = Vector3.zero;

    private void Awake() {
        rd = GetComponent<Rigidbody>();
        attackHandle = attackDetect.attackEvent;
        attackHandle.AddListener(AttackHandle);
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (!GameManager.Instance.IsPlaying)
            return;
        if (timerAttack > 0) {
            timerAttack -= Time.deltaTime;
            if (timerAttack <= 0) {
                at.SetTrigger("Hit");
            }
        }
        else if (timerAttack2 > 0) {
            timerAttack2 -= Time.deltaTime;
            if (timerAttack2 <= 0) {
                at.SetTrigger("Hit2");
            }
        }
        dirMove = (player.transform.position - transform.position);
    }

    void FixedUpdate(){
        if (!GameManager.Instance.IsPlaying)
            return;
        if (player != null && !isAttack) {
            dirMove.Normalize();
            dirMove.y = 0;
            transform.LookAt(player.transform);
            transform.Translate(dirMove * speed * Time.deltaTime, Space.World);
            at.SetBool("Walk", true);
        }
    }

    void AttackHandle(bool attack) {
        if (attack) {
            isAttack = true;
            at.SetBool("Idle", false);
            at.SetBool("Walk", false);
            if (attackType == 0) {
                timerAttack = timeAttackDelay;
            }
            else {
                timerAttack2 = timeAttackDelay;
            }
            attackType = (attackType + 1) % 2;
        }
        else {
            isAttack = false;
            at.SetBool("Idle", true);
        } 
    }
}
