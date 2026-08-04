using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    [SerializeField] InputActionReference _moveAction;
    [SerializeField] InputActionReference _jumpAction;

    InputData _inputData;
    void OnEnable() {
        _inputData = new();
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
        InputData inputData = _inputData.Copy();
        _inputData.Reset();
        return inputData;
    }
}

public class InputData {
    public Vector2 direction;
    public bool jump;

    public void Reset() {
        direction = Vector2.zero;
        jump = false;
    }

    public InputData Copy() {
        return new InputData {
            direction = this.direction,
            jump = this.jump
        };
    }
}
