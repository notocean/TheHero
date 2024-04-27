using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MainVirtual))]
public class MainInfor : MonoBehaviour
{
    private MainVirtual mainVirtual;
    private UnityEvent<MainState> changeStateEvent;

    private MainState state;
    private int attackIndex = -1;

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
        }
    }

    public bool IsState(MainState state) {
        return this.state == state;
    }

    public int GetAttackIndex() {
        attackIndex = (attackIndex + 1) % 3;
        return attackIndex + 1;
    }

    public void DontAttack() {
        mainVirtual.DontAttack();
    }
}
