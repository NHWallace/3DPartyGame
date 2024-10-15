using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton pattern
public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance {
        get { return instance; }
    }

    private PlayerInputActions playerInputActions;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }

        playerInputActions = new PlayerInputActions();
        Cursor.visible = false;
    }

    private void OnEnable() {
        playerInputActions.Enable();
    }

    private void OnDisable() {
        playerInputActions.Disable();
    }

    public Vector2 GetPlayerMovement() {
        return playerInputActions.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta() {
        return playerInputActions.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumpedThisFrame() {
        return playerInputActions.Player.Jump.triggered; // Returns true if trigger on this frame.
    }

}
