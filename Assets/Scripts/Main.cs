using UnityEngine;

public class Main : MonoBehaviour {
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameRenderer _gameRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    void FixedUpdate() {
        InputData inputData = _inputManager.GetInput();
    }

    void LateUpdate() {
        _gameRenderer.Render();
    }
}
