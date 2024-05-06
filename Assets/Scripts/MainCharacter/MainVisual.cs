using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainVisual : MonoBehaviour
{
    // reference to some script
    private MainInfor mainInfor;

    private Animator animator;

    [HideInInspector] public UnityEvent<MainState> changeStateEvent;
    [HideInInspector] public UnityEvent<float, bool> increaseAttackSpeedEvent;

    [SerializeField] private List<GameObject> slashObj;
    [SerializeField] private GameObject slashObj_Skill1;
    [SerializeField] private List<WeaponDamage> basicAttack;

    [SerializeField] private Material increaseAttackSpeedMaterial;

    private void Awake() {
        mainInfor = GetComponent<MainInfor>();

        animator = GetComponent<Animator>();

        changeStateEvent = new UnityEvent<MainState>();
        changeStateEvent.AddListener(ChangeStateHandle);
        increaseAttackSpeedEvent = new UnityEvent<float, bool>();
        increaseAttackSpeedEvent.AddListener(IncreaseAttackSpeedHandle);

        increaseAttackSpeedMaterial.DisableKeyword("_EMISSION");
    }

    private void Update() {

    }

    private void ChangeStateHandle(MainState state) {
        switch (state) {
            case MainState.Idle:
                animator.SetBool("IsRun", false);
                animator.SetBool("IsSurf", false);
                break;
            case MainState.Run:
                animator.SetBool("IsRun", true);
                break;
            case MainState.Attack:
                animator.SetInteger("Attack", mainInfor.GetAttackType());
                mainInfor.ChangeAttackType();
                break;
            case MainState.Surf:
                animator.SetBool("IsSurf", true);
                break;
        }
    }

    public void StopAttack() {
        animator.SetInteger("Attack", 0);
    }

    public void EnableSlash(int i) {
        basicAttack[i].SetDamage(mainInfor.GetBasicAttackDamage(i));
        slashObj[i].SetActive(true);
    }

    public void DisableSlash(int i) {
        slashObj[i].SetActive(false);
    }

    public void IncreaseAttackSpeedHandle(float attackSpeed, bool isEffect) {
        animator.SetFloat("AttackSpeed", attackSpeed);
        if (isEffect) {
            increaseAttackSpeedMaterial.EnableKeyword("_EMISSION");
        }
        else {
            increaseAttackSpeedMaterial.DisableKeyword("_EMISSION");
        }
    }
}
