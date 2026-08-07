using UnityEngine;

public class Main : MonoBehaviour {
    [SerializeField] PlayerController _playerController;
    [SerializeField] InputManager _inputManager;
    [SerializeField] GameRenderer _gameRenderer;

    RendererContext _rendererContext;
    InputData _inputData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _rendererContext = new();
        _inputData = new();
    }

    void Update() {
        _inputManager.GetInput(ref _inputData);
        _playerController.Tick(_inputData, Time.deltaTime);
    }

    void FixedUpdate() {
        _playerController.PhysicsTick(_inputData, Time.fixedDeltaTime);
        _inputData.jump = false;
    }

    void LateUpdate() {
        _rendererContext.PlayerModel = _playerController.PlayerModel;
        _gameRenderer.Render(_rendererContext);
    }
}
