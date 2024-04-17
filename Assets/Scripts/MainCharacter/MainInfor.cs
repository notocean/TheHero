using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainInfor : MonoBehaviour
{
    private UnityEvent<byte> rigHandleEvent;

    private MainState state;

    private void Awake() {
        rigHandleEvent = GetComponent<MainRigHandle>().rigHandleEvent;
    }

    private void Start() {
        ChangeState(MainState.Run);
    }

    private void ChangeState(MainState state) {
        this.state = state;

        rigHandleEvent?.Invoke((byte)state);
    }

    public bool IsState(MainState state) {
        return this.state == state;
    }
}
