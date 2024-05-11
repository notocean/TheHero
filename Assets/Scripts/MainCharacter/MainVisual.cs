using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class MainVisual : MonoBehaviour
{
    // reference to some script
    private MainInfor mainInfor;

    private Animator animator;

    [HideInInspector] public UnityEvent<MainState> changeStateEvent = new UnityEvent<MainState>();
    [HideInInspector] public UnityEvent<float, bool> skillQVisual = new UnityEvent<float, bool>();

    [SerializeField] private List<GameObject> slashObj;
    [SerializeField] private List<WeaponDamage> basicAttack;
    [SerializeField] private GameObject lightingTrailObj;

    [SerializeField] private Material increaseAttackSpeedMaterial;

    // sound
    private AudioSource audioSource;
    [SerializeField] private AudioClip slashAudio;
    [SerializeField] private AudioClip skillEAudio;

    private void Awake() {
        mainInfor = GetComponent<MainInfor>();

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        changeStateEvent.AddListener(ChangeStateHandle);
        skillQVisual.AddListener(SkillQVisual);

        increaseAttackSpeedMaterial.DisableKeyword("_EMISSION");
    }

    private void Start() {
        audioSource.volume = GameManager.Instance.soundFactor;
    }

    private void ChangeStateHandle(MainState state) {
        switch (state) {
            case MainState.Idle:
                animator.SetBool("IsRun", false);
                animator.SetBool("IsSurf", false);
                lightingTrailObj.GetComponent<VisualEffect>().Stop();
                audioSource.Stop();
                break;
            case MainState.Run:
                animator.SetBool("IsRun", true);
                audioSource.Play();
                break;
            case MainState.Attack:
                animator.SetInteger("Attack", mainInfor.GetAttackType());
                mainInfor.ChangeAttackType();
                break;
            case MainState.Surf:
                animator.SetBool("IsSurf", true);
                lightingTrailObj.GetComponent<VisualEffect>().Play();
                audioSource.PlayOneShot(skillEAudio, GameManager.Instance.soundFactor);
                break;
            case MainState.Die:
                animator.SetBool("Dying", true);
                break;
        }
    }

    public void StopAttack() {
        animator.SetInteger("Attack", 0);
    }

    public void EnableSlash(int i) {
        basicAttack[i].SetDamage(mainInfor.GetBasicAttackDamage(i));
        slashObj[i].SetActive(true);
        audioSource.PlayOneShot(slashAudio, 0.7f * GameManager.Instance.soundFactor);
    }

    public void DisableSlash(int i) {
        slashObj[i].SetActive(false);
    }

    public void SkillQVisual(float attackSpeed, bool isEffect) {
        animator.SetFloat("AttackSpeed", attackSpeed);
        if (isEffect) {
            increaseAttackSpeedMaterial.EnableKeyword("_EMISSION");
        }
        else {
            increaseAttackSpeedMaterial.DisableKeyword("_EMISSION");
        }
    }

    public void IncreaseRunSpeed(float value) {
        animator.SetFloat("RunSpeed", animator.GetFloat("RunSpeed") + value);
    }

    public void IncreaseAttackSpeed(float value) {
        animator.SetFloat("AttackSpeed", animator.GetFloat("AttackSpeed") + value);
    }
}
