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

    private MainInputAction mainInputAction;
    private UnityEvent<MovementMode> changeMovementModeEvent;

    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject clickedPointObject;

    private new Rigidbody rigidbody;

    private Vector3 moveToPos;                          // the main character needs to move to it
    private Quaternion currentQuaternion;               // rotation
    private Quaternion targetQuaternion;
    private bool isRotate = false;
    private int rotateSpeed;
    
    private float moveSpeed;                       // movement
    private Vector3 moveDirection;
    private bool isMove = false;
    private bool isSurf = false;
    private float surfDis = 0f;
    private Vector3 surfDir = Vector3.zero;

    private void Awake() {
        mainInfor = GetComponent<MainInfor>();
        rotateSpeed = mainInfor.GetRotateSpeed();
        moveSpeed = mainInfor.GetMoveSpeed();

        mainInputAction = new MainInputAction();
        mainInputAction.Enable();
        mainInputAction.MainCharacter.MouseMovement.performed += Movement_performed;
        mainInputAction.MainCharacter.KeyMovement.performed += Movement_performed;
        mainInputAction.MainCharacter.Attack.performed += Attack_performed;
        mainInputAction.MainCharacter.Skill1.performed += Skill1_performed;
        mainInputAction.MainCharacter.Skill2.performed += Skill2_performed;
        mainInputAction.MainCharacter.Skill3.performed += Skill3_performed;
        mainInputAction.MainCharacter.Skill4.performed += Skill4_performed;

        changeMovementModeEvent = new UnityEvent<MovementMode>();
        changeMovementModeEvent.AddListener(ChangeMovementControllerMode);
        GameSettings.Instance.SetChangeMovementModeEvent(changeMovementModeEvent);

        rigidbody = GetComponent<Rigidbody>();

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
        if (isMove) {
            Vector3 targetPos = transform.position + (isRotate ? 0.5f * moveSpeed: moveSpeed) * moveDirection * Time.deltaTime;
            rigidbody.MovePosition(targetPos);
        }
        if (isSurf) {
            surfDis += (mainInfor.surfSpeed * surfDir * Time.deltaTime).magnitude;
            Vector3 pos = transform.position + mainInfor.surfSpeed * surfDir * Time.deltaTime;
            rigidbody.MovePosition(pos);
            if (surfDis >= mainInfor.surfDistance) {
                isSurf = false;
                mainInfor.ChangeState(MainState.Idle);
                moveToPos = transform.position;
            }
        }
    }

    private void ChangeMovementControllerMode(MovementMode mode) {
        if (mode == MovementMode.Mouse) {
            mainInputAction.MainCharacter.MouseMovement.Enable();
            mainInputAction.MainCharacter.KeyMovement.Disable();
        }
        else {
            mainInputAction.MainCharacter.MouseMovement.Disable();
            mainInputAction.MainCharacter.KeyMovement.Enable();
        }
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

    private void Skill1_performed(InputAction.CallbackContext obj) {
        mainInfor.StopCoroutine(mainInfor.IncreaseAttackSpeed());
        mainInfor.StartCoroutine(mainInfor.IncreaseAttackSpeed());
    }

    private void Skill2_performed(InputAction.CallbackContext obj) {

    }

    private void Skill3_performed(InputAction.CallbackContext obj) {
        if (!mainInfor.IsState(MainState.Attack)) {
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
                //StartCoroutine(Surf(transform.forward));
            }

            DontMove();
            mainInfor.ChangeState(MainState.Surf);
        }
    }

    //private IEnumerator Surf(Vector3 surfDir) {
    //    float dis = 0f;

    //    while (dis < mainInfor.surfDistance) {
    //        dis += (mainInfor.surfSpeed * surfDir * Time.deltaTime).magnitude;
    //        Vector3 pos = rigidbody.position + mainInfor.surfSpeed * surfDir * Time.deltaTime;
    //        Debug.Log(pos);
    //        rigidbody.MovePosition(pos);
    //        yield return null;
    //    }

    //    mainInfor.ChangeState(MainState.Idle);
    //}

    private void Skill4_performed(InputAction.CallbackContext obj) {

    }
}
