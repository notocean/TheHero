using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance {  get; private set; }

    private MovementMode movementMode;
    private UnityEvent<MovementMode> changeMovementModeEvent;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        ChangeMovementMode(MovementMode.Mouse);
    }

    private void ChangeMovementMode(MovementMode mode) {
        movementMode = mode;

        changeMovementModeEvent?.Invoke(mode);
    }

    public MovementMode GetMovementMode() {
        return movementMode;
    }

    public void SetChangeMovementModeEvent(UnityEvent<MovementMode> changeMovementModeEvent) {
        this.changeMovementModeEvent = changeMovementModeEvent;
    }
}
