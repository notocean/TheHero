using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MainVisual))]
public class MainInfor : MonoBehaviour
{
    private MainVisual mainVisual;
    private UnityEvent<MainState> changeStateEvent;
    private UnityEvent<float, bool> increaseAttackSpeedEvent;

    // basic infor
    private MainState state;
    private int attackType = 1;                     // main has 3 types of attacks
    private bool isAttack = false;
    private float attackSpeed = 1.8f;

    private int rotateSpeed = 500;
    private float moveSpeed = 5f;
    private int basicDamage = 100;
    //private int armor = 10;

    // skill 1 infor
    private float timeSkill1 = 2f;                      // time of increase attack speed
    private float increaseAttackSpeedFactor = 0.5f;
    private float increaseAttackDamageFactor = 0.5f;
    private float timerSkill1 = 0f;

    // skill 3 infor
    public float surfDistance { get; set; }
    public float surfSpeed { get; set; }

    private void Awake() {
        mainVisual = GetComponent<MainVisual>();

        changeStateEvent = mainVisual.changeStateEvent;
        increaseAttackSpeedEvent = mainVisual.increaseAttackSpeedEvent;

        surfDistance = 5f;
        surfSpeed = 10f;
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
        mainVisual.StopAttack();
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

    public int GetBasicAttackDamage(int i) {
        float damageFactor = i == 0 ? 1.5f : 1f;
        damageFactor = timerSkill1 == 0 ? damageFactor : increaseAttackDamageFactor * damageFactor + damageFactor;
        return Mathf.FloorToInt(damageFactor * basicDamage);
    }

    public void SetSkill1Time(float factor) {
        timerSkill1 *= factor;
    }

    public void SetIncreaseAttackSpeedFactor(float factor) {
        increaseAttackSpeedFactor *= factor;
    }

    public void SetIncreaseAttackDamageFactor(float factor) {
        increaseAttackDamageFactor *= factor;
    }

    public IEnumerator IncreaseAttackSpeed() {
        increaseAttackSpeedEvent?.Invoke(attackSpeed + attackSpeed * increaseAttackSpeedFactor, true);

        while (timerSkill1 < timeSkill1) {
            timerSkill1 += Time.deltaTime;
            yield return null;
        }

        timerSkill1 = 0f;
        increaseAttackSpeedEvent?.Invoke(attackSpeed, false);
    }
}
