using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MainInfor))]
public class MainController : MonoBehaviour
{
    private MainInfor mainInfor;

    public MainInputAction mainInputAction { get; private set; }

    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask wallLayerMask;
    [SerializeField] GameObject clickedPointObject;

    private Rigidbody rigidbd;

    private Vector3 moveToPos;                          // the main character needs to move to it
    private Quaternion currentQuaternion;               // rotation
    private Quaternion targetQuaternion;
    private bool isRotate = false;
    private int rotateSpeed;
    
    private float runSpeed;                       // movement
    private Vector3 moveDirection;
    private bool isMove = false;
    private bool isSurf = false;
    private float surfDis = 0f;
    private Vector3 surfDir = Vector3.zero;
    private Vector3 surfTarget = Vector3.zero;

    private void Awake() {
        mainInfor = GetComponent<MainInfor>();
        rotateSpeed = mainInfor.GetRotateSpeed();
        runSpeed = mainInfor.GetRunSpeed();

        mainInputAction = new MainInputAction();
        mainInputAction.Enable();
        mainInputAction.MainCharacter.MouseMovement.performed += Movement_performed;
        mainInputAction.MainCharacter.KeyMovement.performed += Movement_performed;
        mainInputAction.MainCharacter.Attack.performed += Attack_performed;
        mainInputAction.MainCharacter.SkillQ.performed += SkillQ_performed;
        mainInputAction.MainCharacter.SkillE.performed += SkillE_performed;

        rigidbd = GetComponent<Rigidbody>();

        moveToPos = transform.position;
    }

    private void Update() {
        if (Vector3.Distance(transform.position, moveToPos) > 0.1f && !mainInfor.IsState(MainState.Surf)) {
            isMove = true;
            moveDirection = (moveToPos - transform.position).normalized;
            mainInfor.ChangeState(MainState.Run);
        }
        else {
            isMove = false;
            if (mainInfor.IsState(MainState.Run)) {
                mainInfor.ChangeState(MainState.Idle);
            }
        }
    }

    private void FixedUpdate() {
        if (isMove && CanMove()) {
            Vector3 targetPos = transform.position + (isRotate ? 0.5f * runSpeed : runSpeed) * moveDirection * Time.deltaTime;
            rigidbd.MovePosition(targetPos);
        }
        if (isSurf) {
            surfDis += mainInfor.surfSpeed * Time.deltaTime;
            Vector3 pos = Vector3.Lerp(transform.position, surfTarget, surfDis / mainInfor.surfDistance);
            if (CanMove())
                rigidbd.MovePosition(pos);
            if (surfDis >= mainInfor.surfDistance) {
                isSurf = false;
                mainInfor.ChangeState(MainState.Idle);
                moveToPos = transform.position;
            }
        }
    }

    private bool CanMove() {
        bool canMove = true;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, 0.5f, wallLayerMask)) {
            canMove = false;
        }
        return canMove;
    }

    private void Movement_performed(InputAction.CallbackContext obj) {
        if (!mainInfor.IsAttack()) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, layerMask)) {
                Instantiate(clickedPointObject, hit.point, clickedPointObject.transform.rotation);
                moveToPos = hit.point;
                StopCoroutine(RotateToPointCoroutine(1.25f * rotateSpeed));

                targetQuaternion = Quaternion.LookRotation(moveToPos - transform.position, Vector3.up);
                if (!isRotate) {
                    StartCoroutine(RotateToPointCoroutine(rotateSpeed));
                }
            }

            if (mainInfor.IsAttack()) {
                mainInfor.DontAttack();
            }
        }
    }

    private void Attack_performed(InputAction.CallbackContext obj) {
        if (!mainInfor.IsAttack() && !mainInfor.IsState(MainState.Surf)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, layerMask)) {
                StopAllCoroutines();
                targetQuaternion = Quaternion.LookRotation(hit.point - transform.position, Vector3.up);

                if (mainInfor.GetAttackType() == 2) {
                    targetQuaternion *= Quaternion.Euler(0f, -60f, 0f);
                }
                else if (mainInfor.GetAttackType() == 3) {
                    targetQuaternion *= Quaternion.Euler(0f, -10f, 0f);
                }
                StartCoroutine(RotateToPointCoroutine(1.25f * rotateSpeed));
            }

            DontMove();
            mainInfor.ChangeState(MainState.Idle);
            mainInfor.ChangeState(MainState.Attack);
        }
    }

    private IEnumerator RotateToPointCoroutine(float rotateSpeed) {
        isRotate = true;
        currentQuaternion = transform.rotation;
        while (!Quaternion.Equals(currentQuaternion, targetQuaternion)) {
            Quaternion rotation = Quaternion.RotateTowards(currentQuaternion, targetQuaternion, rotateSpeed * Time.deltaTime);
            currentQuaternion = rotation;
            transform.rotation = currentQuaternion;

            yield return null;
        }
        isRotate = false;
    }

    public void DontMove() {
        moveToPos = transform.position;
    }

    private void SkillQ_performed(InputAction.CallbackContext obj) {
        if (mainInfor.canUseSkillQ) {
            mainInfor.StartCoroutine(mainInfor.SkillQ());
            mainInfor.StartCoroutine(mainInfor.CoolDownQ());
        }
    }

    private void SkillE_performed(InputAction.CallbackContext obj) {
        if (!mainInfor.IsState(MainState.Attack) && mainInfor.canUseSkillE) {
            mainInfor.StartCoroutine(mainInfor.CoolDownE());
            isRotate = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, layerMask)) {
                StopAllCoroutines();
                targetQuaternion = Quaternion.LookRotation(hit.point - transform.position, Vector3.up);
                transform.rotation = targetQuaternion;
                surfDis = 0f;
                surfDir = transform.forward;
                isSurf = true;
                surfTarget = transform.position + mainInfor.surfDistance * surfDir.normalized;
            }

            DontMove();
            mainInfor.ChangeState(MainState.Surf);
        }
    }
}
