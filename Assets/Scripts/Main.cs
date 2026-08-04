using UnityEngine;

public class Main : MonoBehaviour {
    [SerializeField] PlayerController _playerController;
    [SerializeField] InputManager _inputManager;
    [SerializeField] GameRenderer _gameRenderer;

    RendererContext _rendererContext;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _rendererContext = new RendererContext();
    }

    void FixedUpdate() {
        InputData inputData = _inputManager.GetInput();
        _playerController.Tick(inputData, Time.fixedDeltaTime);

        _rendererContext.PlayerModel = _playerController.PlayerModel;
    }

    void LateUpdate() {
        _gameRenderer.Render(_rendererContext);
    }
}
