using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _jumpAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _moveAction.action.Enable();
        _jumpAction.action.Enable();
    }

    public InputData GetInput() {
        InputData inputData = new InputData {
            direction = _moveAction.action.ReadValue<Vector2>(),
            jump = _jumpAction.action.WasPressedThisFrame()
        };
        return inputData;
    }
}

public struct InputData {
    public Vector2 direction;
    public bool jump;
}
