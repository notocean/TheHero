using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

[RequireComponent(typeof(MainVisual))]
public class MainInfor : MonoBehaviour
{
    private MainVisual mainVisual;
    private UnityEvent<MainState> changeStateEvent;
    private UnityEvent<float, bool> skillQVisual;

    [SerializeField] private TMP_Text runSpeedText;
    [SerializeField] private TMP_Text attackSpeedText;
    [SerializeField] private TMP_Text attackDamageText;
    [SerializeField] private TMP_Text armorText;
    [SerializeField] private GameObject itemsObj;
    [SerializeField] private Image cooldownQImage;
    [SerializeField] private Image cooldownEImage;
    [SerializeField] private Image healthBar;

    // basic infor
    private MainState state;
    private int attackType = 1;                     // main has 3 types of attacks
    private bool isAttack = false;
    private int rotateSpeed = 500;

    private int maxHealth = 800;
    private int health;
    private float attackSpeed = 1.8f;
    private float runSpeed = 5f;
    private int basicDamage = 100;
    private int armor = 10;

    // skill Q infor
    private float timeSkillQ = 2f;                      // time of increase attack speed
    private float increaseAttackSpeedFactor = 0.5f;
    private float increaseAttackDamageFactor = 0.5f;
    private float timerSkillQ = 0f;
    private float timeCooldownQ = 4f;
    public bool canUseSkillQ { get; private set; }

    // skill E infor
    public float surfDistance { get; set; }
    public float surfSpeed { get; set; }
    private float timeCooldownE = 2f;
    public bool canUseSkillE { get; private set; }

    private void Awake() {
        mainVisual = GetComponent<MainVisual>();

        changeStateEvent = mainVisual.changeStateEvent;
        skillQVisual = mainVisual.skillQVisual;

        surfDistance = 5f;
        surfSpeed = 15f;
        canUseSkillQ = canUseSkillE = true;
        health = maxHealth;
    }

    private void Start() {
        state = MainState.Idle;

        AddExtraFactor();
        SetItems();
        healthBar.fillAmount = 1;
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
        damageFactor = timerSkillQ == 0 ? damageFactor : increaseAttackDamageFactor * damageFactor + damageFactor;
        return Mathf.FloorToInt(damageFactor * basicDamage);
    }

    public IEnumerator SkillQ() {
        timerSkillQ = 0.001f;
        ShowInfor(true);
        skillQVisual?.Invoke(attackSpeed + attackSpeed * increaseAttackSpeedFactor, true);

        while (timerSkillQ < timeSkillQ) {
            timerSkillQ += Time.deltaTime;
            yield return null;
        }

        timerSkillQ = 0f;
        skillQVisual?.Invoke(attackSpeed, false);
        ShowInfor(false);
    }

    public IEnumerator CoolDownQ() {
        float timer = 0f;
        canUseSkillQ = false;
        while (timer <= timeCooldownQ) {
            timer += Time.deltaTime;
            cooldownQImage.fillAmount = timer / timeCooldownQ;
            yield return null;
        }
        canUseSkillQ = true;
    }

    public IEnumerator CoolDownE() {
        float timer = 0f;
        canUseSkillE = false;
        while (timer <= timeCooldownE) {
            timer += Time.deltaTime;
            cooldownEImage.fillAmount = timer / timeCooldownE;
            yield return null;
        }
        canUseSkillE = true;
    }

    public void AddExtraFactor() {
        int extraRunSpeed = 0, extraAttackSpeed = 0, extraAttackDamage = 0, extraArmor = 0;
        MainInfor mainInfor = GameObject.FindGameObjectWithTag("Player").GetComponent<MainInfor>();
        if (mainInfor != null && GameManager.Instance.itemIds.Count > 0) {
            List<Item> items = GameManager.Instance.GetItems();
            List<string> itemIds = GameManager.Instance.itemIds;

            for (int j = 0; j < itemIds.Count; j++) {
                if (itemIds[j] != null) {
                    for (int i = 0; i < items.Count; i++) {
                        if (items[i].Id.Equals(itemIds[j])) {
                            extraRunSpeed += items[i].RunSpeed;
                            extraAttackSpeed += items[i].AttackSpeed;
                            extraAttackDamage += items[i].AttackDamage;
                            extraArmor += items[i].Armor;
                        }
                    }
                }
            }
        }

        mainVisual.IncreaseRunSpeed(extraRunSpeed / 100f);
        runSpeed += extraRunSpeed / 100f * runSpeed;
        mainVisual.IncreaseAttackSpeed(extraAttackSpeed / 100f * attackSpeed);
        attackSpeed += extraAttackSpeed / 100f * attackSpeed;
        basicDamage += extraAttackDamage;
        armor += extraArmor;

        ShowInfor(false);
    }

    public void ShowInfor(bool useSkillQ) {
        runSpeedText.text = runSpeed.ToString();
        if (useSkillQ) {
            attackSpeedText.text = (attackSpeed + attackSpeed * increaseAttackSpeedFactor).ToString();
            attackDamageText.text = GetBasicAttackDamage(1).ToString();
        }
        else {
            attackSpeedText.text = attackSpeed.ToString();
            attackDamageText.text = basicDamage.ToString();
        }
        armorText.text = armor.ToString();
    }

    public void SetItems() {
        List<string> ids = GameManager.Instance.itemIds;
        if (ids.Count > 0) {
            List<SelectedItem> selectedItems = itemsObj.GetComponentsInChildren<SelectedItem>().ToList<SelectedItem>();
            List<Item> items = GameManager.Instance.GetItems();
            for (int j = 0; j < selectedItems.Count; j++) {
                if (ids[j] != null) {
                    for (int i = 0; i < items.Count; i++) {
                        if (items[i].Id.Equals(ids[j])) {
                            selectedItems[j].SetItem(items[i]);
                        }
                    }
                }
            }
        }
    }

    public void TakeDamage(int damage) {
        int currHealth = health;
        health = Mathf.Clamp(Mathf.FloorToInt(health - damage * 100f / (100 + armor)), 0, currHealth);
        healthBar.fillAmount = 1f * health / maxHealth;

        if (health == 0) {
            GetComponent<MainController>().mainInputAction.Disable();
            ChangeState(MainState.Die);
        }
    }

    public void IsDie() {
        UIPlayManager.Instance.ShowLoseNotify();
    }
}
