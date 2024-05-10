using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLookCamera : MonoBehaviour
{
    private Camera _camera;

    private void Awake() {
        _camera = Camera.main;
    }

    private void LateUpdate() {
        transform.rotation = Quaternion.LookRotation(-_camera.transform.forward);
    }
}
