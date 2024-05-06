using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

[RequireComponent(typeof(MainVisual))]
public class MainInfor : MonoBehaviour
{
    private MainVisual mainVisual;
    private UnityEvent<MainState> changeStateEvent;
    private UnityEvent<float, bool> skillQVisual;

    // basic infor
    private MainState state;
    private int attackType = 1;                     // main has 3 types of attacks
    private bool isAttack = false;
    private int rotateSpeed = 500;

    private int health = 800;
    private float attackSpeed = 1.8f;
    private float runSpeed = 5f;
    private int basicDamage = 100;
    private int armor = 10;

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
        skillQVisual = mainVisual.skillQVisual;

        surfDistance = 5f;
        surfSpeed = 15f;
    }

    private void Start() {
        state = MainState.Idle;

        AddExtraFactor();
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

    public float GetRunSpeed() {
        return runSpeed;
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

    public IEnumerator SkillQ() {
        skillQVisual?.Invoke(attackSpeed + attackSpeed * increaseAttackSpeedFactor, true);

        while (timerSkill1 < timeSkill1) {
            timerSkill1 += Time.deltaTime;
            yield return null;
        }

        timerSkill1 = 0f;
        skillQVisual?.Invoke(attackSpeed, false);
    }

    public void AddExtraFactor() {
        int extraRunSpeed = 0, extraAttackSpeed = 0, extraAttackDamage = 0, extraArmor = 0;
        MainInfor mainInfor = GameObject.FindGameObjectWithTag("Player").GetComponent<MainInfor>();
        if (mainInfor != null && GameManager.Instance.itemIds.Count > 0) {
            List<Item> items = GameManager.Instance.GetItems();
            List<string> itemIds = GameManager.Instance.itemIds;
            int j = 0;
            for (int k = 0; k < items.Count && j < itemIds.Count; k++) {
                if (itemIds[j] == null) {
                    j++;
                }
                else if (items[k].Id.Equals(itemIds[j])) {
                    extraRunSpeed += items[k].RunSpeed;
                    extraAttackSpeed += items[k].AttackSpeed;
                    extraAttackDamage += items[k].AttackDamage;
                    extraArmor += items[k].Armor;
                    j++;
                }
            }
        }
        mainVisual.IncreaseRunSpeed(extraRunSpeed / 100f);
        runSpeed += extraRunSpeed / 100f * runSpeed;
        mainVisual.IncreaseAttackSpeed(extraAttackSpeed / 100f * attackSpeed);
        attackSpeed += extraAttackSpeed / 100f * attackSpeed;
        basicDamage += extraAttackDamage;
        armor += extraArmor;
    }

    public void TakeDamage(int damage) {
        int currHealth = health;
        health = Mathf.Clamp(Mathf.FloorToInt(health - damage * 100f / (100 + armor)), 0, currHealth);
    }
}
