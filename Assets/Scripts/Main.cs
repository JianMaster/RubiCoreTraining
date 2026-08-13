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
        _enemyHUD.Init(_enemy.Data, _enemy.HUDAnchor.GetChild(0), _enemy.HUDAnchor.GetChild(1));

    }

    void OnEnable() {
        _enemy.OnHpChanged += _enemyHUD.OnHpChanged;
        _enemy.OnFocusChanged += _enemyHUD.OnFocusChanged;
    }

    void OnDisable() {
        _enemy.OnHpChanged -= _enemyHUD.OnHpChanged;
        _enemy.OnFocusChanged -= _enemyHUD.OnFocusChanged;
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
        _rendererContext.PlayerModel = _playerController.Data;
        _gameRenderer.Render(_rendererContext);
    }
}
