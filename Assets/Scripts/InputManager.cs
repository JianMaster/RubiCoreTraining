using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    [SerializeField] InputActionReference _moveAction;
    [SerializeField] InputActionReference _jumpAction;
    [SerializeField] InputActionReference _mouseAction;
    [SerializeField] InputActionReference _rightClickAction;

    void OnEnable() {
        _moveAction.action.Enable();
        _jumpAction.action.Enable();
        _mouseAction.action.Enable();
        _rightClickAction.action.Enable();
    }
    void OnDisable() {
        _moveAction.action.Disable();
        _jumpAction.action.Disable();
        _mouseAction.action.Disable();
        _rightClickAction.action.Disable();
    }

    public void GetInput(ref InputData inputData) {
        inputData.direction = _moveAction.action.ReadValue<Vector2>();
        Vector2 screenPos = _mouseAction.action.ReadValue<Vector2>();
        inputData.mousePos = Camera.main.ScreenToWorldPoint(screenPos);
        if (_jumpAction.action.WasPressedThisFrame()) {
            inputData.jump = true;
        }

        inputData.onFocus = _rightClickAction.action.IsPressed();

    }
}

public class InputData {
    public Vector2 direction;
    public bool jump;
    public Vector2 mousePos;
    public bool onFocus;


    public InputData Copy() {
        return new InputData {
            direction = this.direction,
            jump = this.jump,
            mousePos = this.mousePos
        };
    }
}
