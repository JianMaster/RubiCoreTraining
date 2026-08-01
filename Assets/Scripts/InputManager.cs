using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _jumpAction;

    private InputData _inputData;
    void OnEnable() {
        _moveAction.action.Enable();
        _jumpAction.action.Enable();
    }
    void OnDisable() {
        _moveAction.action.Disable();
        _jumpAction.action.Disable();
    }

    void Update() {
        _inputData.direction = _moveAction.action.ReadValue<Vector2>();
        if (_jumpAction.action.WasPressedThisFrame()) {
            _inputData.jump = true;
        }

    }

    public InputData GetInput() {
        InputData inputData = _inputData;
        _inputData.Reset();
        return inputData;
    }
}

public struct InputData {
    public Vector2 direction;
    public bool jump;

    public void Reset() {
        direction = Vector2.zero;
        jump = false;
    }
}
