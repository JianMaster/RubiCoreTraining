using UnityEngine;

public class Main : MonoBehaviour {
    [SerializeField] PlayerController _playerController;
    [SerializeField] InputManager _inputManager;
    [SerializeField] GameRenderer _gameRenderer;
    [SerializeField] Enemy _enemy;
    [SerializeField] EnemyHUD _enemyHUD;

    RendererContext _rendererContext;
    InputData _inputData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _rendererContext = new();
        _inputData = new();
        _enemyHUD.Init(_enemy);

    }

    

    void Update() {
        _inputManager.GetInput(ref _inputData);
        _playerController.Tick(_inputData, Time.deltaTime);
        _enemy.Tick(Time.deltaTime);
    }

    void FixedUpdate() {
        _playerController.PhysicsTick(_inputData, Time.fixedDeltaTime);
        _inputData.jump = false;
        _inputData.attack = false;
    }

    void LateUpdate() {
        _rendererContext.PlayerModel = _playerController.Data;
        _gameRenderer.Render(_rendererContext);
    }
}
