using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension {

    [SerializeField]
    private float cameraSpeed = 10f;

    [SerializeField]
    private float clampAngle = 80f; // How far camera can be rotated upwards and downwards

    private InputManager inputManager;
    private Vector3 rotation;

    protected override void Awake() {
        inputManager = InputManager.Instance;
        // Get starting rotation
        if (rotation == null) rotation = transform.localRotation.eulerAngles;
        base.Awake(); // Calls Awake from parent class
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime) {
        if (vcam.Follow) {
            if (stage == CinemachineCore.Stage.Aim) {
                Vector2 deltaInput = inputManager.GetMouseDelta();
                rotation.x += deltaInput.x * cameraSpeed * Time.deltaTime;
                rotation.y += deltaInput.y * cameraSpeed * Time.deltaTime;
                rotation.y = Mathf.Clamp(rotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(-rotation.y, rotation.x, 0f);
            }
        }
    }
}
