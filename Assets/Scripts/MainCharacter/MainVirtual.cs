using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainVirtual : MonoBehaviour
{
    // reference to some script
    private MainInfor mainInfor;

    private Animator animator;

    [HideInInspector] public UnityEvent<MainState> changeStateEvent;

    private void Awake() {
        mainInfor = GetComponent<MainInfor>();

        animator = GetComponent<Animator>();

        changeStateEvent = new UnityEvent<MainState>();
        changeStateEvent.AddListener(ChangeStateHandle);
    }

    private void Update() {

    }

    private void ChangeStateHandle(MainState state) {
        switch (state) {
            case MainState.Idle:
                animator.SetBool("IsRun", false);
                break;
            case MainState.Run:
                animator.SetBool("IsRun", true);
                break;
            case MainState.Attack:
                animator.SetInteger("Attack", mainInfor.GetAttackType());
                mainInfor.ChangeAttackType();
                break;
        }
    }

    public void StopAttack() {
        animator.SetInteger("Attack", 0);
    }
}
