using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpiderScr : MonoBehaviour
{
    Rigidbody rd;
    Animator at;
    GameObject player;
    [SerializeField] AttackDetect attackDetect;
    UnityEvent<bool> attackHandle;

    float speed = 3.25f;
    float timeAttackDelay = 0.8f, timeAttack;
    bool isAttack = false;

    void Start()
    {
        rd = GetComponent<Rigidbody>();
        at = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        attackHandle = attackDetect.attackEvent;
        attackHandle.AddListener(AttackHandle);
        timeAttack = 0;
    }

    void Update()
    {
        if (!GameManager.Instance.IsPlaying)
            return;
        if (timeAttack > 0) {
            timeAttack -= Time.deltaTime;
            if (timeAttack <= 0) {
                at.SetTrigger("Hit");
            }
        }
    }

    void FixedUpdate(){
        if (!GameManager.Instance.IsPlaying)
            return;
        if (player != null && !isAttack) {
            Vector3 dirMove = (player.transform.position - transform.position);
            dirMove.Normalize();
            dirMove.y = 0;
            transform.LookAt(player.transform);
            transform.position += dirMove * speed * Time.deltaTime;
            at.SetBool("Run", true);
        }
    }

    void AttackHandle(bool attack) {
        if (attack) {
            isAttack = true;
            at.SetBool("Run", false);
            timeAttack = timeAttackDelay;
        }
        else {
            isAttack = false;
        }
    }
}
