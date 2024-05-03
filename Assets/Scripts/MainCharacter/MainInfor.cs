using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MainVirtual))]
public class MainInfor : MonoBehaviour
{
    private MainVirtual mainVirtual;
    private UnityEvent<MainState> changeStateEvent;

    [SerializeField] private GameObject swordObject;


    // basic infor
    private MainState state;
    private int attackType = 1;                    // main has 3 types of attacks
    private bool isAttack = false;

    private int rotateSpeed = 500;
    private float moveSpeed = 5f;
    private int basicDamage = 100;
    private int armor = 10;
    
    // skill infor

    private void Awake() {
        mainVirtual = GetComponent<MainVirtual>();

        changeStateEvent = mainVirtual.changeStateEvent;
    }

    private void Start() {
        state = MainState.Idle;
    }

    private void Update() {

    }

    public void ChangeState(MainState state) {
        if (this.state != state) {
            this.state = state;

            changeStateEvent?.Invoke(state);
            if (state == MainState.Attack) {
                isAttack = true;

                switch (attackType) {
                    case 1:
                        swordObject.GetComponent<WeaponDamage>().SetDamage(Mathf.FloorToInt(1.5f * basicDamage));
                        break;
                    default:
                        swordObject.GetComponent<WeaponDamage>().SetDamage(Mathf.FloorToInt(basicDamage));
                        break;
                }
            }
        }
    }

    public bool IsState(MainState state) {
        return this.state == state;
    }

    // return values from 1 to 3 and increase each time the function is called
    public void ChangeAttackType() {
        attackType = attackType % 3 + 1;
    }

    public int GetAttackType() {
        return attackType;
    }

    public void DontAttack() {
        mainVirtual.StopAttack();
        isAttack = false;
    }

    public bool IsAttack() {
        return isAttack;
    }

    public int GetRotateSpeed() {
        return rotateSpeed;
    }

    public float GetMoveSpeed() {
        return moveSpeed;
    }
}
