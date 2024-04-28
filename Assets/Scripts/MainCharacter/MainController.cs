using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private int rotateSpeed = 200;

    [SerializeField] private float moveSpeed = 3f;      // movement
    private Vector3 moveDirection;
    private bool isMove = false;
    Vector3 v;

    private void Awake() {
        mainInfor = GetComponent<MainInfor>();

        mainInputAction = new MainInputAction();
        mainInputAction.Enable();
        mainInputAction.MainCharacter.MouseMovement.performed += Movement_performed;
        mainInputAction.MainCharacter.KeyMovement.performed += Movement_performed;
        mainInputAction.MainCharacter.Attack.performed += Attack_performed;

        changeMovementModeEvent = new UnityEvent<MovementMode>();
        changeMovementModeEvent.AddListener(ChangeMovementControllerMode);
        GameSettings.Instance.SetChangeMovementModeEvent(changeMovementModeEvent);

        rigidbody = GetComponent<Rigidbody>();

        moveToPos = transform.position;
    }

    private void Update() {
        if (Vector3.Distance(transform.position, moveToPos) > 0.1f) {
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
        if (GameSettings.Instance.GetMovementMode() == MovementMode.Mouse) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, layerMask)) {
                Instantiate(clickedPointObject, hit.point, clickedPointObject.transform.rotation);
                moveToPos = hit.point;
                v = moveToPos;
                StopCoroutine(RotateToPointCoroutine(1.25f * rotateSpeed));

                targetQuaternion = Quaternion.LookRotation(moveToPos - transform.position, Vector3.up);
                if (!isRotate) {
                    StartCoroutine(RotateToPointCoroutine(rotateSpeed));
                }
            }
        }

        if (mainInfor.IsAttack()) {
            mainInfor.DontAttack();
        }
    }

    private void Attack_performed(InputAction.CallbackContext obj) {
        if (!mainInfor.IsAttack()) {
            DontMove();
            mainInfor.ChangeState(MainState.Idle);
            mainInfor.ChangeState(MainState.Attack);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, layerMask)) {
                v = hit.point;
                StopAllCoroutines();
                targetQuaternion = Quaternion.LookRotation(hit.point - transform.position, Vector3.up);
                StartCoroutine(RotateToPointCoroutine(1.25f * rotateSpeed));
            }
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
}
