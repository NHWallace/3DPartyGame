using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Basis of controller provided by example by Unity at https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
// Example has been modified to use the new input system

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;

    private void Start() {
        controller = gameObject.GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update() {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        Vector2 deltaInput = inputManager.GetMouseDelta();
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f; // Ensure above operation does not alter y
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Change direction player faces on mouse movement or WASD movement
        if (deltaInput != Vector2.zero || movement != Vector2.zero) {
            gameObject.transform.forward = new Vector3 (cameraTransform.forward.x, 0f, cameraTransform.forward.z);
        }

        // Changes the height position of the player..
        if (inputManager.PlayerJumpedThisFrame() && groundedPlayer) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}