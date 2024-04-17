using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class MainRigHandle : MonoBehaviour
{
    // reference to some script
    private MainInfor mainInfor;
    private RigBuilder rigBuilder;

    // variable handle the change of rig layers
    [HideInInspector] public UnityEvent<byte> rigHandleEvent;
    private byte rigCount;

    // variable handle hand pos target of main
    [SerializeField] private Transform handPosTarget;
    [SerializeField] private Transform spinePos;
    private Vector3 spinePos2HandPosTarget;

    private void Awake() {
        mainInfor = GetComponent<MainInfor>();
        rigBuilder = GetComponent<RigBuilder>();

        rigHandleEvent = new UnityEvent<byte>();
        rigHandleEvent.AddListener(RigHandle);
        rigCount = (byte)rigBuilder.layers.Count;

        spinePos2HandPosTarget = handPosTarget.position - spinePos.position;
    }

    private void Update() {
        if (mainInfor.IsState(MainState.Run)) {
            UpdateHandPosTarget();
        }
    }

    private void RigHandle(byte layerRig) {
        for (byte i = 0; i < rigCount; i++) {
            if (i != layerRig)
                rigBuilder.layers[i].active = false;
        }

        rigBuilder.layers[layerRig].active = true;
    }


    public void UpdateHandPosTarget() {
        handPosTarget.position = spinePos.position + spinePos2HandPosTarget;
    }
}
