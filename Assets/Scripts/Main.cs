using UnityEngine;

public class Main : MonoBehaviour {
    [SerializeField] PlayerController _player;
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
        _enemyHUD.Bind(_enemy);
        _gameRenderer.Bind(_player.Renderer, _enemy.Renderer);
        _enemy.OnRushEvent += OnRush;
    }

    void Update() {
        _inputManager.GetInput(ref _inputData);
        _player.Tick(_inputData, Time.deltaTime);
        if (timeSlow - Time.realtimeSinceStartup < 0) {
            Time.timeScale = 1;
        }
    }

    void FixedUpdate() {
        _player.PhysicsTick(_inputData, Time.fixedDeltaTime);
        _enemy.PhysicsTick(Time.fixedDeltaTime);
        _inputData.jump = false;
        _inputData.attack = false;
    }

    void LateUpdate() {
        _rendererContext.PlayerModel = _player.Data;
        _gameRenderer.Render(_rendererContext);
    }

    public float timeSlow = 0;
    void OnRush(int _) {
        timeSlow = Time.realtimeSinceStartup + 1;
        Time.timeScale = 0.01f;
    }
}
